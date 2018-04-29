using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN__part_4_
{
    public class InputLayer : Layer
    {
        public InputLayer(int neuronCount, byte [][] pixels)
        {
            NumOfNeurons = neuronCount;
            Neurons = new List<Neuron>();
            _NeuronType = NeuronType.Input;
            NeuronInitialize(pixels);
        }
        //private void NeuronInitialize(byte[][] pixels)
        //{
        //    for(int i = 0; i < pixels.Length; i++)
        //    {
        //        for(int j = 0; j < pixels[i].Length; j++)
        //        {
        //            List<double> inputs = new List<double>() { pixels[i][j] };
        //            Neurons.Add(new Neuron(inputs, null, NeuronType.Input, i, j));
        //        }
        //    }
        //}
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
