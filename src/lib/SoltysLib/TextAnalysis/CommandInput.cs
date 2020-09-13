using System.IO;

namespace SoltysLib.TextAnalysis
{
    public class CommandInput : ICommandInput
    {
        private readonly TextReader inputStream;

        public CommandInput(string input)
        {
            this.inputStream = new StringReader(input);
        }

        public string GetToEnd()
        {
            return this.inputStream.ReadToEnd();
        }
    }
}
