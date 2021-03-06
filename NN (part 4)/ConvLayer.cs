﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN__part_4_
{
    public class ConvLayer : Layer
    {
        public ConvLayer(Layer prevLayer, double[][] weights)
        {
            NumOfNeurons = prevLayer.NumOfNeurons - weights.Length + 1;
            NumOfPrevious = prevLayer.NumOfNeurons;
            Weights = weights;
            Neurons = new List<Neuron>();
            _NeuronType = NeuronType.Conv;
            NeuronInitialize(prevLayer, 1);
        }

        //private void NeuronInitialize(Layer prevLayer)
        //{
        //    List<double> inputs = new List<double>();
        //    for (int i = 0; i <= NumOfPrevious - Weights.Length; i++)
        //    {
        //        for(int j = 0; j <= NumOfPrevious - Weights.Length; j++)
        //        {
        //            OverlayMatrix(i, j, prevLayer);
        //        }
        //    }
        //}

        //private void OverlayMatrix(int shiftI, int shiftJ, Layer prevLayer)
        //{
        //    int wi = 0; int wj = 0;
        //    List<Neuron> lastNeurons = new List<Neuron>();
        //    List<double> weights = new List<double>();
        //    List<double> inputs = new List<double>();
        //    for (int i = 0 + shiftI; i < Weights.Length + shiftI; i++)
        //    {
        //        for (int j = 0 + shiftJ; j < Weights.Length + shiftJ; j++)
        //        {
        //            weights.Add(Weights[wi][wj]);
        //            inputs.Add(prevLayer.Neurons.Find(x => x.MapX == i && x.MapY == j).Output);
        //            lastNeurons.Add(prevLayer.Neurons.Find(x => x.MapX == i && x.MapY == j));
        //            wj++;
        //        }
        //        wj = 0; wi++;
        //    }
        //    Neurons.Add(new Neuron(inputs, weights, NeuronType.Conv, shiftI, shiftJ));
        //    Neurons.Last().LastNeurons = lastNeurons;
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
