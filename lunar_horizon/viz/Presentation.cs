using OpenTK;

namespace lunar_horizon.viz
{
    public class Presentation
    {
        public void UpdateCamera(OpenGLControlDelegate w)
        {
        }

        public void PaintFarScene(OpenGLControlDelegate w, Vector3d eye)
        {
            for (var i = 0; i < w.TheWorld.FarShapes.Count; i++)
                w.TheWorld.FarShapes[i].Draw(false, eye);  // modified
        }

        public void PaintNearScene(OpenGLControlDelegate w, Vector3d eye)
        {
            for (var i = 0; i < w.TheWorld.NearShapes.Count; i++)
                w.TheWorld.NearShapes[i].Draw(true, eye);
        }

        public void PaintSensors(OpenGLControlDelegate w, Vector3d eye)
        {
            for (var i = 0; i < w.TheWorld.NearShapes.Count; i++)
                w.TheWorld.NearShapes[i].DrawSensors(eye);
        }
    }

    public class PhasingLoopsOverview : Presentation
    {
    }

    public class Science : Presentation
    {
    }
}
