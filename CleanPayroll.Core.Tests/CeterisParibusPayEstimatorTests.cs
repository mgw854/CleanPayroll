using System.Threading.Tasks;
using NodaTime;
using Xunit;

namespace CleanPayroll.Core.Tests
{
  public class CeterisParibusPayEstimatorTests
  {
    [Fact]
    public async Task EstimateSalaryOverYear_FirstPay_ReturnsCorrectValue()
    {
      Money estimate = await new CeterisParibusPayEstimator().EstimateSalaryOverYearAsync(Money.Zero, new DateInterval(new LocalDate(2020, 01, 01), new LocalDate(2020,01,14) ), new SemimonthlyPayCycle(), new Money(1_000m));

      Assert.Equal(new Money(24_000m), estimate);
    }

    [Fact]
    public async Task EstimateSalaryOverYear_MiddlePay_ReturnsCorrectValue()
    {
      Money estimate = await new CeterisParibusPayEstimator().EstimateSalaryOverYearAsync(new Money(12_000m), new DateInterval(new LocalDate(2020, 07, 01), new LocalDate(2020, 07, 14)), new SemimonthlyPayCycle(), new Money(1_000m));

      Assert.Equal(new Money(24_000m), estimate);
    }

    [Fact]
    public async Task EstimateSalaryOverYear_LastPay_ReturnsCorrectValue()
    {
      Money estimate = await new CeterisParibusPayEstimator().EstimateSalaryOverYearAsync(new Money(23_000m), new DateInterval(new LocalDate(2020, 12, 15), new LocalDate(2020, 12, 31)), new SemimonthlyPayCycle(), new Money(1_000m));

      Assert.Equal(new Money(24_000m), estimate);
    }
  }
}
