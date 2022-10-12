using System;
using System.Text;

namespace Steganography
{
    public static class Extensions
    {
        public static string ToBinary(this char value, byte width = 8)
        {
            return Convert.ToString(value, 2).PadLeft(width, '0');
        }

        public static string ToBinary(this string value, byte width = 8)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
                builder.Append(value[i].ToBinary(width));

            return builder.ToString();
        }
    }
}
