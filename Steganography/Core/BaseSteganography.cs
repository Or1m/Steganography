using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Steganography.Core
{
    abstract class BaseSteganography : ISteganography
    {
        protected readonly Header header;
        protected readonly Bitmap image;

        protected (int x, int y) headerEndCoords;


        public BaseSteganography(Header header, Bitmap image) // Constructor for hide
        {
            this.header = header;
            this.image = image;
        }
        public BaseSteganography(Bitmap image) // Constructor for reveal
        {
            this.image = image;

            ReadHeader(image, out var list);

            if (!Header.FromBinaryList(list, out var header))
                throw new Exception("Failed to parse header");

            this.header = header;

            //TODO
            foreach (var item in list)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }
        
        protected void WriteHeader(BitArray headerBits, ref int iterator)
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
                        rgb[i] = rgb[i] & 0b11111110; // Set all LSBs to 0

                        if (headerBits[iterator])
                            rgb[i] = rgb[i] | 0b00000001; // Set LSB to 1 if bit is 1

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
        protected void ReadHeader(Bitmap image, out List<string> list)
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

        public abstract bool Hide(string param);
        public abstract string Reveal(Bitmap image, out byte[] bytes);

        protected void WriteBits(BitArray bits, int maxLength, ref int iterator)
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
                        rgb[i] = rgb[i] & 0b11111110;

                        if (bits[ii - Header.Size])
                            rgb[i] = rgb[i] | 0b00000001; // Set last bit to 0 or 1 by binary AND (&) based on value of bits[iterator++] 
                    }

                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
                }
            }
        }
        protected List<string> ReadBits(Bitmap image)
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

            return arr;
        }
    }
}
