using System;

namespace Soltys.VirtualMachine
{
    public interface IInstruction
    {
        void Accept(IRuntimeVisitor visitor);
        ReadOnlySpan<byte> GetBytes();
    }
}
