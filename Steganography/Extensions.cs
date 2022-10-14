using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steganography
{
    public static class Extensions
    {
        /// <summary>
        /// 
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

                if (byteVal < 32 || byteVal > 126)
                    return false;

                bytes[i] = byteVal;
            }

            bits = new BitArray(bytes);
            return true;
        }
        /// <summary>
        /// 
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

        // Old string based solution
        //public static string ToBinary(this char value, int bitsPerChar = 8)
        //{
        //    return Convert.ToString(value, 2).PadLeft(width, '0');
        //}

        //public static string ToBinary(this string value, int bitsPerChar = 8)
        //{
        //    StringBuilder builder = new StringBuilder();

        //    for (int i = 0; i < value.Length; i++)
        //        builder.Append(value[i].ToBinary(width));

        //    return builder.ToString();
        //}
    }
}
