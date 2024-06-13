using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.Game.Poker.NN
{
    internal class Layer
    {
        private int numNodesIn;
        private int numNodesOut;

        private double[,] weights;
        private double[] biases;

        public Layer(int numNodesIn, int numNodesOut)
        {
            this.numNodesIn = numNodesIn;
            this.numNodesOut = numNodesOut;

            weights = new double[numNodesIn, numNodesOut];
            biases = new double[numNodesOut];

            for (int o = 0; o < numNodesOut; o++)
            {
                biases[o] = rand() * 2;
            }

            for (int i = 0; i < numNodesIn; i++)
            {
                for (int o = 0; o < numNodesOut; o++)
                {
                    weights[i, o] = rand() * 2;
                }
            }
        }

        public Layer(Layer layer)
        {
            numNodesIn = layer.numNodesIn;
            numNodesOut = layer.numNodesOut;

            weights = new double[numNodesIn, numNodesOut];
            biases = new double[numNodesOut];

            for (int o = 0; o < numNodesOut; o++)
            {
                biases[o] = this.biases[o] + rand();
            }

            for (int i = 0; i < numNodesIn; i++)
            {
                for (int o = 0; o < numNodesOut; o++)
                {
                    weights[i, o] = this.weights[i, o] + rand();
                }
            }
        }

        private double rand()
        {
            return (Random.Shared.NextDouble() - 0.5) * 100;
        }

        public double[] CalculateOutputs(double[] inputs)
        {
            double[] values = new double[numNodesOut];

            for(int o = 0; o < numNodesOut; o++)
            {
                double value = biases[o];
                for(int i = 0; i < numNodesIn; i++)
                {
                    value += inputs[i] * weights[i, o];
                }
                values[o] = Function(value);
            }

            return values;
        }

        private double Function(double value)
        {
            return value > 0 ? 1 : 0;
        }
    }
}
