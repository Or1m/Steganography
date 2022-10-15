using System.Drawing;
using System.IO;

namespace Steganography.Core
{
    class FileSteganography : ISteganography
    {
        private readonly Header header;
        private readonly Bitmap targetImage;

        public FileSteganography(Header header, Bitmap targetImage)
        {
            this.header = header;
            this.targetImage = targetImage;
        }

        public bool Hide(string param)
        {
            var bytes = File.ReadAllBytes(param);
            return true;
        }

        public string Reveal(Bitmap image, out byte[] bytes)
        {
            throw new System.NotImplementedException();
        }
    }
}
