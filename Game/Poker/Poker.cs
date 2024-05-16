using System.Security.Cryptography;
using UNSERcasino.Game.Poker.Eval;
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

        private int _state = 0;

        //public PokerPlayer Me { get; private set; }
        public PokerPlayer Me
        {
            get
            {
                return Current;
            }
        }

        private int indexOfLastRaise;

        public bool isCurrentMe
        {
            get
            {
                return !Ended && Me == Current;
            }
        }

        public PokerPlayer Current
        {
            get
            {
                return AlivePlayers[currentPlayer];
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

            /*Me = createBalancedPlayer();
            createBotPlayer("Emilio");
            createBotPlayer("Jonas");
            createBotPlayer("Tim");*/
            createBalancedPlayer();
            createPlayer();
            createPlayer();
            createPlayer();

            DealerHand = new Card[] {
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop()
            };

            foreach(Card ca in DealerHand)
            {
                ca.Hidden = true;
            }

            Pot = 0;
        }

        private PokerPlayer createPlayer()
        {
            Card[] hand = new Card[] {
                _cards.Pop(),
                _cards.Pop()
            };

            PokerPlayer player = new PokerPlayer(this, "Test", 0, hand);
            Players.Add(player);
            AlivePlayers.Add(player);
            return player;
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
                currentPlayer = 0;
                AlivePlayers[0].OnWin(Pot);
                Pot = 0;
                return;
            }

            currentPlayer = (currentPlayer + 1) % AlivePlayers.Count;
            indexOfLastRaise++;

            if(indexOfLastRaise == AlivePlayers.Count) // Round complete
            {
                CurrentBet = 0;
                indexOfLastRaise = 0;
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
                    case 4:
                        {
                            Ended = true;
                            break;
                        }
                }

                foreach (PokerPlayer player in AlivePlayers)
                {
                    if(Ended)
                    {
                        Evaluator.Eval(DealerHand, player.Hand);
                    }

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

            currentPlayer--;
            indexOfLastRaise--;

            next();
        }

        public int raise(PokerPlayer player, int change)
        {
            if (player != Current) { throw new NotYouException(); }
            CurrentBet = CurrentBet + change;
            int v = CurrentBet - player.Bet;
            player.Bet = CurrentBet;

            indexOfLastRaise = 0;

            next();

            return v;
        }

        public int check(PokerPlayer player)
        {
            if (player != Current) { throw new NotYouException(); }
            int v = CurrentBet - player.Bet;
            player.Bet = CurrentBet;

            next();

            return v;
        }
    }
}
