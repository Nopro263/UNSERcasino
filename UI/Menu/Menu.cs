namespace UNSERcasino.UI.Menu
{
    internal abstract class Menu // Baseclass for all menus
    {
        protected Scene scene;
        public Menu()
        {
            scene = new Scene(this);
            scene.addView(new PlayerBalanceView(), Flow.END, Flow.START); // Put the players balance top-right.
        }

        public Scene GetScene()
        {
            return scene;
        }

        public virtual void onResume()
        {
            scene.reset();
        }
        public virtual void onHide()
        {
            scene.reset();
        }

        public virtual void onClick(IClickable button)
        {

        }
    }
}
