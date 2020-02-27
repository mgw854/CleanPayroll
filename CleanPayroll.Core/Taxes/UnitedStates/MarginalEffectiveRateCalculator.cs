using System.Collections.Generic;
using System.Linq;

namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public sealed class MarginalEffectiveRateCalculator
  {
    public TaxRate CalculateEffectiveRate(IReadOnlyCollection<(Money floor, TaxRate rate)> marginalRates, Money expectedSalary)
    {
      var sortedMarginalRates = marginalRates.OrderBy(mr => mr.floor).ToList();
      sortedMarginalRates.Add((new Money(1_000_000_000_000.00m), new TaxRate(1.0m)));

      Money remaining = new Money(expectedSalary.Value);
      decimal effectiveRate = 0.0m;

      foreach ((Money floor, Money ceiling, TaxRate rate) in sortedMarginalRates.Zip(sortedMarginalRates.Skip(1), ((r1, r2) => (r1.floor, r2.floor, r1.rate))))
      {
        if (floor >= expectedSalary || remaining == Money.Zero)
        {
          break;
        }

        // What portion of your salary falls in this bucket
        Money portion = Money.Min(ceiling - floor, remaining);
        decimal salaryInBucket =  portion / expectedSalary;

        // What rate is that taxed at
        effectiveRate += salaryInBucket * rate.Value;

        remaining -= portion;
      }

      return new TaxRate(effectiveRate);
    }
  }
}
