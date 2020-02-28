using System.Collections.Generic;
using CleanPayroll.Core.Taxes.UnitedStates;
using Xunit;

namespace CleanPayroll.Core.Tests.Taxes.UnitedStates
{
  public class MarginalEffectiveRateCalculatorTests
  {
    [Fact]
    public void CalculateEffectiveRate_GivenSingleStep_ReturnsCorrectValue()
    {
      List<TaxBracket> rates = new List<TaxBracket>()
      {
        new TaxBracket(Money.Zero, new TaxRate(0.1m)),
        new TaxBracket(new Money(10_000m), new TaxRate(0.2m))
      };

      TaxRate effective = new MarginalEffectiveRateCalculator().CalculateEffectiveRate(rates, new Money(2_000m));

      Assert.Equal(new TaxRate(0.1m), effective);
    }

    [Fact]
    public void CalculateEffectiveRate_GivenTwoSteps_ReturnsCorrectValue()
    {
      List<TaxBracket> rates = new List<TaxBracket>()
      {
        new TaxBracket(Money.Zero, new TaxRate(0.1m)),
        new TaxBracket(new Money(10_000m), new TaxRate(0.2m))
      };

      TaxRate effective = new MarginalEffectiveRateCalculator().CalculateEffectiveRate(rates, new Money(20_000m));

      Assert.Equal(new TaxRate(0.15m), effective);
    }
  }
}
