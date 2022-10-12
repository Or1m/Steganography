using System;
using System.Drawing;

namespace Steganography
{
    class TextSteganography : ISteganography
    {
        public void Hide(Bitmap image, string param, int numOfChannels = 3)
        {
            var binary = param.ToBinary();
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

        public string UnHide(Bitmap image, out byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
