namespace Soltys.Library.VisualStudioSolution
{
    public class AstSolution
    {
        public AstSolution(AstHeader header, AstBody body)
        {
            Header = header;
            Body = body;
        }

        public AstHeader Header
        {
            get;
            set;
        }

        public AstBody Body
        {
            get;
            set;
        }
    }
}
