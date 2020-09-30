using System.Collections.Generic;
using System.Linq;

namespace Soltys.Lisp.Compiler
{
    internal class AstList : IAstNode
    {
        private readonly List<IAstNode> elements;
        public int Length => this.elements.Count;
        public AstList()
        {
            this.elements = new List<IAstNode>();
        }

        public AstList(IEnumerable<IAstNode> listElements) : this()
        {
            this.elements.AddRange(listElements);
        }

        public void Add(IAstNode node)
        {
            this.elements.Add(node);
        }

        public IAstNode this[int index] => this.elements[index];

        public void Accept(IAstVisitor visitor) => visitor.VisitList(this);

        public override string ToString() => $"({ToStringElements()})";

        private string ToStringElements() => string.Join(' ',this.elements.Select(x=>x.ToString()));
    }
}
