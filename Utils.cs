using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace RemocaoFundo
{
    public static class Utils
    {
        public static DenseTensor<float> ImageToTensor(Bitmap img)
        {
            var tensor = new DenseTensor<float>(new[] { 1, 3, img.Height, img.Width });

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var pixel = img.GetPixel(x, y);
                    tensor[0, 0, y, x] = pixel.R / 255f;
                    tensor[0, 1, y, x] = pixel.G / 255f;
                    tensor[0, 2, y, x] = pixel.B / 255f;
                }
            }

            return tensor;
        }

        public static Bitmap MaskToBitmap(float[] output, int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    byte alpha = (byte)(output[index] * 255);
                    bmp.SetPixel(x, y, Color.FromArgb(alpha, 255, 255, 255));
                }
            }

            return bmp;
        }

        public static byte[] AplicarMascara(Bitmap original, Bitmap mascara)
        {
            Bitmap resultado = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    var cor = original.GetPixel(x, y);
                    var alpha = mascara.GetPixel(x, y).A;
                    resultado.SetPixel(x, y, Color.FromArgb(alpha, cor.R, cor.G, cor.B));
                }
            }

            using var ms = new MemoryStream();
            resultado.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}