using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN__part_4_
{
    public class Neuron
    {
        public Neuron(List<double> inputs, List<double> weights, NeuronType neuronType, int mapX, int mapY)
        {
            Inputs = inputs;
            Weight = weights;
            _NeuronType = neuronType;
            MapX = mapX;
            MapY = mapY;
            LastNeurons = new List<Neuron>();
        }
        NeuronType _NeuronType;
        public int MapX { get; set; }
        public int MapY { get; set; }
        public List<double> Weight { get; set; }
        public List<double> Inputs { get; set; }
        public List<Neuron> LastNeurons { get; set; }
        public double Output { get => Activation(); }

        private double Activation()
        {
            double sum = 0; double result = 0;
            if (_NeuronType == NeuronType.MaxPooling)
            {
                double max = 0;
                for(int i = 0; i < Inputs.Count; i++)
                {
                    if(Inputs[i] > max)
                    {
                        max = Inputs[i];
                    }
                }
                result = max;
            }
            else
            {
                if (Weight != null)
                {
                    for (int i = 0; i < Weight.Count; i++)
                    {
                        sum += Weight[i] * Inputs[i];
                    }
                }
                else
                {
                    sum = Inputs[0];
                }
                if (_NeuronType == NeuronType.Conv || _NeuronType == NeuronType.Input)
                {
                    result = ActivationFunction.ReLu(sum);
                }
            }
            return result;
        }
    }
}
