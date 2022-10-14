using Newtonsoft.Json;
using Steganography.Enums;
using System.Collections;
using System.Drawing;
using System.IO;

namespace Steganography.Core
{
    public class Header
    {
        public const int MaxNameLength = 64;
        public const int Size = 8 + 16 + 96 + 512;

        public EType MsgType { get; set; } // 4 bits, because of packing to byte with ValidPixelChannels
        public EValidPixelChannels ValidPixelChannels { get; set; } // 4 bits
        public int NumOfBits { get; set; } // 32 bits

        public int FirstX { get; set; } // 32 bits
        public int FirstY { get; set; } // 32 bits

        public byte StepX { get; set; } // 8 bits
        public byte StepY { get; set; } // 8 bits

        /// Only valid for <see cref="EType.File"/>
        public string FileName { get; set; } // MaxNameLength * 8 bits

        public static Header FromJSON(string json)
        {
            return JsonConvert.DeserializeObject<Header>(json);
        }
        public static Header FromImage(Bitmap image)
        {
            return new Header(); //TODO dorob parsovanie
        }
    }
}
