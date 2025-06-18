using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;
using System.Drawing;

namespace RemocaoFundo
{
    public class BackgroundRemover
    {
        private readonly InferenceSession _session;

        public BackgroundRemover(string modeloCaminho)
        {
            _session = new InferenceSession(modeloCaminho);
        }

        public byte[] RemoverFundo(byte[] imagemBytes)
        {
            using var ms = new MemoryStream(imagemBytes);
            using var original = new Bitmap(ms);

            var resized = new Bitmap(original, new Size(320, 320));
            var inputTensor = Utils.ImageToTensor(resized);

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", inputTensor)
            };

            using var resultados = _session.Run(inputs);
            var output = resultados.First().AsEnumerable<float>().ToArray();

            var mask = Utils.MaskToBitmap(output, 320, 320);
            var maskResized = new Bitmap(mask, original.Size);

            return Utils.AplicarMascara(original, maskResized);
        }
    }
}