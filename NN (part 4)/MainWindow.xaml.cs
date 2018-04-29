using NN_Part_1_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NN__part_4_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Layer layer = Test();
            System.Drawing.Image image = GetImage(layer);
            imageDigit.Source = GetImageStream(image);
        }
        private static Layer Test()
        {
            DigitImage[] digitImage = LoadImage(@"C:\Users\123\Desktop\IR\train-images.idx3-ubyte", @"C:\Users\123\Desktop\IR\train-labels.idx1-ubyte");
            double[][] weights = new double[][]
            {new double[] {0, 1, 0 },
            new double[] {1, -4, 1 },
            new double[] {0, 1, 0} };
            double[][] weights2 = new double[][]
            {new double[] {0, 0, 1 , 0, 0},
            new double[] {0, 0, 0, 0, 0 },
            new double[] {1, 0, -4, 0, 1},
            new double[] {0, 0, 0, 0, 0},
            new double[] {0, 0, 1, 0, 0}};
            InputLayer inputLayer = new InputLayer(28, digitImage[0].Pixels);
            ConvLayer convLayer = new ConvLayer(inputLayer, weights2);
            MaxPoolingLayer poolingLayer = new MaxPoolingLayer(convLayer);
            ConvLayer convLayer2 = new ConvLayer(poolingLayer, weights2);
            MaxPoolingLayer poolingLayer2 = new MaxPoolingLayer(convLayer2);
            HiddenLayer hiddenLayer = new HiddenLayer(poolingLayer2);
            return poolingLayer2;
        }

        private static System.Drawing.Image GetImage(Layer layer)
        {
            byte[][] pixels = new byte[layer.NumOfNeurons][];
            for(int i = 0; i < layer.NumOfNeurons; i++)
            {
                pixels[i] = new byte[layer.NumOfNeurons];
            }
            for (int i = 0; i < layer.NumOfNeurons; i++)
            {
                for(int j = 0; j < layer.NumOfNeurons; j++)
                {
                    pixels[i][j] = (byte)layer.Neurons.Find(x => x.MapX == i && x.MapY == j).Output;
                }
            }
            DigitImage digitImage = new DigitImage(layer.NumOfNeurons, layer.NumOfNeurons, pixels, 4);
            Bitmap bitmap = GetBitmap(digitImage, 1);
            System.Drawing.Image image = bitmap;
            return image;
        }

        public static DigitImage[] LoadImage(string pixelFile, string labelFile)
        {
            int numImages = 60000;
            DigitImage[] digitImages = new DigitImage[numImages];
            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new byte[28];
            }
            FileStream ifsPixels = new FileStream(pixelFile, FileMode.Open);
            FileStream ifsLabels = new FileStream(labelFile, FileMode.Open);
            BinaryReader brImages = new BinaryReader(ifsPixels);
            BinaryReader brLabels = new BinaryReader(ifsLabels);
            int magic1 = brImages.ReadInt32(); // обратный порядок байтов
            magic1 = ReverseBytes(magic1); // преобразуем в формат Intel
            int imageCount = brImages.ReadInt32();
            imageCount = ReverseBytes(imageCount);
            int numRows = brImages.ReadInt32();
            numRows = ReverseBytes(numRows);
            int numCols = brImages.ReadInt32();
            numCols = ReverseBytes(numCols);
            int magic2 = brLabels.ReadInt32();
            magic2 = ReverseBytes(magic2);
            int numLabels = brLabels.ReadInt32();
            numLabels = ReverseBytes(numLabels);
            for (int i = 0; i < numImages; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    for (int k = 0; k < 28; k++)
                    {
                        byte b = brImages.ReadByte();
                        pixels[j][k] = b;
                    }
                }
                byte lb = brLabels.ReadByte();
                DigitImage digitImage = new DigitImage(28, 28, pixels, lb);
                digitImages[i] = digitImage;
            }
            ifsPixels.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();
            return digitImages;
        }
        public static int ReverseBytes(int bytes)
        {
            byte[] intAsByte = BitConverter.GetBytes(bytes);
            Array.Reverse(intAsByte);
            return BitConverter.ToInt32(intAsByte, 0);
        }

        public static Bitmap GetBitmap(DigitImage digitImage, int mag)
        {
            int width = digitImage.Width * mag;
            int height = digitImage.Height * mag;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i < digitImage.Height; i++)
            {
                for (int j = 0; j < digitImage.Width; j++)
                {
                    int pixelColor = 255 - digitImage.Pixels[i][j];
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    SolidBrush solidBrush = new SolidBrush(color);
                    graphics.FillRectangle(solidBrush, j * mag, i * mag, mag, mag);
                }
            }
            return bitmap;
        }
        public static BitmapSource GetImageStream(System.Drawing.Image myImage)
        {
            var bitmap = new Bitmap(myImage);
            IntPtr bmpPt = bitmap.GetHbitmap();
            BitmapSource bitmapSource =
             System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                   bmpPt,
                   IntPtr.Zero,
                   Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());

            //freeze bitmapSource and clear memory to avoid memory leaks
            bitmapSource.Freeze();
            DeleteObject(bmpPt);

            return bitmapSource;
        }
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);
    }
}
