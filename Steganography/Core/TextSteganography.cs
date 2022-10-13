using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Steganography.Core
{
    class TextSteganography : ISteganography
    {
        public void Hide(Bitmap image, string param, int bitsPerPixel = 3)
        {
            var bits = param.ToBitArray(); // Custom extension method to transform string into BitArray instance
            var bitsLength = bits.Length;
            var height = image.Height;
            var width = image.Width;

            int iterator = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A }; // Helper array initialized with all color components
                                                                                  // but only up to value of bitsPerPixel are used
                    for (int i = 0; i < bitsPerPixel && iterator < bitsLength; i++)
                    {
                        int mask = bits[iterator++] ? 255 : 254;
                        rgb[i] = rgb[i] & mask; // Set last bit to 0 or 1 by binary AND (&) based on value of bits[iterator++] 
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }

#if DEBUG
            for (int i = bitsLength - 1; i >= 0; i--)
                Console.Write(bits[i] ? 1 : 0);
            
            Console.WriteLine();
#endif
        }

        public string Reveal(Bitmap image, out byte[] bytes, int bitsPerPixel = 3)
        {
            List<string> arr = new List<string>();
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

                    for (int i = 0; i < bitsPerPixel && iterator < 32; i++) //TODO koncovy bit v hlavicke // && iterator < 32
                    {
                        if (rgb[i] % 2 == 1)
                            builder.Append(1);
                        else
                            builder.Append(0);

                        // Split to separate strings of length 8
                        if (builder.Length == 8)
                        {
                            arr.Add(builder.ToString().ReverseStr()); // Need to be reversed because of BitArray
                            builder.Clear();
                        }

                        iterator++;
                    }
                }
            }

            arr.Reverse(); // Need to be reversed because of BitArray
            var result = arr.ToASCII();

            bytes = Encoding.ASCII.GetBytes(result);
            return result;
        }
    }
}
