using System;
using System.Drawing;

namespace Steganography.Core
{
    class SteganographyDetection
    {
        private readonly Bitmap image;

        public SteganographyDetection(Bitmap image)
        {
            this.image = image;
        }

        /// <summary></summary>
        /// <param name="threshold">Number of neigbours which have to be slightly different from middle pixel</param>
        public bool IsSteganography(int threshold)
        {
            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    var neigbours = GetNeighbours(x, y);

                    if (IsSteganographyOnNeighbours(pixel, neigbours, threshold))
                        return true;
                }
            }

            return false;
        }

        private Color[] GetNeighbours(int x, int y)
        {
            return new Color[]
            {
                image.GetPixel(x - 1, y - 1),
                image.GetPixel(x, y - 1),
                image.GetPixel(x + 1, y - 1),
                image.GetPixel(x - 1, y),
                image.GetPixel(x + 1, y),
                image.GetPixel(x - 1, y + 1),
                image.GetPixel(x, y + 1),
                image.GetPixel(x + 1, y + 1),
            };
        }
        /// <summary></summary>
        /// <param name="threshold">Number of neigbours which have to be slightly different from middle pixel</param>
        /// <returns>True if detects steganography on neighbours, False otherwise</returns>
        private bool IsSteganographyOnNeighbours(Color pixel, Color[] neigbours, int threshold)
        {
            int diffCount = 0;
            int[] pixelComponents = new int[] { pixel.R, pixel.G, pixel.B, pixel.A };

            foreach (var neighbour in neigbours)
            {
                bool flag = false;
                int[] neighboursComponents = new int[] { neighbour.R, neighbour.G, neighbour.B, neighbour.A };

                if (!AreColorsSimilar(neighbour, pixel, 50)) // Filter very different pixels like black & white
                    continue;

                for (int i = 0; i < neighboursComponents.Length; i++)
                {
                    var pComp = pixelComponents[0];
                    var nComp = neighboursComponents[0];

                    if (pComp == nComp)
                        continue;

                    if (Math.Abs(pComp - nComp) != 1)
                        continue;

                    // Difference of i component between middle and neighbour is exactly 1
                    flag = true;
                }

                if (flag)
                    diffCount++;
            }

            return diffCount >= threshold;
        }
        private bool AreColorsSimilar(Color c1, Color c2, int tolerance)
        {
            return Math.Abs(c1.R - c2.R) < tolerance &&
                   Math.Abs(c1.G - c2.G) < tolerance &&
                   Math.Abs(c1.B - c2.B) < tolerance &&
                   Math.Abs(c1.A - c2.A) < tolerance;
        }
    }
}
