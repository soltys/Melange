#nullable disable
namespace Soltys.Library;

public class Result<TValue>
{
    public static Result<TValue> Fail(string message)
    {
        var r = new Result<TValue>();
        r.Add(new Error(message));
        return r;
    }

    public Result(TValue value)
    {
        Value = value;
    }

    protected Result()
    {
        Value = default(TValue);
    }

    public TValue Value { get; }

    private readonly List<Reason> reasons = new List<Reason>();
    public void Add(Reason reason) => this.reasons.Add(reason);
    public IEnumerable<Error> Errors => this.reasons.OfType<Error>();
    public IEnumerable<Success> Successes => this.reasons.OfType<Success>();

    public bool IsFailure => this.reasons.Any(x => x is Error);
    public bool IsSuccess => !IsFailure;
}
