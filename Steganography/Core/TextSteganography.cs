using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Steganography.Core
{
    class TextSteganography : ISteganography
    {
        private readonly Header header;
        private readonly Bitmap image;

        public TextSteganography(Header header, Bitmap image)
        {
            this.header = header;
            this.image = image;
        }
        public TextSteganography(Bitmap image)
        {
            this.image = image;
            this.header = Header.FromImage(image);
        }

        public bool Hide(string param)
        {
            if (!param.ToBitArray(out var bits)) // Custom extension method to transform string into BitArray instance
                return false;

            header.NumOfBits = bits.Length;

            if (!header.ToBitArray(out var headerBits)) // Custom extension method to bitpack Header
                return false;

            var maxLength = headerBits.Length + bits.Length;

            int iterator = 0;
            var (x, y) = WriteHeader(headerBits, ref iterator);
            WriteBits(bits, x, y, maxLength, ref iterator);

            return true;
        }

        public string Reveal(Bitmap image, out byte[] bytes)
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

                    for (int i = 0; i < (byte)header.ValidPixelChannels && iterator < 32; i++) //TODO koncovy bit v hlavicke // && iterator < 32
                    {
                        builder.Append(rgb[i] % 2); // Append 1 if last bit is 1, append 0 otherwise

                        if (builder.Length == MainForm.BitsPerChar) // Add string to array after length reaches value of MainForm.BitsPerChar 
                        {
                            arr.Add(builder.ToString());
                            builder.Clear();
                        }

                        iterator++;
                    }
                }
            }

            var result = arr.ToASCII();
            bytes = Encoding.ASCII.GetBytes(result);
            return result;
        }

        private (int x, int y) WriteHeader(BitArray headerBits, ref int iterator)
        {
            int x = 0, y = 0;

            for (; y < image.Height; y++)
            {
                for (; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A }; // Helper array initialized with all color components
                                                                                  // but only up to value of bitsPerPixel are used
                    for (int i = 0; i < (byte)header.ValidPixelChannels && iterator < headerBits.Length; i++)
                    {
                        int mask = headerBits[iterator++] ? 255 : 254;
                        rgb[i] = rgb[i] & mask; // Set last bit to 0 or 1 by binary AND (&) based on value of bits[iterator++] 
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }

            return (x, y);
        }
        private void WriteBits(BitArray bits, int lastX, int lastY, int maxLength, ref int iterator)
        {
            for (int y = lastY + header.FirstY; y < image.Height; y += header.StepY)
            {
                for (int x = lastX + header.FirstX; x < image.Width; x += header.StepX)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A }; // Helper array initialized with all color components
                                                                                  // but only up to value of bitsPerPixel are used
                    for (int i = 0; i < (byte)header.ValidPixelChannels && iterator < maxLength; i++)
                    {
                        int mask = bits[iterator++] ? 255 : 254;
                        rgb[i] = rgb[i] & mask; // Set last bit to 0 or 1 by binary AND (&) based on value of bits[iterator++] 
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }
        }
    }
}
