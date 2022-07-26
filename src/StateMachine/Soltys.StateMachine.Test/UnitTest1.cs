namespace Soltys.StateMachine.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Compiler compiler = new Compiler();
        var stateMachine = compiler.Run();

        Assert.Equal("CashMachine", stateMachine.Name);
        Assert.Equal(5, stateMachine.States.Count);
        Assert.Equal(1, stateMachine.Processes.Count);
        Assert.Equal(3, stateMachine.Processes[0].States.Count);
    }
}
