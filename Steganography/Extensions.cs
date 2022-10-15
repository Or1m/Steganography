using Steganography.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Steganography
{
    public static class Extensions
    {
        /// <summary>
        /// </summary>
        /// <param name="value">ASCII string message</param>
        /// <returns>BitArray created from byte representation of each character</returns>
        public static bool ToBitArray(this string value, out BitArray bits)
        {
            var length = value.Length;
            var bytes = new byte[length];
            bits = null;

            for (int i = 0; i < length; i++)
            {
                var byteVal = (byte)value[i];

                if (byteVal < 9 || byteVal > 126) // +- valid Alphanumeric chars and some special chars
                    return false;

                bytes[i] = byteVal;
            }

            bits = new BitArray(bytes);
            return true;
        }
        /// <summary>
        /// </summary>
        /// <param name="list">List of binary strings of length <see cref="MainForm.BitsPerChar"/></param>
        /// <returns>Decoded ASCII string</returns>
        public static string ToASCII(this List<string> list)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var byteStr in list)
            {
                byte val = Convert.ToByte(byteStr.ReverseStr(), 2); // Need to be reversed first because of endianity
                builder.Append((char)val);
            }

            return builder.ToString();
        }
        
        public static string ReverseStr(this string str)
        {
            return new string(str.Reverse().ToArray());
        }

        public static int AvailableBits(this Bitmap image, Header header)
        {
            var rawPixelCount = (image.Width / header.StepX) * (image.Height / header.StepY);
            var availablePixelCount = rawPixelCount - header.FirstX - (header.FirstY * image.Width);
            return availablePixelCount * (byte)header.ValidPixelChannels - Header.Size;
        }
    }
}
