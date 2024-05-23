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

        public Poker()
        {
            Pot = 0;

            Players.Add(new PokerPlayer(this, "Player1", new Card[] {new Card(CardValue.KOENIG, CardType.Pik, false),
                                                                     new Card(CardValue.DREI, CardType.Pik, false)}));
            Players.Add(new PokerPlayer(this, "Player2", new Card[] {new Card(CardValue.ZWEI, CardType.Pik, false),
                                                                     new Card(CardValue.VIER, CardType.Pik, false)}));
            Players.Add(new PokerPlayer(this, "Player3", new Card[] {new Card(CardValue.FUENF, CardType.Pik, false),
                                                                     new Card(CardValue.SECHS, CardType.Pik, false)}));
            Players.Add(new PokerPlayer(this, "Player4", new Card[] {new Card(CardValue.SIEBEN, CardType.Pik, false),
                                                                     new Card(CardValue.ACHT, CardType.Pik, false)}));

            DealerHand = new Card[] {new Card(CardValue.ASS, CardType.Herz, false),
                                     new Card(CardValue.DREI, CardType.Kreuz, false),
                                     new Card(CardValue.DREI, CardType.Kreuz, false),
                                     new Card(CardValue.DREI, CardType.Kreuz, false),
                                     new Card(CardValue.DREI, CardType.Kreuz, false)};

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
