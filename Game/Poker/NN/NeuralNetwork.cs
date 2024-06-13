using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.Game.Poker.NN
{
    internal class NeuralNetwork
    {
        private Layer[] layers;

        public NeuralNetwork(params int[] sizes)
        {
            layers = new Layer[sizes.Length - 1];
            for(int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(sizes[i], sizes[i + 1]);
            }
        }

        public NeuralNetwork(NeuralNetwork clone)
        {
            layers = new Layer[clone.layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(clone.layers[i]);
            }
        }

        private double[] CalculateOutputs(double[] inputs)
        {
            foreach(Layer layer in layers)
            {
                inputs = layer.CalculateOutputs(inputs);
            }

            return inputs;
        }

        public int IndexOfHighestNode(double[] inputs)
        {
            double[] outputs = CalculateOutputs(inputs);

            double maxv = 0;
            int maxi = -1;

            for(int i = 0; i < outputs.Length;i++)
            {
                if(maxi == -1 || outputs[i] > maxv)
                {
                    maxi = i;
                    maxv = outputs[i];
                }
            }

            return maxi;
        }
    }
}
