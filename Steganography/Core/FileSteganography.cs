using System.Drawing;

namespace Steganography.Core
{
    class FileSteganography : ISteganography
    {
        public void Hide(Bitmap image, string param, int bitsPerPixel = 3)
        {
            throw new System.NotImplementedException();
        }

        public string Reveal(Bitmap image, out byte[] bytes, int bitsPerPixel = 3)
        {
            throw new System.NotImplementedException();
        }
    }
}
