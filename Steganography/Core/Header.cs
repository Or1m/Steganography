using Newtonsoft.Json;
using Steganography.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steganography.Core
{
    public class Header
    {
        public const EValidPixelChannels HeaderChannels = EValidPixelChannels.RGB;
        public const string DefaultHeaderPath           = "Resources\\DefaultHeader.txt";

        public const int MaxNameLength  = 64;
        public const int BitsPerChar    = 8; // ASCII
        public const int Size           = 8 + 16 + 96 + 512;

        public EType MsgType { get; set; } // 4 bits, because of packing to byte with ValidPixelChannels
        public EValidPixelChannels ValidPixelChannels { get; set; } // 4 bits
        public int NumOfBits { get; set; } // 32 bits

        public int FirstX { get; set; } // 32 bits
        public int FirstY { get; set; } // 32 bits

        public byte StepX { get; set; } // 8 bits
        public byte StepY { get; set; } // 8 bits

        /// Only used for <see cref="EType.File"/>
        public string FileName { get; set; } // MaxNameLength * 8 bits


        public static Header FromJSON(string json)
        {
            return JsonConvert.DeserializeObject<Header>(json);
        }
        /// <summary>
        /// Bit packing function for Header
        /// </summary>
        public static bool ToBitArray(Header header, out BitArray headerBits)
        {
            List<byte> bytes = new List<byte>();

            int msgType = (byte)header.MsgType << 4;
            int channels = (byte)header.ValidPixelChannels;
            byte msgChannels = (byte)(msgType | channels);

            bytes.Add(msgChannels);
            Utils.PackInt(bytes, header.NumOfBits);
            Utils.PackInt(bytes, header.FirstX);
            Utils.PackInt(bytes, header.FirstY);
            bytes.Add(header.StepX);
            bytes.Add(header.StepY);

            StringBuilder builder = new StringBuilder(header.FileName); // Append ~ for all remaining header bytes
            for (int i = builder.Length; i < MaxNameLength; i++)
                builder.Append("~");

            var nameBytes = Encoding.ASCII.GetBytes(builder.ToString());
            bytes.AddRange(nameBytes);

            headerBits = new BitArray(bytes.ToArray());
            return true;
        }
        /// <summary>
        /// Bit unpacking function for Header
        /// </summary>
        public static bool FromBinaryList(List<string> list, out Header header)
        {
            int idx = 0;
            header = new Header();

            var msgChannelslist = Convert.ToByte(list[idx++].ReverseStr(), 2);
            header.MsgType = (EType)(msgChannelslist >> 4);
            header.ValidPixelChannels = (EValidPixelChannels)(msgChannelslist & 0b00001111);

            header.NumOfBits = Utils.UnpackInt(list, ref idx);
            header.FirstX = Utils.UnpackInt(list, ref idx);
            header.FirstY = Utils.UnpackInt(list, ref idx);
            header.StepX = Convert.ToByte(list[idx++].ReverseStr(), 2);
            header.StepY = Convert.ToByte(list[idx++].ReverseStr(), 2);

            header.FileName = Encoding.Default.GetString(
                list.Select(x => Convert.ToByte(x.ReverseStr(), 2)).ToArray(), idx, MaxNameLength);

            return true;
        }
    }
}
