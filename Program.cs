using System.Drawing;
using System.Drawing.Imaging;

namespace noise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string url = "http://localhost:8000/api"; //replace this with your own web API URL
            WebApplication webapi = (WebApplication)new HostBuilder().Build();

            webapi.UseRouting();
            webapi.MapGet("/noise", NoiseGenerator.Generate);
            webapi.UseEndpoints(e => { });

            webapi.Run(url);
        }
    }

    public class NoiseGenerator
    {
        private static FastNoiseLite ftlNoise = new FastNoiseLite();

        public static Bitmap Generate()
        {
            //Output noise as a float array.
            ftlNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            float[] noiseData = new float[128 * 128];
            int index = 0;

            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    noiseData[index++] = ftlNoise.GetNoise(x, y);
                }
            }

            //For some reason, FastNoiseLite outputs noise as a float array, but a
            //bitmap has to be made out of a byte array. So we first have to
            //convert the float array into a byte array.
            //TODO: if float array is not normalized, normalize it first

            //a normalization function would look like this:
            //in float[] - out byte[]
            //float min = floatArray.Min();
            //float max = floatArray.Max();
            //byte[] byteArray = new byte[floatArray.Length];

            //for (int i = 0; i < floatArray.Length; i++)
            //{
            //byteArray[i] = (byte)((floatArray[i] - min) / (max - min) * 255);
            //}

            //return byteArray;

            byte[] btNoiseData = new byte[128 * 128];
            for (int i = 0; i < noiseData.Length; i ++)
            {
                btNoiseData[i] = (byte)(noiseData[i] * 255.0f);
            }

            //Now we make the bitmap out of the byte array.
            Bitmap bitmap = new Bitmap(128, 128);

            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    index = x * 128 + y;
                    Color color = Color.FromArgb(255, btNoiseData[index], btNoiseData[index], btNoiseData[index]);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}