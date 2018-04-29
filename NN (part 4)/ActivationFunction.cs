using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN__part_4_
{
    public static class ActivationFunction
    {
        public static double ReLu(double x)
        {
            if (x < 0)
                return 0;
            else
                return x;
        }
    }
}
