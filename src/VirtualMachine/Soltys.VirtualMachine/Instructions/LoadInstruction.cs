using System;

namespace Soltys.VirtualMachine
{
    public abstract class LoadInstruction
    {
        public LoadKind LoadKind
        {
            get;
        }

        protected LoadInstruction(LoadKind loadKind)
        {
            LoadKind = loadKind;
        }


        public static (IInstruction, int) Create(in ReadOnlySpan<byte> span)
        {
            var loadType = OpcodeHelper.ToLoadType(span[0]);
            var bytesRead = 1; // for LoadKind
            switch (loadType)
            {
                case LoadKind.Argument:
                case LoadKind.Local:
                case LoadKind.StaticField:
                    return (new LoadPlaceInstruction(loadType, span[1]), bytesRead + 1);
                case LoadKind.String:
                    return CreateLoadStringInstruction(span.Slice(bytesRead), bytesRead);
                case LoadKind.Integer:
                case LoadKind.Double:
                    return CreateLoadConstantInstruction(span.Slice(bytesRead), bytesRead, loadType);
                case LoadKind.Library:
                    return CreateLoadLibraryInstruction(span.Slice(bytesRead), bytesRead);
                default:
                    throw new LoadTypeDecodeException();
            }
        }

        private static (IInstruction, int) CreateLoadConstantInstruction(ReadOnlySpan<byte> span, in int alreadyBytesRead, LoadKind loadKind)
        {
            int totalBytesRead = alreadyBytesRead;
            object value;
            switch (loadKind)
            {
                case LoadKind.Integer:
                    value = BitConverter.ToInt32(span);
                    totalBytesRead += sizeof(int);
                    break;
                case LoadKind.Double:
                    value = BitConverter.ToDouble(span);
                    totalBytesRead += sizeof(double);
                    break;
                default:
                    throw new InvalidOperationException($"LoadKind: {loadKind} is not supported for loading constant instruction");
            }

            return (new LoadConstantInstruction(loadKind, value), totalBytesRead);
        }

        private static (IInstruction, int) CreateLoadStringInstruction(in ReadOnlySpan<byte> span, int alreadyBytesRead)
        {
            var (stringItself, stringBytesRead) = InstructionDecoder.DecodeString(span);
            return (new LoadStringInstruction(stringItself), alreadyBytesRead + stringBytesRead);
        }

        private static (IInstruction, int) CreateLoadLibraryInstruction(ReadOnlySpan<byte> span, in int alreadyBytesRead)
        {
            var (stringItself, stringBytesRead) = InstructionDecoder.DecodeString(span);
            return (new LoadLibraryInstruction(stringItself), alreadyBytesRead + stringBytesRead);
        }
    }
}
