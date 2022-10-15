using Steganography.Enums;
using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace Steganography.Core
{
    class FileSteganography : BaseSteganography
    {
        public override EType AllowedMsgType => EType.File;


        public FileSteganography(Header header, Bitmap image) : base(header, image) { }
        public FileSteganography(Bitmap image, MainForm mainForm) : base(image, mainForm) { }

        public override string Reveal(Bitmap image, out byte[] bytes)
        {
            var arr = ReadBits(image);
            int length = arr.Count;

            bytes = new byte[length];

            for (int i = 0; i < length; i++)
                bytes[i] = Convert.ToByte(arr[i].ReverseStr(), 2);

            return header.FileName;
        }

        /// <summary>
        /// Called before body of public method Hide to set right header parameters
        /// </summary>
        protected override bool InternalHide(string param)
        {
            var bytes = File.ReadAllBytes(param);
            bits = new BitArray(bytes);

            header.NumOfBits = bits.Length;
            header.MsgType = AllowedMsgType;
            header.FileName = Path.GetFileName(param);

            return true;
        }
    }
}
