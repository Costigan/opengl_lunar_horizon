using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using lunar_horizon.calc;
using lunar_horizon.features;
using lunar_horizon.mouse;
using lunar_horizon.tiles;
using lunar_horizon.views;

namespace lunar_horizon.view
{
    public interface IMapLayer : System.IDisposable
    {
        string Name { get; }
        float Transparency { get; set; }
        void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null);
        System.Windows.Forms.UserControl GetPropertySheet();

        /// <summary>
        /// Called when the layer is highighted or unhighlighted (mouse highlighted).
        /// Currently, only one layer can be highlighted.
        /// </summary>
        /// <param name="view"></param>
        void Selected(MapView view, bool isSelected);

        /// <summary>
        ///  Called when the layer's checkbox is changed (which changes whether the layer is rendered)
        /// </summary>
        /// <param name="view"></param>
        /// <param name="isChecked"></param>
        void Checked(MapView view, bool isChecked);
    }

    public abstract class MapLayer : IMapLayer
    {
        public string Name { get; set; }
        protected float _Transparency = 1f;
        protected readonly ImageAttributes _imageAttributes = new ImageAttributes();  // Holds the image attributes so it can be reused
        protected ImageAttributes _drawAttributes = null;  // Either points at _imageAttributes or null
        protected readonly ColorMatrix _colorMatrix = new ColorMatrix();
        public float Transparency
        {
            get { return _Transparency; }
            set
            {
                _Transparency = value;
                if (_Transparency < 0f) _Transparency = 0f;
                if (_Transparency > 1f) _Transparency = 1f;
                if (_Transparency == 1f)
                    _drawAttributes = null;
                else
                {
                    _colorMatrix.Matrix33 = _Transparency;
                    _imageAttributes.SetColorMatrix(_colorMatrix);
                    _drawAttributes = _imageAttributes;
                }
            }
        }

        public override string ToString() => Name;

        public abstract void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null);

        public virtual UserControl GetPropertySheet() => null;

        public virtual void Selected(MapView view, bool isSelected) { }
        public virtual void Checked(MapView view, bool isChecked) { }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MapLayer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class TileTreeMapLayer : MapLayer
    {
        public TileTree Tree;
        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            Tree.Draw(g, t, _drawAttributes);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Tree?.Dispose();
        }
    }

    public class SunShadowMapLayer : MapLayer
    {
        public LunarHorizon MainWindow;
        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            var view = MainWindow.MapView;
            var bounds = view.Bounds;
            foreach (var p in view.FinishedPatches)
            {
                if (p.IsVisible(t, bounds))
                {
                    p.FillMatrices(TileTreeNode.Terrain);
                    p.Draw(g, t, p.GetSunShadowsCarefully(view.SunVector));
                }
            }
        }
    }

    public class EarthShadowMapLayer : MapLayer
    {
        public LunarHorizon MainWindow;
        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            var view = MainWindow.MapView;
            var bounds = view.Bounds;
            foreach (var p in view.FinishedPatches)
            {
                if (p.IsVisible(t, bounds))
                {
                    p.FillMatrices(TileTreeNode.Terrain);
                    p.Draw(g, t, p.GetEarthShadows(view.EarthVector));
                }
            }
        }
    }

    public class FeatureNameMapLayer : MapLayer
    {
        public LunarHorizon MainWindow;
        private readonly FeatureManager _features;

        public FeatureNameMapLayer()
        {
            _features = new FeatureManager();
            _features.Load();
        }
        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            var view = MainWindow.MapView;
            var bounds = view.Bounds;
            _features.Draw(g, t);
        }
    }

    public class SelectedPatchesMapLayer : MapLayer
    {
        public LunarHorizon MainWindow;
        protected readonly Brush _selectionBrush = new SolidBrush(Color.FromArgb(200, 100, 255, 100)); // green
        protected readonly Pen _selectionPen = new Pen(Color.FromArgb(130, 255, 130), 1);

        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            foreach (var p in MainWindow.MapView.SelectedPatches)
            {
                p.Draw(g, t, _selectionBrush);
                p.Draw(g, t, _selectionPen);
            }
        }
    }

    public class LocationProbeMapLayer : MapLayer
    {
        public static LocationProbePropertySheet DefaultPropertySheet = new LocationProbePropertySheet();
        public List<LocationProbe> Probes = new List<LocationProbe>();
        public LunarHorizon MainWindow;

        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null) { }

        public override void Checked(MapView view, bool isChecked) {
            view.Probes = isChecked ? Probes : null;
            view.Invalidate();
        }

        public override UserControl GetPropertySheet() => DefaultPropertySheet;
    }

}
