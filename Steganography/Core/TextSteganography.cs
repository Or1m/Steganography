using System.Drawing;
using System.Text;

namespace Steganography.Core
{
    class TextSteganography : BaseSteganography
    {
        public TextSteganography(Header header, Bitmap image) : base(header, image) { }
        public TextSteganography(Bitmap image) : base(image) { }

        public override bool Hide(string param)
        {
            if (!param.ToBitArray(out var bits)) // Custom extension method to transform string into BitArray instance
                return false;

            header.NumOfBits = bits.Length;

            if (!Header.ToBitArray(header, out var headerBits))
                return false;

            var maxLength = headerBits.Length + bits.Length;

            int iterator = 0;
            WriteHeader(headerBits, ref iterator);
            WriteBits(bits, maxLength, ref iterator);

            foreach (bool item in headerBits) //TODO
            {
                System.Console.Write(item ? 1 : 0);
            }
            System.Console.WriteLine();

            return true;
        }
        public override string Reveal(Bitmap image, out byte[] bytes)
        {
            var result = ReadBits(image).ToASCII();

            bytes = Encoding.ASCII.GetBytes(result);
            return result;
        }
    }
}
