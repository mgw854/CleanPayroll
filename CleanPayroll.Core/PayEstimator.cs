using System.Linq;
using NodaTime;

namespace CleanPayroll.Core
{
  public sealed class PayEstimator
  {
    public Money EstimateSalaryOverYear(Money grossPayToDate, DateInterval payPeriod, PayCycle payCycle, Money grossPay)
    {
      return grossPayToDate + grossPay + new Money(payCycle.GetPayDates(payPeriod.Start.Year).Count(d => d > payPeriod.End) * grossPay.Value);
    }
  }
}
