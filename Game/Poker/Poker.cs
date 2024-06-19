using System.Security.Cryptography;
using UNSERcasino.Game.Poker.Eval;
using UNSERcasino.Game.Poker.NN;
using UNSERcasino.UI.Menu;

namespace UNSERcasino.Game.Poker
{
    internal class Poker
    {
        public int Pot { get; private set; }
        public int CurrentBet { get; private set; }

        public List<PokerPlayer> Players = new List<PokerPlayer>();
        private List<PokerPlayer> _alivePlayers = new List<PokerPlayer>();

        private PokerPlayer _currentPlayer;
        private PokerPlayer? _lastRaisePlayer;

        public PokerPlayer CurrentVisualPlayer
        {
            get
            {
                return _currentPlayer;
            }
        }

        public Card[] DealerHand { get; private set; }

        public bool Ended { get; private set; }

        private Stack<Card> _cards;

        private int _state = 0;

        private static void _shuffle(Card[] cards)
        {
            for (int _ = 0; _ < 10000; _++)
            {
                int i = RandomNumberGenerator.GetInt32(0, cards.Length);
                int j = RandomNumberGenerator.GetInt32(0, cards.Length);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        private Card[] _getCards()
        {
            return new Card[]
            {
                _cards.Pop(),
                _cards.Pop()
            };
        }

        public Poker()
        {
            Pot = 0;

            Card[] c = Card.GetCards();
            _shuffle(c);

            _cards = new Stack<Card>(c);

            NetworkTrainer networkTrainer = NetworkTrainer.load();

            Players.Add(new PokerPlayerWithBalance(this, CasinoManager.Instance.Name, _getCards(), CasinoManager.Instance));
            Players.Add(new PokerPlayer(this, "Player2", _getCards()));
            Players.Add(new PokerPlayer(this, "Player3", _getCards()));
            Players.Add(new NeuralPlayer(networkTrainer, this, "Bot4", _getCards()));

            DealerHand = new Card[]
            {
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop()
            };

            PokerPlayer[] p = Players.ToArray();
            for (int _ = 0; _ < 10; _++)
            {
                int i = RandomNumberGenerator.GetInt32(0, p.Length);
                int j = RandomNumberGenerator.GetInt32(0, p.Length);

                PokerPlayer temp = p[i];
                p[i] = p[j];
                p[j] = temp;
            }

            Players = new List<PokerPlayer>(p);

            foreach (Card card in DealerHand)
            {
                card.Hidden = true;
            }

            foreach (PokerPlayer player in Players)
            {
                _alivePlayers.Add(player);
            }

            _currentPlayer = _alivePlayers[0];
            _lastRaisePlayer = null;
            Ended = false;
            _state = -1;

            Players[1].SetBet(5);
            Players[0].SetBet(10);
            CurrentBet = 10;
            _lastRaisePlayer = Players[0];
            _currentPlayer = Players[0];
        }

        public bool CanRaise()
        {
            return !Ended && _state != -1;
        }

        public bool CanCheck()
        {
            return !Ended;
        }

        public bool CanFold()
        {
            return !Ended && _state != -1;
        }

        public void Check(PokerPlayer player, int difference)
        {
            if(!(player is NeuralPlayer))
            {
                NetworkTrainer.load().Train(player.Hand, DealerHand, CurrentBet, Pot, new bool[5] { false, true, false, false, false });
            }
            if(_state == -1)
            {
                _state = 0;
            }
            player.afterCheck(difference);
            Next();
            _checkEnd();
        }

        public void Raise(PokerPlayer player, int difference, int amount)
        {
            if (!(player is NeuralPlayer))
            {
                NetworkTrainer.load().Train(player.Hand, DealerHand, CurrentBet, Pot, new bool[5] { false, false, true, true, true });
            }
            CurrentBet = player.Bet;
            _lastRaisePlayer = player;
            player.afterRaise(difference, amount);
            Next();
            _checkEnd();
        }

        public void Fold(PokerPlayer player)
        {
            if (!(player is NeuralPlayer))
            {
                NetworkTrainer.load().Train(player.Hand, DealerHand, CurrentBet, Pot, new bool[5] { true, false, false, false, false });
            }
            player.afterFold();
            Next();
            _alivePlayers.Remove(player);
            _checkEnd();
        }

        private void Next()
        {
            if (_lastRaisePlayer == null && _alivePlayers.IndexOf(_currentPlayer) == _alivePlayers.Count - 1)
            {
                _lastRaisePlayer = _alivePlayers[0];
            }

            _currentPlayer = _alivePlayers[(_alivePlayers.IndexOf(_currentPlayer) + 1) % _alivePlayers.Count];

            if (_currentPlayer == _lastRaisePlayer)
            {
                CurrentBet = 0;
                foreach (PokerPlayer player in _alivePlayers)
                {
                    Pot += player.Bet;
                    player.ResetBet();
                }

                _state++;

                switch (_state)
                {
                    case 1:
                        {
                            DealerHand[0].Hidden = false;
                            DealerHand[1].Hidden = false;
                            DealerHand[2].Hidden = false;
                            break;
                        }
                    case 2:
                        {
                            DealerHand[3].Hidden = false;
                            break;
                        }
                    case 3:
                        {
                            DealerHand[4].Hidden = false;
                            break;
                        }
                }
            }

            _currentPlayer.Next();

            //Console.ReadLine();
        }

        private void _checkEnd()
        {
            if (Ended)
            {
                return;
            }
            if (_alivePlayers.Count == 1)
            {
                _onEnd();
            }
            else if (_state == 4)
            {
                _onEnd();
            }
        }

        private void _onEnd()
        {
            Ended = true;
            //MenuManager.close(); //TODO: change
            if (_alivePlayers.Count == 1) // this player wins everything
            {
                _alivePlayers[0].Win(Pot);
            }
            else
            {
                PokerWinMenu menu = new PokerWinMenu(DealerHand);

                List<PokerPlayer>? bestPlayer = null;
                int bestScore = 0;
                foreach (PokerPlayer player in _alivePlayers)
                {
                    List<Result> result = Evaluator.Eval(DealerHand, player.Hand);
                    menu.addPlayer(player, result);
                    int score = Evaluator.GetScore(result);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestPlayer = new List<PokerPlayer> { player };
                    }
                    else if (score == bestScore && bestPlayer != null)
                    {
                        bestPlayer.Add(player);
                    }
                }

                int payputPerPlayer = Pot / bestPlayer.Count;

                foreach (PokerPlayer pokerPlayer in bestPlayer)
                {
                    pokerPlayer.Win(payputPerPlayer);
                }

                MenuManager.open(menu);
            }
        }
    }
}
