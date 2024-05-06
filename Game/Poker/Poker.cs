using System.Security.Cryptography;
using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker
{
    internal class Poker
    {
        public Card[] DealerHand { get; private set; }

        public int Pot { get; private set; }
        public int CurrentBet { get; private set; }
        public bool Ended { get; private set; }

        private Stack<Card> _cards;
        public List<PokerPlayer> Players { get; private set; }
        public List<PokerPlayer> AlivePlayers { get; private set; }

        public PokerPlayer Me { get; private set; }

        private int indexOfLastRaise;

        public bool isCurrentMe
        {
            get
            {
                return Me == Current;
            }
        }

        public PokerPlayer Current
        {
            get
            {
                return Players[currentPlayer];
            }
        }

        private int currentPlayer;

        public Poker()
        {
            Players = new List<PokerPlayer>();
            AlivePlayers = new List<PokerPlayer>();
            Ended = false;

            Card[] c = Card.GetCards();
            shuffle(c);

            _cards = new Stack<Card>(c);

            currentPlayer = 0;
            indexOfLastRaise = 0;

            Me = createBalancedPlayer();
            createBotPlayer("Emilio");

            DealerHand = new Card[] {
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop()
            };

            Pot = 100;
        }

        private PokerPlayer createBalancedPlayer()
        {
            Card[] hand = new Card[] {
                _cards.Pop(),
                _cards.Pop()
            };

            PokerPlayer player = new BalancedPokerPlayer(this, CasinoManager.Instance.Name, 0, hand);
            Players.Add(player);
            AlivePlayers.Add(player);
            return player;
        }

        private PokerPlayer createBotPlayer(string name)
        {
            Card[] hand = new Card[] {
                _cards.Pop(),
                _cards.Pop()
            };

            PokerPlayer player = new RandomBotPokerPlayer(this, name, 0, hand);
            Players.Add(player);
            AlivePlayers.Add(player);
            return player;
        }

        private static void shuffle(Card[] cards)
        {
            for(int _ = 0; _ < 10000; _++)
            {
                int i = RandomNumberGenerator.GetInt32(0, cards.Length);
                int j = RandomNumberGenerator.GetInt32(0, cards.Length);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        private void next()
        {
            if (AlivePlayers.Count == 1)
            {
                Ended = true;
                return;
            }

            currentPlayer = (currentPlayer + 1) % AlivePlayers.Count;

            if(currentPlayer == indexOfLastRaise) // Round complete
            {
                foreach(PokerPlayer player in AlivePlayers)
                {
                    Pot += player.Bet;
                    player.Bet = 0;
                }
            }

            Current.OnAction();
        }

        public void fold(PokerPlayer player)
        {
            if(player != Current) { throw new NotYouException(); }
            Pot += player.Bet;
            player.Bet = 0;

            AlivePlayers.Remove(player);

            foreach(Card c in player.Hand)
            {
                c.Hidden = true;
            }

            next();
        }

        public void raise(PokerPlayer player, int change)
        {
            if (player != Current) { throw new NotYouException(); }
            CurrentBet = CurrentBet + change;
            player.Bet += change;

            next();
        }

        public void check(PokerPlayer player)
        {
            if (player != Current) { throw new NotYouException(); }
            player.Bet = CurrentBet;

            next();
        }
    }
}
