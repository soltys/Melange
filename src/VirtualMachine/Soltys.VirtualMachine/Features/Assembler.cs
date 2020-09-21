using System.Collections.Generic;
using System.IO;

namespace Soltys.VirtualMachine
{
    internal class Assembler 
    {
        public void Assemble(Stream outputStream, IEnumerable<IInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                outputStream.Write(instruction.GetBytes());
            }
        }
    }
}
