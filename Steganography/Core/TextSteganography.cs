using System;
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

        private (int x, int y) headerEndCoords;

        public TextSteganography(Header header, Bitmap image)
        {
            this.header = header;
            this.image = image;
        }
        public TextSteganography(Bitmap image)
        {
            this.image = image;

            ReadHeader(image, out var list);

            if (!Header.FromBinaryList(list, out var header))
                throw new Exception("Failed to parse header");

            this.header = header;
        }

        public bool Hide(string param)
        {
            if (!param.ToBitArray(out var bits)) // Custom extension method to transform string into BitArray instance
                return false;

            header.NumOfBits = bits.Length;

            if (!Header.ToBitArray(header, out var headerBits))
                return false;

            var maxLength = headerBits.Length + bits.Length;

            int iterator = 0;
            WriteHeader(headerBits, ref iterator);
            WriteBits(bits, maxLength, ref iterator);

            return true;
        }
        public string Reveal(Bitmap image, out byte[] bytes)
        {
            List<string> arr = new List<string>();
            StringBuilder builder = new StringBuilder();

            int iterator = 0;
            for (int y = headerEndCoords.y + header.FirstY; y < image.Height; y += header.StepY)
            {
                for (int x = headerEndCoords.x + header.FirstX; x < image.Width; x += header.StepX)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A };

                    for (int i = 0; i < (byte)header.ValidPixelChannels && iterator < header.NumOfBits; i++)
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

        private void WriteHeader(BitArray headerBits, ref int iterator)
        {
            int y = 0;
            for (; y < image.Height; y++)
            {
                int x = 0;
                for (; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A }; // Helper array initialized with all color components
                                                                                  // but only up to value of bitsPerPixel are used
                    for (int i = 0; i < (byte)header.ValidPixelChannels; i++)
                    {
                        int mask = headerBits[iterator] ? 255 : 254;
                        rgb[i] = rgb[i] & mask; // Set last bit to 0 or 1 by binary AND (&) based on value of bits[iterator++] 

                        if (iterator++ >= headerBits.Length - 1)
                        {
                            headerEndCoords = (x, y);
                            return;
                        }
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }

            headerEndCoords = (-1, -1);
        }
        private void WriteBits(BitArray bits, int maxLength, ref int iterator)
        {
            for (int y = headerEndCoords.y + header.FirstY; y < image.Height; y += header.StepY)
            {
                for (int x = headerEndCoords.x + header.FirstX; x < image.Width; x += header.StepX)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A }; // Helper array initialized with all color components
                                                                                  // but only up to value of bitsPerPixel are used
                    for (int i = 0; i < (byte)header.ValidPixelChannels && iterator < maxLength; i++)
                    {
                        int ii = iterator++;
                        int mask = bits[ii - Header.Size] ? 255 : 254;
                        rgb[i] = rgb[i] & mask; // Set last bit to 0 or 1 by binary AND (&) based on value of bits[iterator++] 
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }
        }

        private void ReadHeader(Bitmap image, out List<string> list)
        {
            list = new List<string>();
            StringBuilder builder = new StringBuilder();

            int y = 0, iterator = 0;
            for (; y < image.Height; y++)
            {
                int x = 0;
                for (; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A };

                    for (int i = 0; i < (byte)Header.HeaderChannels; i++)
                    {
                        builder.Append(rgb[i] % 2); // Append 1 if last bit is 1, append 0 otherwise

                        if (builder.Length == MainForm.BitsPerChar) // Add string to array after length reaches value of MainForm.BitsPerChar 
                        {
                            list.Add(builder.ToString());
                            builder.Clear();
                        }

                        if (iterator++ >= Header.Size)
                        {
                            headerEndCoords = (x, y);
                            return;
                        }
                    }
                }
            }

            headerEndCoords = (-1, -1);
        }
    }
}
