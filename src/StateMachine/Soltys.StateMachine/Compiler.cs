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


        var visitor = new StateMachineVisitor();
        visitor.Visit(tree);
        return visitor.StateMachine;
    }
}

public class StateMachineVisitor : StateMachineBaseVisitor<Node>
{
    private IStateHolder currentStateHolder;
    private ITransitionHolder currentTransitionHolder;

    public StateMachine StateMachine
    {
        get;
    }

    public StateMachineVisitor()
    {
        StateMachine = new StateMachine();
        currentStateHolder = StateMachine;
        currentTransitionHolder = null;
    }
    public override Node VisitAttribute([NotNull] StateMachineParser.AttributeContext context)
    {
        var key = context.IDEN(0);
        var value = context.IDEN(1);

        if (key.GetText() == "name")
        {
            StateMachine.Name = value.GetText();
        }

        return base.VisitAttribute(context);
    }

    public override Node VisitStateMachine([NotNull] StateMachineParser.StateMachineContext context)
    {
        VisitChildren(context);
        return StateMachine;
    }

    public override Node VisitStateDefinition([NotNull] StateMachineParser.StateDefinitionContext context)
    {
        var tempTransitionHolder = currentTransitionHolder;
        var state = new State { Name = context.IDEN().GetText() };
        
        currentStateHolder.States.Add(state);

        currentTransitionHolder = state;
        base.VisitStateDefinition(context);
        currentTransitionHolder = tempTransitionHolder;
        return state;
    }

    public override Node VisitProcessDefinition([NotNull] StateMachineParser.ProcessDefinitionContext context)
    {
        var tempStateHolder = currentStateHolder;
        var tempTransitionHolder = currentTransitionHolder;
        var process = new Process { Name = context.IDEN().GetText() };
        StateMachine.Processes.Add(process);

        currentStateHolder = process;
        currentTransitionHolder = process;
        base.VisitProcessDefinition(context);
        currentStateHolder = tempStateHolder;
        currentTransitionHolder = tempTransitionHolder;
        return process;
    }
    public override Node VisitState_entry([NotNull] StateMachineParser.State_entryContext context)
    {
        return base.VisitState_entry(context);
    }
    public override Node VisitTransition([NotNull] StateMachineParser.TransitionContext context)
    {
        var trasition = new Transition();
        if (context.MANUAL_TRANSITION() != null)
        {
            trasition.TransitionType = TransitionType.Manual;
        }
        else if (context.AUTO_TRANSITION() != null)
        {
            trasition.TransitionType = TransitionType.Automatic;
        }

        trasition.Destination = context.IDEN().GetText();
        
        currentTransitionHolder.Transitions.Add(trasition);
        base.VisitTransition(context);
        
        return trasition;
    }
}

public class Node
{
}

public class StateMachine : Node, IStateHolder
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

public interface ITransitionHolder
{
    public List<Transition> Transitions
    {
        get;
    }
}

public class State : Node, ITransitionHolder
{
    public string Name
    {
        get; set;
    }

    public List<Transition> Transitions
    {
        get;
    } = new List<Transition>();
}

public class Transition : Node
{
    public TransitionType TransitionType
    {
        get; set;
    }

    public string Destination
    {
        get; set;
    }

    public string TriggerName
    {
        get; set;
    }
}

public enum TransitionType
{
    Automatic,
    Manual
}

public class Process : Node, IStateHolder, ITransitionHolder
{
    public string Name
    {
        get; set;
    }

    public List<State> States
    {
        get;
    } = new List<State>();

    public List<Transition> Transitions { get; } = new List<Transition>();
}
