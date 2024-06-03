namespace UNSERcasino.UI.Menu
{
    internal abstract class Menu // Baseclass for all menus
    {
        protected Scene _scene;
        public Menu()
        {
            _scene = new Scene(this);
            _scene.addView(new PlayerBalanceView(), Flow.END, Flow.START); // Put the players balance top-right.
        }

        /// <summary>
        /// gets the associated scene from the menu
        /// </summary>
        /// <returns>the scene</returns>
        public Scene GetScene()
        {
            return _scene;
        }

        /// <summary>
        /// called when this menu is the top again
        /// </summary>
        public virtual void onResume()
        {
            _scene.reset();
        }

        /// <summary>
        /// called when the menu is hidden from the screen
        /// </summary>
        public virtual void onHide()
        {
            _scene.reset();
        }

        /// <summary>
        /// called when an IClickable is pressed
        /// </summary>
        /// <param name="button">the IClickable</param>
        public virtual void onClick(IClickable button)
        {

        }
    }
}
