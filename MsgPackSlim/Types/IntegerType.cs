using System.IO;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Types
{
    public class IntegerType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x00, 7);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xd0, 2);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xe0, 5);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            if ((formatByte & 0x80) == 0)
                return ValueInfo.TypeOnlyValue;
            if ((formatByte & 0xe0) == 0xe0)
                return ValueInfo.TypeOnlyValue;

            switch (formatByte)
            {
                case 0xd0:
                    return ValueInfo.ForHeaderlessContent(1);
                case 0xd1:
                    return ValueInfo.ForHeaderlessContent(2);
                case 0xd2:
                    return ValueInfo.ForHeaderlessContent(4);
                case 0xd3:
                    return ValueInfo.ForHeaderlessContent(8);
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            if (formatByte == 0xd3)
                return NumericConverter.ToInt64(contentBytes);

            if ((formatByte & 0x80) == 0)
                return (int) formatByte;
            if ((formatByte & 0xe0) == 0xe0)
                return (int) (formatByte | 0xffffff00);

            var number = NumericConverter.ToInt32(contentBytes);
            return ExpandNegativeInteger(formatByte, number);
        }

        private static int ExpandNegativeInteger(byte formatByte, int number)
        {
            switch (formatByte)
            {
                case 0xd0:
                    if ((number & 0x80) != 0)
                        return number | (-1 ^ 0xff);
                    break;

                case 0xd1:
                    if ((number & 0x8000) != 0)
                        return number | (-1 ^ 0xffff);
                    break;

                case 0xd2:
                    if ((number & 0x800000) != 0)
                        return number | (-1 ^ 0xffffff);
                    break;

                case 0xd3:
                    break;

                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
            return number;
        }
    }
}
