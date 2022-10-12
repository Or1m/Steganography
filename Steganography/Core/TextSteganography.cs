using System;
using System.Drawing;
using System.Text;

namespace Steganography
{
    class TextSteganography : ISteganography
    {
        public void Hide(Bitmap image, string param, int numOfChannels = 3)
        {
            var binary = param.ToBinary(); Console.WriteLine(binary);
            int iterator = 0;
            
            var height = image.Height;
            var width = image.Width;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A };

                    for (int i = 0; i < numOfChannels && iterator < binary.Length; i++)
                    {
                        int mask = (binary[iterator++] == '1') ? 255 : 254;
                        rgb[i] = rgb[i] & mask;
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }
        }

        public string Reveal(Bitmap image, out byte[] bytes, int numOfChannels = 3)
        {
            StringBuilder builder = new StringBuilder();
            var height = image.Height;
            var width = image.Width;
            int iterator = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A };

                    for (int i = 0; i < numOfChannels; i++) //TODO koncovy bit v hlavicke // && iterator < 32
                    {
                        if (rgb[i] % 2 == 1)
                            builder.Append("1");
                        else
                            builder.Append("0");

                        iterator++;
                    }
                }
            }

            var result = builder.ToString();
            bytes = Encoding.ASCII.GetBytes(result);
            return result;
        }
    }
}
