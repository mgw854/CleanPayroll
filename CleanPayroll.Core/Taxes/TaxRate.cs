using System;

namespace CleanPayroll.Core
{
  public readonly struct TaxRate
  {
    public TaxRate(decimal rate)
    {
      if (rate < 0 || rate > 1)
      {
        throw new ArgumentOutOfRangeException(nameof(rate), rate, "A tax rate may not be less than 0% or more than 100%");
      }

      this.Value = rate;
    }

    internal decimal Value { get; }

    public override string ToString()
    {
      return this.Value.ToString("P");
    }
  }
}
