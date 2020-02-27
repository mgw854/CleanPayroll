using System;
using System.Globalization;
using System.Linq;

namespace CleanPayroll.Core
{
  public readonly struct Money : IComparable<Money>, IEquatable<Money>
  {
    public static Money Zero { get; } = new Money(0.0m);

    public Money(decimal value)
    {
      if (value < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(value), value, "Money must be positive in value");
      }

      string str = value.ToString(CultureInfo.InvariantCulture);

      if (str.Contains('.') && str.Split('.')[1].Length > 2)
      {
        throw new ArgumentOutOfRangeException(nameof(value), value, "Money may not represent values smaller than cents");
      }

      this.Value = value;
    }

    internal decimal Value { get; }

    public override string ToString()
    {
      return this.Value.ToString("C");
    }

    public static Money operator +(Money a, Money b)
    {
      if (b == Money.Zero)
      {
        return a;
      }

      return new Money(a.Value + b.Value);
    }

    public static Money operator -(Money a, Money b)
    {
      if (b == Money.Zero)
      {
        return a;
      }

      return new Money(a.Value - b.Value);
    }

    public static Money operator *(Money amount, TaxRate rate)
    {
      return new Money(Math.Round(amount.Value * rate.Value, 2, MidpointRounding.ToEven));
    }

    public static decimal operator /(Money a, Money b)
    {
      return a.Value / b.Value;
    }

    public static bool operator >(Money a, Money b)
    {
      return a.Value > b.Value;
    }

    public static bool operator <(Money a, Money b)
    {
      return a.Value < b.Value;
    }

    public static bool operator >=(Money a, Money b)
    {
      return a.Value >= b.Value;
    }

    public static bool operator <=(Money a, Money b)
    {
      return a.Value <= b.Value;
    }

    public int CompareTo(Money other)
    {
      return this.Value.CompareTo(other.Value);
    }

    public bool Equals(Money other)
    {
      return this.Value == other.Value;
    }

    public override bool Equals(object obj)
    {
      return obj is Money other && this.Equals(other);
    }

    public override int GetHashCode()
    {
      return this.Value.GetHashCode();
    }

    public static bool operator ==(Money left, Money right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Money left, Money right)
    {
      return !left.Equals(right);
    }

    public static Money Min(Money left, Money right)
    {
      if (left < right)
      {
        return left;
      }

      return right;
    }

    public static Money Max(Money left, Money right)
    {
      if (left > right)
      {
        return left;
      }

      return right;
    }
  }
}
