using System.Threading.Tasks;
using NodaTime;

namespace CleanPayroll.Core.Taxes
{
  public interface ITaxCalculator
  {
    Task<TaxAssessment?> CalculateAsync(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate);
  }
}
