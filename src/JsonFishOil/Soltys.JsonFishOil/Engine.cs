using Antlr4.Runtime;
using Newtonsoft.Json;

namespace Soltys.JsonFishOil;
public class Engine
{
    public static JsonFunc CompileToFunc()
    {
        var input = File.ReadAllText("make_obj.txt");
        AntlrInputStream inputStream = new AntlrInputStream(input);
        var lexer = new JsonFishOilLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new JsonFishOilParser(tokens);

        var tree = parser.fishOil();
        var visitor = new JsonFishOilVisitor();
        return visitor.Visit(tree);
    }

    public static string ExectuteFunc()
    {
        var jsonFunc = CompileToFunc();
        var fishOilOutput = jsonFunc.Execute(FishOilContext.Create(File.ReadAllText("data.json")));
        return FormatJson(fishOilOutput);
    }

    private static string FormatJson(string json)
    {
        dynamic parsedJson = JsonConvert.DeserializeObject(json);
        return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
    }
}
