using Steganography.Enums;
using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace Steganography.Core
{
    class FileSteganography : BaseSteganography
    {
        public FileSteganography(Header header, Bitmap image) : base(header, image) { }
        public FileSteganography(Bitmap image) : base(image) { }

        public override bool Hide(string param)
        {
            var bytes = File.ReadAllBytes(param);
            BitArray bits = new BitArray(bytes);

            header.NumOfBits = bits.Length;
            header.MsgType = EType.File;
            header.FileName = Path.GetFileName(param);

            if (!Header.ToBitArray(header, out var headerBits))
                return false;

            var maxLength = headerBits.Length + bits.Length;

            int iterator = 0;
            WriteHeader(headerBits, ref iterator);
            WriteBits(bits, maxLength, ref iterator);

            return true;
        }
        public override string Reveal(Bitmap image, out byte[] bytes)
        {
            var arr = ReadBits(image);
            int length = arr.Count;

            bytes = new byte[length];

            for (int i = 0; i < length; i++)
                bytes[i] = Convert.ToByte(arr[i].ReverseStr(), 2);

            return header.FileName;
        }
    }
}
