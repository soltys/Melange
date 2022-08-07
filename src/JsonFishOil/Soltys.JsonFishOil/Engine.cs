using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Soltys.JsonFishOil;
public class Engine
{
    public Engine()
    {

    }

    public static JsonFunc CompileToFunc()
    {
        AntlrInputStream inputStream = new AntlrInputStream(File.ReadAllText("example.txt"));
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


public class JsonFishOilVisitor : JsonFishOilBaseVisitor<JsonFunc>
{
    public override JsonFunc VisitFishOil([NotNull] JsonFishOilParser.FishOilContext context)
    {
        var accessChain = context.accessChain();
        if (accessChain != null)
        {
            var func = Visit(accessChain);
            if (func != null)
            {
                return func;
            }
        }
        return Visit(context.objMake());
    }

    public override JsonFunc VisitObjMake([NotNull] JsonFishOilParser.ObjMakeContext context)
    {
        var objMakeFunc = new MakeObjectFunc();

        foreach (var propertyExpr in context.propertyExpr())
        {
            if (propertyExpr != null)
            {
                objMakeFunc.PropertyFuncs.Add(Visit(propertyExpr) as MakePropertyFunc);
            }

        }

        return objMakeFunc;
    }

    public override JsonFunc VisitPropertyExpr([NotNull] JsonFishOilParser.PropertyExprContext context)
    {
        var makePropertyFunc = new MakePropertyFunc();
        makePropertyFunc.PropertyName = context.NAME().GetText();
        makePropertyFunc.ValueFunc = Visit(context.jsonValue());
        return makePropertyFunc;
    }

    public override JsonFunc VisitJsonValue([NotNull] JsonFishOilParser.JsonValueContext context)
    {
        if (context.NUMBER() != null)
        {
            return new ConstValueFunc { Value = context.NUMBER().GetText() };
        }
        else if (context.STRING() != null)
        {
            return new ConstValueFunc { Value = context.STRING().GetText() };
        }
        return VisitChildren(context);
    }

    public override JsonFunc VisitAccessChain([NotNull] JsonFishOilParser.AccessChainContext context)
    {
        var accessFunc = Visit(context.access()) as AccessFunc;
        if (accessFunc == null)
        {
            throw new InvalidOperationException("Unexpected null value while visiting access chain node");
        }

        if (context.accessChain() != null)
        {
            accessFunc.SubAccess = Visit(context.accessChain()) as AccessFunc;
        }

        return accessFunc;
    }

    public override JsonFunc VisitAccess([NotNull] JsonFishOilParser.AccessContext context)
    {
        AccessFunc access = new AccessFunc();

        var name = context.NAME();
        if (name != null)
        {
            access.ElementName = name.GetText();
        }

        var arrayIndex = context.NUMBER();
        if (arrayIndex != null)
        {
            access.ArrayIndex = int.Parse(arrayIndex.GetText());
        }

        return access;
    }

    public override JsonFunc VisitArrMake([NotNull] JsonFishOilParser.ArrMakeContext context)
    {
        var makeArrayFunc = new MakeArrayFunc();

        foreach (var jsonValue in context.jsonValue())
        {
            makeArrayFunc.ValueFuncs.Add(Visit(jsonValue));
        }

        return makeArrayFunc;
    }
}

public class FishOilContext
{
    public JToken Current
    {
        get; set;
    }

    public static FishOilContext Create(string jsonData)
    {
        return new FishOilContext { Current = JToken.Parse(jsonData) };
    }

    public static FishOilContext Create(JToken jToken)
    {
        return new FishOilContext { Current = jToken };
    }
}

public abstract class JsonFunc
{
    public abstract string Execute(FishOilContext context);
}

public class AccessFunc : JsonFunc
{
    public string ElementName
    {
        get; set;
    }

    public int? ArrayIndex
    {
        get; set;
    }

    public AccessFunc SubAccess
    {
        get; set;
    }

    public override string Execute(FishOilContext context)
    {

        if (ElementName == null)
        {
            // case where input is '.'
            return context.Current.ToString();
        }

        if (context.Current[ElementName] == null)
        {
            throw new InvalidOperationException($"{ElementName} does not exist");
        }

        JToken newCurrentToken;

        if (ArrayIndex.HasValue)
        {
            if (context.Current[ElementName].Type != JTokenType.Array)
            {
                throw new InvalidOperationException($"{ElementName} is not a array");
            }

            newCurrentToken = context.Current[ElementName];
            newCurrentToken = newCurrentToken.ElementAt(ArrayIndex.Value);
        }
        else
        {
            newCurrentToken = context.Current[ElementName];
        }

        if (SubAccess != null)
        {
            return SubAccess.Execute(FishOilContext.Create(newCurrentToken));
        }
        else
        {

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                newCurrentToken.WriteTo(writer);
            }

            return sb.ToString();
        }
    }
}

public class MakeObjectFunc : JsonFunc
{
    public List<MakePropertyFunc> PropertyFuncs
    {
        get;
    } = new List<MakePropertyFunc>();

    public override string Execute(FishOilContext context)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("{");
        sb.Append(string.Join(",", PropertyFuncs.Select(x => x.Execute(context))));
        sb.AppendLine("}");

        return sb.ToString();
    }
}

public class MakeArrayFunc : JsonFunc
{
    public List<JsonFunc> ValueFuncs
    {
        get;
    } = new List<JsonFunc>();

    public override string Execute(FishOilContext context)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("[");
        sb.Append(string.Join(",", ValueFuncs.Select(x => x.Execute(context))));
        sb.AppendLine("]");

        return sb.ToString();
    }
}

public class MakePropertyFunc : JsonFunc
{
    public string PropertyName
    {
        get; set;
    }

    public JsonFunc ValueFunc
    {
        get; set;
    }

    public override string Execute(FishOilContext context)
    {
        return $"\"{PropertyName}\": {ValueFunc.Execute(context)}";
    }
}

public class ConstValueFunc : JsonFunc
{
    public string Value
    {
        get; set;
    }

    public override string Execute(FishOilContext context)
    {
        return Value;
    }
}
