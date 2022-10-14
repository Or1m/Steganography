using System.Drawing;

namespace Steganography.Core
{
    public interface ISteganography
    {
        bool Hide(string param);
        string Reveal(Bitmap image, out byte[] bytes);
    }
}
