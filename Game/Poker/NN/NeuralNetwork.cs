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

        private NeuralNetwork() { }

        public void save(string prefix)
        {
            for(int i = 0; i < layers.Length; i++)
            {
                layers[i].saveToFile($"{prefix}{i}.layer");
            }
        }

        public static NeuralNetwork load(string prefix)
        {
            NeuralNetwork n = new NeuralNetwork();
            int layers = 0;
            foreach(string f in Directory.GetFiles("."))
            {
                if (!f.StartsWith(prefix)) continue;

                layers++;
            }

            n.layers = new Layer[layers];
            
            foreach (string f in Directory.GetFiles("."))
            {
                if (!f.StartsWith(prefix)) continue;

                string s = f.Substring(prefix.Length, f.LastIndexOf('.'));
                n.layers[int.Parse(s)] = Layer.loadFromFile(f);
            }

            return n;
        }

        public double[] CalculateOutputs(double[] inputs)
        {
            foreach(Layer layer in layers)
            {
                inputs = layer.CalculateOutputs(inputs);
            }

            return inputs;
        }

        public static int IndexOfHighestNode(double[] outputs)
        {
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
