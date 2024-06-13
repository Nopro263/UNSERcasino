using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.UI.Menu
{
    internal class PokerWaitMenu : Menu
    {
        public PokerWaitMenu() : base()
        {
            _scene.addView(new ButtonView(new Text("Skip to next Player"), false), Flow.CENTER, Flow.CENTER);
        }

        public override void onClick(IClickable button)
        {
            base.onClick(button);

            MenuManager.close();
        }
    }
}
