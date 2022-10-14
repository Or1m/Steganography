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
    }
}
