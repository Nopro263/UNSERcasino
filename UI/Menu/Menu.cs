namespace UNSERcasino.UI.Menu
{
    internal abstract class Menu
    {
        protected Scene scene;
        public Menu()
        {
            scene = new Scene();
        }

        public Scene GetScene()
        {
            return scene;
        }

        public virtual void onResume()
        {
            scene.reset();
        }

        public virtual void onClick(IClickable button)
        {

        }
    }
}
