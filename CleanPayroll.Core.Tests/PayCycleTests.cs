using System.Linq;
using NodaTime;
using Xunit;

namespace CleanPayroll.Core.Tests
{
  public class PayCycleTests
  {
    [Fact]
    public void BiweeklyPayCycleGetPayDates_Year2020_ReturnsList()
    {
      BiweeklyPayCycle period = new BiweeklyPayCycle();
      var payDates = period.GetPayDates(2019);

      Assert.Equal(26, payDates.Count);
      Assert.Equal(new LocalDate(2019, 1, 4), payDates.First());
      Assert.Equal(new LocalDate(2019, 12, 20), payDates.Last());
    }

    [Fact]
    public void SemimonthlyPayCycleGetPayDates_Year2020_ReturnsList()
    {
      SemimonthlyPayCycle period = new SemimonthlyPayCycle();
      var payDates = period.GetPayDates(2019);

      Assert.Equal(24, payDates.Count);
      Assert.Equal(new LocalDate(2019, 1, 1), payDates.First());
      Assert.Equal(new LocalDate(2019, 12, 13), payDates.Last());
    }

    [Fact]
    public void MonthlyPayCycleGetPayDates_Year2020_ReturnsList()
    {
      MonthlyPayCycle period = new MonthlyPayCycle();
      var payDates = period.GetPayDates(2019);

      Assert.Equal(12, payDates.Count);
      Assert.Equal(new LocalDate(2019, 1, 31), payDates.First());
      Assert.Equal(new LocalDate(2019, 12, 31), payDates.Last());
    }
  }
}
