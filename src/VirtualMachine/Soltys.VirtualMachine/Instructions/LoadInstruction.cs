using System;

namespace Soltys.VirtualMachine
{
    public abstract class LoadInstruction
    {
        public LoadType LoadType
        {
            get;
        }

        protected LoadInstruction(LoadType loadType)
        {
            LoadType = loadType;
        }


        public static (IInstruction, int) Create(in ReadOnlySpan<byte> span)
        {
            var loadType = OpcodeHelper.ToLoadType(span[0]);
            var bytesRead = 1; // for LoadType
            switch (loadType)
            {
                case LoadType.Argument:
                case LoadType.Local:
                case LoadType.StaticField:
                    return (new LoadPlaceInstruction(loadType, span[1]), bytesRead + 1);
                case LoadType.String:
                    return CreateLoadStringInstruction(span.Slice(bytesRead), bytesRead);
                case LoadType.Integer:
                case LoadType.Double:
                    return CreateLoadConstantInstruction(span.Slice(bytesRead), bytesRead, loadType);
                default:
                    throw new LoadTypeDecodeException();
            }
        }

        private static (IInstruction, int) CreateLoadConstantInstruction(ReadOnlySpan<byte> span, in int alreadyBytesRead, LoadType loadType)
        {
            int totalBytesRead = alreadyBytesRead;
            object value;
            switch (loadType)
            {
                case LoadType.Integer:
                    value = BitConverter.ToInt32(span);
                    totalBytesRead += sizeof(int);
                    break;
                case LoadType.Double:
                    value = BitConverter.ToDouble(span);
                    totalBytesRead += sizeof(double);
                    break;
                default:
                    throw new InvalidOperationException($"LoadType: {loadType} is not supported for loading constant instruction");
            }

            return (new LoadConstantInstruction(loadType, value), totalBytesRead);
        }

        private static (IInstruction, int) CreateLoadStringInstruction(in ReadOnlySpan<byte> span, int alreadyBytesRead)
        {
            var (stringItself, stringBytesRead) = OpcodeHelper.DecodeString(span);
            return (new LoadStringInstruction(stringItself), alreadyBytesRead + stringBytesRead);
        }
    }
}
