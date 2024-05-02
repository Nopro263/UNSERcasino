﻿namespace UNSERcasino.UI.Menu
{
    internal class MainMenu : Menu
    {
        public MainMenu() : base()
        {
            scene.addView(new ButtonView(new Text("Crash"), false), Flow.CENTER, Flow.CENTER);
            scene.addView(new ButtonView(new Text("Dice"), false), Flow.CENTER, Flow.CENTER, 0, 1);
            scene.addView(new ButtonView(new Text("Poker"), false), Flow.CENTER, Flow.CENTER, 0, 2);
            scene.addView(new ButtonView(new Text("Exit"), false), Flow.CENTER, Flow.CENTER, 0, 3);
        }

        public override void onClick(IClickable i)
        {
            ButtonView? button = i as ButtonView;
            if (button == null)
            {
                return;
            }
            if (button.Text.getContent() == "Crash")
            {
                MenuManager.open(new CrashMenu());
            }
            if (button.Text.getContent() == "Dice")
            {
                MenuManager.open(new DiceMenu());
            }
            if (button.Text.getContent() == "Poker")
            {
                MenuManager.open(new PokerMenu());
            }
            if (button.Text.getContent() == "Exit")
            {
                MenuManager.close();
            }
        }
    }
}
