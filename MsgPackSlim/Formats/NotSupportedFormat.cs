using System;
using System.IO;

namespace MsgPackSlim.Formats
{
    public class NotSupportedFormat : IMsgPackFormat
    {
        public void Register(IMsgPackFormat[] formatMap)
        {
            throw new NotSupportedException();
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            throw Exceptions.FormatByteNotSupported(formatByte);
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            throw Exceptions.FormatByteNotSupported(formatByte);
        }
    }
}
