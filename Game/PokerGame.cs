using NoahCardOutput;

namespace UNSERcasino.Game
{
    internal class PokerGame
    {
        private Stack<PrintableCard> _cards;

        private PrintableCard[] _communityCards;

        private List<PokerPlayer> _players;
        private string? _currentPlayer;

        private PrintableCard[] shuffle()
        {
            PrintableCard[] r = PrintableCard.getAllCards();
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                int i1 = random.Next(r.Length);
                int i2 = random.Next(r.Length);

                PrintableCard temp = r[i1];
                r[i1] = r[i2];
                r[i2] = temp;
            }

            return r;
        }


        public PokerGame()
        {
            _cards = new Stack<PrintableCard>(shuffle());

            _players = new List<PokerPlayer>();
            _currentPlayer = null;

            _communityCards = new PrintableCard[5];

            for (int i = 0; i < _communityCards.Length; i++)
            {
                _communityCards[i] = _cards.Pop();
                _communityCards[i].Hidden = true;
            }
        }

        public void addPlayer(string name, int token)
        {
            PrintableCard[] hand = new PrintableCard[2];
            hand[0] = _cards.Pop();
            hand[1] = _cards.Pop();

            _players.Add(new PokerPlayer(name, hand, token));

            if (_currentPlayer == null)
            {
                _currentPlayer = name;
            }
        }

        public List<PokerPlayer> getOpponents()
        {
            List<PokerPlayer> r = new List<PokerPlayer>();

            foreach (PokerPlayer p in _players)
            {
                if (p.Name != _currentPlayer)
                {
                    r.Add(p);
                }
            }

            return r;
        }

        public PokerPlayer getPlayer()
        {
            foreach (PokerPlayer p in _players)
            {
                if (p.Name == _currentPlayer)
                {
                    return p;
                }
            }

            return null;
        }

        public int getPot()
        {
            return 7;
        }

        public PrintableCard[] getCommunityCards()
        {
            return _communityCards;
        }

        public void FirstThree()
        {
            _communityCards[0].Hidden = false;
            _communityCards[1].Hidden = false;
            _communityCards[2].Hidden = false;
        }

        public void FourthThree()
        {
            _communityCards[3].Hidden = false;
        }

        public void FifthThree()
        {
            _communityCards[4].Hidden = false;
        }
    }
}
