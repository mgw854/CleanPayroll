using NodaTime;
using Xunit;

namespace CleanPayroll.Core.Tests
{
  public class PayEstimatorTests
  {
    [Fact]
    public void EstimateSalaryOverYear_FirstPay_ReturnsCorrectValue()
    {
      Money estimate = new PayEstimator().EstimateSalaryOverYear(Money.Zero, new DateInterval(new LocalDate(2020, 01, 01), new LocalDate(2020,01,14) ), new SemimonthlyPayCycle(), new Money(1_000m));

      Assert.Equal(new Money(24_000m), estimate);
    }

    [Fact]
    public void EstimateSalaryOverYear_MiddlePay_ReturnsCorrectValue()
    {
      Money estimate = new PayEstimator().EstimateSalaryOverYear(new Money(12_000m), new DateInterval(new LocalDate(2020, 07, 01), new LocalDate(2020, 07, 14)), new SemimonthlyPayCycle(), new Money(1_000m));

      Assert.Equal(new Money(24_000m), estimate);
    }

    [Fact]
    public void EstimateSalaryOverYear_LastPay_ReturnsCorrectValue()
    {
      Money estimate = new PayEstimator().EstimateSalaryOverYear(new Money(23_000m), new DateInterval(new LocalDate(2020, 12, 15), new LocalDate(2020, 12, 31)), new SemimonthlyPayCycle(), new Money(1_000m));

      Assert.Equal(new Money(24_000m), estimate);
    }
  }
}
