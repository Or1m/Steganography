﻿using System.Drawing;

namespace Steganography
{
    class FileSteganography : ISteganography
    {
        public void Hide(Bitmap image, string param, int numOfChannels = 3)
        {
            throw new System.NotImplementedException();
        }

        public string Reveal(Bitmap image, out byte[] bytes, int numOfChannels = 3)
        {
            throw new System.NotImplementedException();
        }
    }
}
