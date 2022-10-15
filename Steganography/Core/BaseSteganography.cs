using Steganography.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Steganography.Core
{
    abstract class BaseSteganography : ISteganography
    {
        protected readonly Bitmap image;
        protected readonly Header header;

        protected BitArray bits;
        protected (int x, int y) headerEndCoords;

        public abstract EType AllowedMsgType { get; }


        public BaseSteganography(Header header, Bitmap image) // Constructor for hide
        {
            this.header = header;
            this.image = image;
        }
        public BaseSteganography(Bitmap image, MainForm mainForm) // Constructor for reveal
        {
            this.image = image;

            ReadHeader(image, out var list);

            if (!Header.FromBinaryList(list, out var header))
                throw new Exception("Failed to parse header");

            this.header = header;

            if (AllowedMsgType != header.MsgType)
                mainForm.HandleTypeMissmatch(GetType().Name, header);
        }

        public bool Hide(string param)
        {
            if (!InternalHide(param))
                return false;

            if (!Header.ToBitArray(header, out var headerBits))
                return false;

            var maxLength = headerBits.Length + bits.Length;

            int iterator = 0;
            WriteHeader(headerBits, ref iterator);
            WriteBits(maxLength, ref iterator);

            return true;
        }
        public abstract string Reveal(Bitmap image, out byte[] bytes);

        protected void WriteHeader(BitArray headerBits, ref int iterator)
        {
            headerEndCoords = (-1, -1);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A }; // Helper array initialized with all color components
                                                                                  // but only up to value of bitsPerPixel are used
                    for (int i = 0; i < (byte)header.ValidPixelChannels; i++)
                    {
                        rgb[i] = rgb[i] & 0b11111110; // Set all LSBs to 0

                        if (headerBits[iterator])
                            rgb[i] = rgb[i] | 0b00000001; // Set LSB to 1 if current bit is 1

                        if (iterator++ >= headerBits.Length - 1) // Header was written, save coords and return
                        {
                            headerEndCoords = (x, y);
                            return;
                        }
                    }

                    ApplyColorToPixel(y, x, rgb);
                }
            }
        }
        protected void ReadHeader(Bitmap image, out List<string> list)
        {
            headerEndCoords = (-1, -1);
            list = new List<string>();
            StringBuilder builder = new StringBuilder();

            int iterator = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    int[] rgb = new int[] { pixel.R, pixel.G, pixel.B, pixel.A };

                    for (int i = 0; i < (byte)Header.HeaderChannels; i++) // Header always uses RGB
                    {
                        builder.Append(rgb[i] % 2); // Append 1 if last bit is 1, append 0 otherwise

                        if (builder.Length == Header.BitsPerChar) // Add string to array after length reaches value of MainForm.BitsPerChar 
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
        }

        /// <summary>
        /// Write bits of message in form found in header
        /// </summary>
        protected void WriteBits(int maxLength, ref int iterator)
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
                        rgb[i] = rgb[i] & 0b11111110; // Set LSB to 0

                        if (bits[ii - Header.Size])
                            rgb[i] = rgb[i] | 0b00000001; // Set LSB to 1 if current bit is 1
                    }

                    ApplyColorToPixel(y, x, rgb);
                }
            }
        }
        /// <summary>
        /// Read bits of message in form found in header
        /// </summary>
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

                        if (builder.Length == Header.BitsPerChar) // Add string to array after length reaches value of MainForm.BitsPerChar 
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
        
        protected abstract bool InternalHide(string param);

        private void ApplyColorToPixel(int y, int x, int[] rgb)
        {
            Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
            image.SetPixel(x, y, Color.FromArgb(rgb[3], color));
        }
    }
}
