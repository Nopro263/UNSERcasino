using System.Security.Cryptography;
using UNSERcasino.Game.Poker.Eval;
using UNSERcasino.UI;
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
            get {
                return _currentPlayer;
            }
        }

        public Card[] DealerHand { get; private set; }

        public bool Ended { get; private set; }

        private Stack<Card> _cards;

        private int _state = 0;

        private static void shuffle(Card[] cards)
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
            shuffle(c);

            _cards = new Stack<Card>(c);

            Players.Add(new PokerPlayerWithBalance(this, CasinoManager.Instance.Name, _getCards(), CasinoManager.Instance));
            Players.Add(new PokerPlayer(this, "Player2", _getCards()));
            Players.Add(new PokerPlayer(this, "Player3", _getCards()));
            Players.Add(new PokerPlayer(this, "Player4", _getCards()));

            DealerHand = new Card[]
            {
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop()
            };

            foreach(Card card in DealerHand)
            {
                card.Hidden = true;
            }

            foreach (PokerPlayer player in Players)
            {
                _alivePlayers.Add(player);
            }

            _currentPlayer = _alivePlayers.First();
            _lastRaisePlayer = null;
            Ended = false;
        }

        public bool CanRaise()
        {
            return true;
        }

        public bool CanCheck()
        {
            return true;
        }

        public bool CanFold()
        {
            return true;
        }

        public void Check(PokerPlayer player)
        {
            Next();
            checkEnd();
        }

        public void Raise(PokerPlayer player)
        {
            CurrentBet = player.Bet;
            _lastRaisePlayer = player;
            Next();
            checkEnd();
        }

        public void Fold(PokerPlayer player)
        {
            Next();
            _alivePlayers.Remove(player);
            checkEnd();
        }

        private void Next()
        {
            _currentPlayer = _alivePlayers[(_alivePlayers.IndexOf(_currentPlayer) + 1) % _alivePlayers.Count];

            if(_currentPlayer == _lastRaisePlayer)
            {
                CurrentBet = 0;
                foreach(PokerPlayer player in _alivePlayers)
                {
                    Pot += player.Bet;
                    player.ResetBet();
                }

                _state++;

                switch(_state)
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
        }

        private void checkEnd()
        {
            if(_alivePlayers.Count == 1)
            {
                onEnd();
            } else if(_state == 4)
            {
                onEnd();
            }
        }

        private void onEnd()
        {
            MenuManager.close(); //TODO: change

        }
    }
}
