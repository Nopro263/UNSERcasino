using System.Security.Cryptography;
using UNSERcasino.Game.Poker.Eval;
using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker
{
    internal class Poker
    {
        public int Pot { get; private set; }
        public int CurrentBet { get; private set; }

        public List<PokerPlayer> Players = new List<PokerPlayer>();
        private List<PokerPlayer> _alivePlayers = new List<PokerPlayer>();

        private PokerPlayer _currentPlayer;

        public PokerPlayer CurrentVisualPlayer
        {
            get {
                return _currentPlayer;
            }
        }

        public Card[] DealerHand { get; private set; }

        public bool Ended { get; private set; }

        private Stack<Card> _cards;

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

            foreach (PokerPlayer player in Players)
            {
                _alivePlayers.Add(player);
            }

            _currentPlayer = _alivePlayers.First();
            Ended = false;
        }

        public void Check(PokerPlayer player)
        {
            Next();
        }

        public void Raise(PokerPlayer player)
        {
            CurrentBet = player.Bet;
            Next();
        }

        public void Fold(PokerPlayer player)
        {
            Next();
            _alivePlayers.Remove(player);
        }

        private void Next()
        {
            _currentPlayer = _alivePlayers[(_alivePlayers.IndexOf(_currentPlayer) + 1) % _alivePlayers.Count];
        }
    }
}
