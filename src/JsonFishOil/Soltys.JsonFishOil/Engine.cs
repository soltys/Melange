using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
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
        makePropertyFunc.ValueFunc = Visit(context.propertyValue());
        return makePropertyFunc;
    }

    public override JsonFunc VisitPropertyValue([NotNull] JsonFishOilParser.PropertyValueContext context)
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
}

public class FishOilContext
{
    JToken JsonRoot
    {
        get;set;
    }
}

public class JsonFunc
{
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
}

public class MakeObjectFunc : JsonFunc
{
    public List<MakePropertyFunc> PropertyFuncs
    {
        get;
    } = new List<MakePropertyFunc>();
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
}

public class ConstValueFunc : JsonFunc
{
    public string Value
    {
        get; set;
    }
}
