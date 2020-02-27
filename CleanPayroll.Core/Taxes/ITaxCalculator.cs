using NodaTime;

namespace CleanPayroll.Core.Taxes
{
  public interface ITaxCalculator
  {
    TaxAssessment? Calculate(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate);
  }
}
