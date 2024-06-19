using System;
using System.Collections;
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
        private List<double[]> results = new List<double[]>();
        private int _count;


        public NetworkTrainer() { }
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

        public void save()
        {
            int i = 0;
            foreach(PokerNetwork nn in neuralNetworks)
            {
                nn.save(i.ToString());
                i++;
            }
        }

        public static NetworkTrainer load(int size)
        {
            NetworkTrainer nt = new NetworkTrainer();

            for(int i = 0; i < size; i++)
            {
                nt.neuralNetworks.Add(PokerNetwork.load(i.ToString()));
            }

            return nt;
        }

        public static NetworkTrainer NMain()
        {
            NetworkTrainer nt = new NetworkTrainer(1000);

            while(nt.Step())
            {}

            nt.save();

            return nt;
        }

        private bool Step()
        {
            generateState(out Card[] hand, out Card[] dealer, out int cb, out int cp);
            Console.WriteLine($"cb: {cb}, cp: {cp}");
            Console.WriteLine(hand[0] + " : " + hand[1]);
            Console.WriteLine(dealer[0] + " : " + dealer[1] + " : " + dealer[2] + " : " + dealer[3] + " : " + dealer[4]);
            Console.WriteLine();

            

            Console.Write("? ");
            string s = Console.ReadLine();
            if(s == "")
            {
                return false;
            }
            NetworkResult desiredResult = (NetworkResult)int.Parse(s);
            //NetworkResult desiredResult = (NetworkResult)Array.FindLastIndex(stats, (x) => x > 0);

            bool[] dr = new bool[5];
            dr[(int)desiredResult] = true;

            int[] stats = null;
            for(int i = 0; i < 5; i++)
            {
                Console.Write(i);
                Console.CursorLeft = 0;
                stats = Train(hand, dealer, cb, cp, dr);
            }

            Console.Clear();

            foreach (int x in stats)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            stats = new int[5];

            foreach(PokerNetwork network in neuralNetworks)
            {
                stats[NeuralNetwork.IndexOfHighestNode(network.Run(hand, dealer, cb, cp))]++;
            }

            foreach (int x in stats)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();

            Console.Clear();
            return true;
        }

        public int Predict(Card[] hand, Card[] dealer, int cb, int cp)
        {
            int[] stats = new int[5];

            foreach (PokerNetwork network in neuralNetworks)
            {
                stats[NeuralNetwork.IndexOfHighestNode(network.Run(hand, dealer, cb, cp))]++;
            }

            return Array.IndexOf(stats, stats.Max());
        }

        private int[] Train(Card[] hand, Card[] dealer, int cb, int cp, bool[] desiredResult)
        {
            int[] stats = new int[5] { 0, 0, 0, 0, 0 };

            NetworkWithOutput[] finalNetworks = new NetworkWithOutput[5];

            foreach (PokerNetwork network in neuralNetworks)
            {
                double[] r = network.Run(hand, dealer, cb, cp);
                int i = NeuralNetwork.IndexOfHighestNode(r);
                results.Add(r);
                stats[i]++;
                if (finalNetworks[i] == null)
                {
                    finalNetworks[i] = new NetworkWithOutput(network, r);
                } else
                {
                    if (r[i] > finalNetworks[i].Result[i])
                    {
                        finalNetworks[i] = new NetworkWithOutput(network, r);
                    }
                }
            }

            results.Clear();
            neuralNetworks.Clear();

            PokerNetwork networkToClone = null;

            for (int i = 0; i < 5; i++)
            {
                if (finalNetworks[i] == null)
                {
                    finalNetworks[i] = new NetworkWithOutput(new PokerNetwork(), new double[5] {0,0,0,0,0});
                }
                if (desiredResult[i])
                {
                    networkToClone = finalNetworks[i].Network;
                } else
                {
                    neuralNetworks.Add(finalNetworks[i].Network);
                }
            }

            for(int i = 0; i < _count - 4; i++)
            {
                neuralNetworks.Add(new PokerNetwork(networkToClone));
            }

            return stats;
        }

        class NetworkWithOutput
        {
            public PokerNetwork Network { get; set; }
            public double[] Result { get; set; }

            public NetworkWithOutput(PokerNetwork network, double[] result)
            {
                Network = network;
                Result = result;
            }
        }
    }
}
