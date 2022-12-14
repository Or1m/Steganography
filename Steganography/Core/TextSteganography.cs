using Steganography.Enums;
using System.Drawing;
using System.Text;

namespace Steganography.Core
{
    class TextSteganography : BaseSteganography
    {
        public override EType AllowedMsgType => EType.Text;


        public TextSteganography(Header header, Bitmap image) : base(header, image) { }
        public TextSteganography(Bitmap image, MainForm mainForm) : base(image, mainForm) { }

        public override string Reveal(Bitmap image, out byte[] bytes)
        {
            var result = ReadBits(image).ToASCII();

            bytes = Encoding.ASCII.GetBytes(result);
            return result;
        }

        /// <summary>
        /// Called before body of public method Hide to set right header parameters
        /// </summary>
        protected override bool InternalHide(string param)
        {
            if (!param.ToBitArray(out bits)) // Custom extension method to transform string into BitArray instance
                return false;

            header.NumOfBits = bits.Length;
            header.MsgType = AllowedMsgType;
            header.FileName = string.Empty;

            return true;
        }
    }
}
