namespace Soltys.StateMachine.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Compiler compiler = new Compiler();
        var stateMachine = compiler.Run();

        Assert.Equal("CashMachine", stateMachine.Name);
        Assert.Equal(6, stateMachine.States.Count);
    }
}
