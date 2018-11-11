using System.Collections.Generic;

namespace lunar_horizon.viz
{
    public class World
    {
        //public PresentationMode Presentation = PresentationMode.PhasingLoopsOverview;
        public enum LODGuideType { LADEE, Telescope, SolarViewer };
        public string SpiceDir;

        private long _time;
        public StarBackground Stars;
        public TexturedBall Earth;
        public Ball Sun;
        //public LadeeStateFetcher Fetcher;
        public Ball ViewTarget;
        public MoonDEM Terrain;

        public readonly List<Shape> NearShapes = new List<Shape>();
        public readonly List<Shape> FarShapes = new List<Shape>();

        public readonly List<OpenGLControlWrapper> Wrappers = new List<OpenGLControlWrapper>();

        public LODGuideType LODGuide = LODGuideType.Telescope;

        public long Time
        {
            get { return _time; }
            set { Update(value); }
        }

        /*
        public LadeeStateFetcher.StateFrame Frame
        {
            get { return Fetcher.Frame; }
            set { Fetcher.Frame = value; }
        }*/

        public void Tick()
        {
            /*
            Fetcher.FetchJ200Attitude(_time, ref Stars.Attitude);
            Fetcher.FetchPositionAndAttitude(LadeeStateFetcher.EarthId, _time, ref Earth.Position, ref Earth.Attitude);
            //Fetcher.FetchPositionAndAttitude(LadeeStateFetcher.MoonId, _time, ref Moon.Position, ref Moon.Attitude);
            Fetcher.FetchPositionAndAttitude(LadeeStateFetcher.SunId, _time, ref Sun.Position, ref Sun.Attitude);

            var earthVec = Earth.Position - ViewTarget.Position;
            earthVec.NormalizeFast();
            ViewTarget.EarthVector.Vector = earthVec;
            var sunVec = Sun.Position - ViewTarget.Position;
            sunVec.NormalizeFast();
            ViewTarget.SunVector.Vector = sunVec;

               */

            var count = Wrappers.Count;
            for (var i = 0; i < count; i++)
            {
                var w = Wrappers[i];
                if (w.CameraMode != null)
                    w.CameraMode.Tick();
                w.Invalidate();
            } 
        }

        public void Update(long t)
        {
            _time = t;
            Tick();
        }
    }

}
