using Newtonsoft.Json;
using Steganography.Enums;
using System.IO;

namespace Steganography.Core
{
    public class Header
    {
        public const int Size = 5 + 16 + 96 + 512;

        public EType MsgType { get; set; } // 1 bit
        public EValidPixelChannels ValidPixelChannels { get; set; } // 4 bits
        public int NumOfBits { get; set; } // 32 bits

        public int FistX { get; set; } // 32 bits
        public int FirstY { get; set; } // 32 bits

        public byte StepX { get; set; } // 8 bits
        public byte StepY { get; set; } // 8 bits

        /// Only valid for <see cref="EType.File"/>
        public string FileName { get; set; } // 64 * 8 bits

        public static Header FromJSON()
        {
            return JsonConvert.DeserializeObject<Header>(Properties.Resources.DefaultHeader);
        }
    }
}
