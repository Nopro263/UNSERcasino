using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.Game.Poker.NN
{
    internal class NetworkTrainer
    {
        private List<PokerNetwork> neuralNetworks = new List<PokerNetwork>();
        private List<NetworkResult> results = new List<NetworkResult>();
        private int _count;

        public NetworkTrainer(int networks)
        {
            _count = networks;
            for(int i = 0; i < networks; i++)
            {
                neuralNetworks.Add(new PokerNetwork());
            }
        }

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

        private static void generateState(out Card[] hand, out Card[] dealer, out int cb, out int cp)
        {
            Card[] c = Card.GetCards();
            _shuffle(c);
            Stack<Card> stack = new Stack<Card>(c);

            hand = new Card[]
            {
                stack.Pop(),
                stack.Pop()
            };

            dealer = new Card[]
            {
                stack.Pop(),
                stack.Pop(),
                stack.Pop(),
                stack.Pop(),
                stack.Pop()
            };

            foreach (Card card in dealer)
            {
                card.Hidden = true;
            }

            switch (RandomNumberGenerator.GetInt32(0, 4))
            {
                case 1:
                    {
                        dealer[0].Hidden = false;
                        dealer[1].Hidden = false;
                        dealer[2].Hidden = false;
                        break;
                    }
                case 2:
                    {
                        dealer[0].Hidden = false;
                        dealer[1].Hidden = false;
                        dealer[2].Hidden = false;
                        dealer[3].Hidden = false;
                        break;
                    }
                case 3:
                    {
                        dealer[0].Hidden = false;
                        dealer[1].Hidden = false;
                        dealer[2].Hidden = false;
                        dealer[3].Hidden = false;
                        dealer[4].Hidden = false;
                        break;
                    }
            }

            cb = RandomNumberGenerator.GetInt32(0, 50);
            cp = RandomNumberGenerator.GetInt32(0, 2000);
        }

        public static void NMain()
        {
            NetworkTrainer nt = new NetworkTrainer(100);

            while(true)
            {
                Console.Clear();
                nt.Step();
            }
        }

        private void Step()
        {
            generateState(out Card[] hand, out Card[] dealer, out int cb, out int cp);
            Console.WriteLine($"cb: {cb}, cp: {cp}");
            Console.WriteLine(hand[0] + " : " + hand[1]);
            Console.WriteLine(dealer[0] + " : " + dealer[1] + " : " + dealer[2] + " : " + dealer[3] + " : " + dealer[4]);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            foreach (PokerNetwork network in neuralNetworks)
            {
                results.Add(network.Run(hand, dealer, cb, cp));
                Console.WriteLine(results.Last());
            }

            Console.WriteLine("?");
            NetworkResult desiredResult = (NetworkResult)int.Parse(Console.ReadLine());

            PokerNetwork? networkToClone = null;

            for (int i = 0; i < neuralNetworks.Count; i++)
            {
                if (results[i] == desiredResult)
                {
                    networkToClone = neuralNetworks[i];
                    break;
                }
            }

            results.Clear();
            neuralNetworks.Clear();

            for (int i = 0; i < _count; i++)
            {
                neuralNetworks.Add(new PokerNetwork(networkToClone));
            }
        }
    }
}
