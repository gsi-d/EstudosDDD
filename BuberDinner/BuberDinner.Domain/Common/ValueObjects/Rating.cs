using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public double Value { get; }

    private Rating(double value)
    {
        Value = Value;
    }
    public static Rating CreateNew(double value)
    {
        return new(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}