using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steganography
{
    public static class Extensions
    {
        public static BitArray ToBitArray(this string value)
        {
            var length = value.Length;
            var bytes = new byte[length];

            for (int i = 0; i < length; i++)
                bytes[i] = (byte)value[i];

            Array.Reverse(bytes);

            return new BitArray(bytes);
        }
        public static string ToASCII(this List<string> list)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var byteStr in list)
            {
                byte val = Convert.ToByte(byteStr, 2);
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
