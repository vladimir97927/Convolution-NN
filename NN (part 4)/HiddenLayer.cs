using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN__part_4_
{
    public class HiddenLayer : Layer
    {
        public HiddenLayer(Layer prevLayer)
        {
            NumOfNeurons = prevLayer.NumOfNeurons;
            NumOfPrevious = prevLayer.NumOfNeurons;
            Neurons = new List<Neuron>();
            _NeuronType = NeuronType.Hidden;
            NeuronInitialize(prevLayer);
        }

        private void NeuronInitialize(Layer prevLayer)
        {
            for (int k = 0; k < prevLayer.Neurons.Count; k++)
            {
                AddWeights(prevLayer, k);
            }
        }

        private void AddWeights(Layer prevLayer, int index)
        {
            List<Neuron> lastNeurons = new List<Neuron>();
            List<double> weights = new List<double>();
            List<double> inputs = new List<double>();
            for (int i = 0; i < NumOfPrevious; i++)
            {
                for (int j = 0; j < NumOfPrevious; j++)
                {
                    weights.Add(0.5);
                    inputs.Add(prevLayer.Neurons.Find(x => x.MapX == i && x.MapY == j).Output);
                    lastNeurons.Add(prevLayer.Neurons.Find(x => x.MapX == i && x.MapY == j));
                }
            }
            Neurons.Add(new Neuron(inputs, weights, _NeuronType, 0, index));
            Neurons.Last().LastNeurons = lastNeurons;
        }

        public override double[] BackwardPass(double[] stuff)
        {
            throw new NotImplementedException();
        }

        public override void Recognize(Layer nextLayer)
        {
            throw new NotImplementedException();
        }
    }
}
