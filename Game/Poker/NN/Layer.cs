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

        private Layer()
        {

        }

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
                biases[o] = layer.biases[o] + rand();
            }

            for (int i = 0; i < numNodesIn; i++)
            {
                for (int o = 0; o < numNodesOut; o++)
                {
                    weights[i, o] = layer.weights[i, o] + rand();
                }
            }
        }

        public string saveToFile()
        {
            string l = $"{numNodesIn} {numNodesOut}\n";
            string b = String.Join(" ", biases) + "\n";
            string w = "";
            for(int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    w += weights[i, j] + " ";
                }

                w = w.TrimEnd() + "\n";
            }
            w = w.TrimEnd();

            return l + b + w;
        }

        public static Layer loadFromFile(string content)
        {
            string[] s = content.Split("\n");

            Layer l = new Layer();

            l.numNodesIn = int.Parse(s[0].Split(" ")[0]);
            l.numNodesOut = int.Parse(s[0].Split(" ")[1]);

            l.weights = new double[l.numNodesIn, l.numNodesOut];
            l.biases = new double[l.numNodesOut];

            int i = 0;
            foreach(string _ in s[1].Split(" "))
            {
                l.biases[i++] = double.Parse(_);
            }

            for(i = 0; i < l.numNodesIn; i++)
            {
                for(int j = 0; j < l.numNodesOut; j++)
                {
                    l.weights[i, j] = double.Parse(s[i + 2].Split(" ")[j]);
                }
            }


            return l;
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
