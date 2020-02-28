using System.Linq;
using System.Threading.Tasks;
using NodaTime;

namespace CleanPayroll.Core
{
  public sealed class CeterisParibusPayEstimator : IPayEstimator
  {
    public Task<Money> EstimateSalaryOverYearAsync(Money grossPayToDate, DateInterval payPeriod, PayCycle payCycle, Money grossPay)
    {
      return Task.FromResult(grossPayToDate + grossPay + new Money(payCycle.GetPayDates(payPeriod.Start.Year).Count(d => d > payPeriod.End) * grossPay.Value));
    }
  }
}
