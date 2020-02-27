using System.Collections.Generic;
using NodaTime;

namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public sealed class SocialSecurityTaxes : ITaxCalculator
  {
    private static readonly IReadOnlyDictionary<int, Money> PayCap = new Dictionary<int, Money>()
    {
      { 2019, new Money(132_900m) },
      { 2020, new Money(137_700m) }
    };

    public TaxAssessment? Calculate(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate)
    {
      Money cap = SocialSecurityTaxes.PayCap[interval.Start.Year];

      Money tax = grossPay.GetTaxOnPayWithCap(grossPayToDate, cap, new TaxRate(0.062m));

      return new TaxAssessment(tax, tax);
    }
  }
}
