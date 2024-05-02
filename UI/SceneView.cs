namespace UNSERcasino.UI
{
    internal class SceneView : IView
    {
        private Scene scene;
        public SceneView(Scene baseScene) {
            scene = baseScene;
        }

        public void addView(IView view, Flow xo, Flow yo, int x, int y)
        {
            scene.addView(view, xo, yo, x, y);
        }
        public int getXSize()
        {
            throw new NotImplementedException();
        }

        public int getYSize()
        {
            throw new NotImplementedException();
        }

        public void printToCanvas(Canvas canvas, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
