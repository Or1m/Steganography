using Steganography.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Steganography
{
    public static class Utils
    {
        public static void PackInt(List<byte> bytes, int value)
        {
            bytes.Add((byte)(value >> 24));
            bytes.Add((byte)(value >> 16));
            bytes.Add((byte)(value >> 8));
            bytes.Add((byte)(value >> 0));
        }
        public static int UnpackInt(List<string> list, ref int idx)
        {
            int b1 = (Convert.ToByte(list[idx++].ReverseStr(), 2) << 24);
            int b2 = (Convert.ToByte(list[idx++].ReverseStr(), 2) << 16);
            int b3 = (Convert.ToByte(list[idx++].ReverseStr(), 2) << 8);
            int b4 = (Convert.ToByte(list[idx++].ReverseStr(), 2) << 0);

            return b1 | b2 | b3 | b4;
        }

        public static void ResizeImage(ref Bitmap image, long neededBits, Header header)
        {
            while (image.AvailableBits(header) <= neededBits)
                ResizeImage(ref image, image.Width * 2, image.Height * 2);
        }
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private static void ResizeImage(ref Bitmap image, int width, int height)
        {
            var rect = new Rectangle(0, 0, width, height);
            var result = new Bitmap(width, height);

            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            image = result;
        }
    }
}
