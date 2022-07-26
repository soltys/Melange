using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Soltys.StateMachine;
public class Compiler
{
    public StateMachine Run()
    {
        AntlrInputStream inputStream = new AntlrInputStream(File.ReadAllText("example.txt"));
        var lexer = new StateMachineLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new StateMachineParser(tokens);

        parser.BuildParseTree = true;

        var tree = parser.stateMachine();


        var listener = new StateMachineListener();
        ParseTreeWalker.Default.Walk(listener, tree);
        return listener.StateMachine;
    }
}

public class StateMachineListener : StateMachineBaseListener
{
    private IStateHolder currentStateHolder;
    public StateMachine StateMachine
    {
        get;
    }

    public StateMachineListener()
    {
        StateMachine = new StateMachine();
        currentStateHolder = StateMachine;
    }
    public override void EnterAttribute([NotNull] StateMachineParser.AttributeContext context)
    {
        var key = context.IDEN(0);
        var value = context.IDEN(1);

        if (key.GetText() == "name")
        {
            StateMachine.Name = value.GetText();
        }


        base.EnterAttribute(context);
    }

    public override void EnterState_def([NotNull] StateMachineParser.State_defContext context)
    {
        var stateIden = context.IDEN();

        if (context.STATE_() != null)
        {
            currentStateHolder.States.Add(new State { Name = stateIden.GetText() });
        }
        else if (context.PROCESS_() != null)
        {
            var newProcess = new Process { Name = stateIden.GetText() };
            currentStateHolder = newProcess;

            StateMachine.Processes.Add(newProcess);
        }
    }
    public override void ExitState_def([NotNull] StateMachineParser.State_defContext context)
    {
        if (context.PROCESS_() != null)
        {
            currentStateHolder = StateMachine;
        }
    }
}

public class StateMachine : IStateHolder
{
    public string Name
    {
        get; set;
    }

    public List<State> States
    {
        get;
    } = new List<State>();

    public List<Process> Processes
    {
        get;
    } = new List<Process>();
}
public interface IStateHolder
{
    public List<State> States
    {
        get;
    }
}

public class State
{
    public string Name
    {
        get; set;
    }
}

public class Process : IStateHolder
{
    public string Name
    {
        get; set;
    }

    public List<State> States
    {
        get;
    }=  new List<State>();
}
