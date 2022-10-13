using System.Drawing;

namespace Steganography.Core
{
    public interface ISteganography
    {
        void Hide(Bitmap image, string param, int bitsPerPixel = 3);
        string Reveal(Bitmap image, out byte[] bytes, int bitsPerPixel = 3);
    }
}
