using System.Threading.Tasks;
using NodaTime;

namespace CleanPayroll.Core
{
  public interface IPayEstimator
  {
    Task<Money> EstimateSalaryOverYearAsync(Money grossPayToDate, DateInterval payPeriod, PayCycle payCycle, Money grossPay);
  }
}