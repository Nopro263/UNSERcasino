using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.Game.Poker.NN
{
    internal class PokerNetwork
    {
        private NeuralNetwork neuralNetwork;

        public PokerNetwork()
        {
            neuralNetwork = new NeuralNetwork(16, 50, 50, 5);
        }

        public PokerNetwork(PokerNetwork clone)
        {
            neuralNetwork = new NeuralNetwork(clone.neuralNetwork);
        }

        public string save()
        {
            return neuralNetwork.save();
        }

        public static PokerNetwork load(string data)
        {
            PokerNetwork p = new PokerNetwork();

            p.neuralNetwork = NeuralNetwork.load(data);

            return p;
        }

        public double[] Run(Card[] hand, Card[] dealer, int currentBet, int currentPot)
        {
            double[] input = new double[16];
            int offset = 2;
            input[0] = currentBet;
            input[1] = currentPot;

            CardToDouble(hand[0], input, ref offset);
            CardToDouble(hand[1], input, ref offset);
            CardToDouble(dealer[0], input, ref offset);
            CardToDouble(dealer[1], input, ref offset);
            CardToDouble(dealer[2], input, ref offset);
            CardToDouble(dealer[3], input, ref offset);
            CardToDouble(dealer[4], input, ref offset);

            double[] i = neuralNetwork.CalculateOutputs(input);
            return i;
        }

        private void CardToDouble(Card card, double[] input, ref int offset)
        {
            int cardSuit = 0;
            int cardValue = card.CardValue.Rating;

            switch(card.CardType)
            {
                case UI.CardType.Pik: cardSuit = 1; break;
                case UI.CardType.Karo: cardSuit = 2; break;
                case UI.CardType.Herz: cardSuit = 3; break;
                case UI.CardType.Kreuz: cardSuit = 4; break;
            }

            if(card.Hidden)
            {
                cardSuit = 0;
                cardValue = 0;
            }

            input[offset++] = cardSuit;
            input[offset++] = cardValue;
        }
    }

    internal enum NetworkResult
    {
        Fold = 0,
        Check = 1,
        RaiseSmall = 2,
        RaiseMedium = 3,
        RaiseLarge = 4,
    }
}
