using System;
using System.IO;
using MsgPackSlim.Parsers;
using MsgPackSlim.Types;

namespace MsgPackSlim
{
    public class FloatingPointType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xca, 1);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            switch (formatByte)
            {
                case 0xca:
                    return ValueInfo.ForHeaderlessContent(4);
                case 0xcb:
                    return ValueInfo.ForHeaderlessContent(8);
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            switch (formatByte)
            {
                case 0xca:
                    return NumericConverter.ToSingle(contentBytes);
                case 0xcb:
                    return NumericConverter.ToDouble(contentBytes);
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
        }
    }
}
