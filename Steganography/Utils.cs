using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

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
            byte b1 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 24);
            byte b2 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 16);
            byte b3 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 8);
            byte b4 = (byte)(Convert.ToByte(list[idx++].ReverseStr(), 2) << 0);

            return b1 | b2 | b3 | b4;
        }

        public static void ResizeImage(ref Bitmap image, int neededBits)
        {
            image = ResizeImage(image, 2000, 1000);
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private static Bitmap ResizeImage(Image image, int width, int height)
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

            return result;
        }
    }
}
