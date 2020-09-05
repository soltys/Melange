using System;

namespace SoltysVm
{
    public interface IInstruction
    {
        void Accept(IRuntimeVisitor visitor);
        ReadOnlySpan<byte> GetBytes();
    }
}
