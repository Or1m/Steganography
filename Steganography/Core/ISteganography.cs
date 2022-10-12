using System.Drawing;

namespace Steganography
{
    public interface ISteganography
    {
        void Hide(Bitmap image, string param, int numOfChannels = 3);
        string UnHide(Bitmap image, out byte[] bytes);
    }
}
