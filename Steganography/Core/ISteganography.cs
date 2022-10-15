using Steganography.Enums;
using System.Drawing;

namespace Steganography.Core
{
    public interface ISteganography
    {
        EType AllowedMsgType { get; }

        bool Hide(string param);
        string Reveal(Bitmap image, out byte[] bytes);
    }
}
