using System;
using System.Collections.Generic;
using System.Linq;

namespace Soltys.VirtualMachine
{
    public class VMFunction :IEquatable<VMFunction>
    {
        public VMFunction(string name)
        {
            Name = name;
            Instructions = Array.Empty<IInstruction>();
        }

        public VMFunction(string name, IEnumerable<IInstruction> instructions)
        {
            Name = name;
            Instructions = instructions.ToArray();
        }

        public string Name
        {
            get;
        }

        public IInstruction[] Instructions
        {
            get;
        }

        public bool Equals(VMFunction? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((VMFunction) obj);
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
