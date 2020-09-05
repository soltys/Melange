using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoltysVm
{
    internal class Disassembler
    {
        public IEnumerable<string> Disassemble(Stream source) => 
            InstructionDecoder.DecodeStream(source).Select(x => x.ToString());
    }
}
