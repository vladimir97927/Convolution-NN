using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN__part_4_
{
    public abstract class Layer
    {
        public int NumOfNeurons { get; set; }
        public int NumOfPrevious { get; set; }
        public List<Neuron> Neurons { get; set; }
        public double[][] Weights { get; set; }
        public NeuronType _NeuronType;
        //public List<double> Data
        //{
        //    set
        //    {
        //        for(int i = 0; i < Neurons.Count; i++)
        //        {
        //            Neurons[i].Inputs = value;
        //        }
        //    }
        //}
        protected void NeuronInitialize(byte[][] pixels) //для входного слоя
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    List<double> inputs = new List<double>() { pixels[i][j] };
                    Neurons.Add(new Neuron(inputs, null, _NeuronType, i, j));
                }
            }
        }

        protected void NeuronInitialize(Layer prevLayer, int step) // для других слоев
        {
            List<double> inputs = new List<double>();
            for (int i = 0; i <= NumOfPrevious - Weights.Length; i += step)
            {
                for (int j = 0; j <= NumOfPrevious - Weights.Length; j += step)
                {
                    OverlayMatrix(i, j, prevLayer, step);
                }
            }
        }

        protected void OverlayMatrix(int shiftI, int shiftJ, Layer prevLayer, int step)
        {
            int wi = 0; int wj = 0;
            List<Neuron> lastNeurons = new List<Neuron>();
            List<double> weights = new List<double>();
            List<double> inputs = new List<double>();
            for (int i = 0 + shiftI; i < Weights.Length + shiftI; i++)
            {
                for (int j = 0 + shiftJ; j < Weights.Length + shiftJ; j++)
                {
                    weights.Add(Weights[wi][wj]);
                    inputs.Add(prevLayer.Neurons.Find(x => x.MapX == i && x.MapY == j).Output);
                    lastNeurons.Add(prevLayer.Neurons.Find(x => x.MapX == i && x.MapY == j));
                    wj++;
                }
                wj = 0; wi++;
            }
            Neurons.Add(new Neuron(inputs, weights, _NeuronType, shiftI / step, shiftJ / step));
            Neurons.Last().LastNeurons = lastNeurons;
        }

        abstract public void Recognize(Layer nextLayer);
        abstract public double[] BackwardPass(double[] stuff);
    }
}
