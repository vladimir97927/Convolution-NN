using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Part_1_
{
    public class DigitImage
    {
        public DigitImage(int width, int height, byte[][] pixels, byte label)
        {
            Width = width;
            Height = height;
            Pixels = new byte[height][];
            for (int i = 0; i < Pixels.Length; ++i)
                Pixels[i] = new byte[Width];
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < Width; ++j)
                    Pixels[i][j] = pixels[i][j];
            Label = label;
        }
        public int Width { get; set; } //28 по умолчанию
        public int Height { get; set; } //28 по умолчанию
        public byte[][] Pixels { get; set; } // 0 - белый, 255 - черный
        public byte Label { get; set; } // метка числа 0 - 9

    }
}
