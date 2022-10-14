using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Steganography
{
    public static class Utils
    {
        public static void PackInt(List<byte> bytes, int value)
        {
            bytes.Add((byte)(value >> 24));
            bytes.Add((byte)(value >> 16));
            bytes.Add((byte)(value >> 8));
            bytes.Add((byte)(value >> 0));
        }
        public static int UnpackInt(List<string> list, ref int idx)
        {
            byte b1 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 24);
            byte b2 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 16);
            byte b3 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 8);
            byte b4 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 0);

            return b1 | b2 | b3 | b4;
        }
    }
}
