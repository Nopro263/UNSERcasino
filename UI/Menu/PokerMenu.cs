﻿using UNSERcasino.Game.Poker;
using UNSERcasino.Game.Poker.Eval;

namespace UNSERcasino.UI.Menu
{
    internal class PokerMenu : Menu, IUpdateable
    {
        private Poker _poker;
        private Text _potText;
        private Text[][] _opponents;
        private CardView _card1;
        private CardView _card2;
        private PokerPlayer? _visualPlayer;

        private ButtonView btFold;
        private ButtonView btCheck;

        private TextInputView tip;
        public PokerMenu() : base()
        {
            _poker = new Poker();

            _potText = new Text("Pot: ");

            _opponents = new Text[][]
            {
                new Text[]
                {
                    new Text("Player"),
                    new Text(""),
                    new Text(""),
                    new Text(""),
                    new Text("")
                },
                new Text[]
                {
                    new Text("Bet"),
                    new Text(""),
                    new Text(""),
                    new Text(""),
                    new Text("")
                }
            };

            _card1 = new CardView(_poker.CurrentVisualPlayer.Hand[0]);
            _card2 = new CardView(_poker.CurrentVisualPlayer.Hand[1]);


            _scene.addView(_card1, Flow.CENTER, Flow.END, -15 / 2 - 1, 0);
            _scene.addView(_card2, Flow.CENTER, Flow.END, 15 / 2 + 1, 0);

            _scene.addView(new CardView(_poker.DealerHand[0]), Flow.CENTER, Flow.START, -32, 0);
            _scene.addView(new CardView(_poker.DealerHand[1]), Flow.CENTER, Flow.START, -16, 0);
            _scene.addView(new CardView(_poker.DealerHand[2]), Flow.CENTER, Flow.START, 0, 0);
            _scene.addView(new CardView(_poker.DealerHand[3]), Flow.CENTER, Flow.START, 16, 0);
            _scene.addView(new CardView(_poker.DealerHand[4]), Flow.CENTER, Flow.START, 32, 0);

            _scene.addView(new TextView(_potText, false, true), Flow.CENTER, Flow.CENTER);
            _scene.addView(new TableView(_opponents), Flow.START, Flow.END);

            btFold = new ButtonView(new Text("Fold"), false);
            btCheck = new ButtonView(new Text("Check"), false);

            tip = new TextInputViewRegex(false, 5, "Raise");

            _scene.addView(btCheck, Flow.END, Flow.END, 0, -2);
            _scene.addView(tip, Flow.END, Flow.END, 0, -1);
            _scene.addView(btFold, Flow.END, Flow.END, 0, 0);

            renderOpponents();
        }

        private void renderOpponents()
        {
            Text[] players = _opponents[0];
            Text[] bets = _opponents[1];

            int i = 1;
            foreach (PokerPlayer player in _poker.Players)
            {
                players[i].setContent(player.Name);
                if (player.HasFolded)
                {
                    bets[i].setContent("-");
                }
                else
                {
                    bets[i].setContent(player.Bet.ToString());
                }

                if (!_poker.Ended && _poker.CurrentVisualPlayer == player)
                {
                    bets[i].Bg = ConsoleColor.DarkYellow;
                    players[i].Bg = ConsoleColor.DarkYellow;
                }
                else
                {
                    bets[i].Bg = Canvas.BACKGROUND;
                    players[i].Bg = Canvas.BACKGROUND;
                }
                i++;
            }
        }

        public void Update()
        {
            if(_visualPlayer == null)
            {
                _visualPlayer = _poker.CurrentVisualPlayer;
            }
            if(_visualPlayer != _poker.CurrentVisualPlayer && !_poker.Ended)
            {
                _visualPlayer = _poker.CurrentVisualPlayer;
                MenuManager.open(new PokerWaitMenu());
            }
            renderOpponents();
            _potText.setContent($"Pot: {_poker.Pot}");

            if (!_poker.CurrentVisualPlayer.CanFold())
            {
                btFold.disable();
            } else
            {
                btFold.enable();
            }

            if (!_poker.CurrentVisualPlayer.CanCheck())
            {
                btCheck.disable();
            } else
            {
                btCheck.enable();
            }

            if (!_poker.CurrentVisualPlayer.CanRaise(1))
            {
                tip.Text.Fg = ConsoleColor.DarkGray;
            } else
            {
                tip.Text.Fg = Canvas.FOREGROUND;
            }

            if(_poker.Ended)
            {
                _potText.setContent("Ended");
            }


            _card1.Card = _poker.CurrentVisualPlayer.Hand[0];
            _card2.Card = _poker.CurrentVisualPlayer.Hand[1];
        }

        public override void onClick(IClickable button)
        {
            base.onClick(button);

            ButtonView? bv = button as ButtonView;

            if (bv != null)
            {
                switch (bv.Text.getContent())
                {
                    case "Fold":
                        {
                            if(_poker.CurrentVisualPlayer.CanFold())
                            {
                                _poker.CurrentVisualPlayer.Fold();
                            }
                            break;
                        }
                    case "Check":
                        {
                            if (_poker.CurrentVisualPlayer.CanCheck())
                            {
                                _poker.CurrentVisualPlayer.Check();
                            }
                            break;
                        }
                }
            }

            TextInputView? tiv = button as TextInputView;

            if(tiv != null)
            {
                int amount = 0;
                if(int.TryParse(tiv.FullContent, out amount))
                {
                    if (amount > 0 && _poker.CurrentVisualPlayer.CanRaise(amount))
                    {
                        _poker.CurrentVisualPlayer.Raise(amount);
                    }
                }
            }
        }
    }
}
