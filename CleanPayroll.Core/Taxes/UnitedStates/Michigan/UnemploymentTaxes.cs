using System.Threading.Tasks;
using NodaTime;

namespace CleanPayroll.Core.Taxes.UnitedStates.Michigan
{
  public sealed class UnemploymentTaxes : ITaxCalculator
  {
    private static readonly Money Ceiling = new Money(9_000.00m);
    private readonly TaxRate _employerRate;

    public UnemploymentTaxes(TaxRate employerRate)
    {
      _employerRate = employerRate;
    }

    public Task<TaxAssessment?> CalculateAsync(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate)
    {
      if (employee.Residence.State != "Michigan")
      {
        return Task.FromResult<TaxAssessment?>(null);
      }

      return Task.FromResult<TaxAssessment?>(new TaxAssessment("Michigan Unemployment Insurance Tax", grossPay.GetTaxOnPayWithCap(grossPayToDate, UnemploymentTaxes.Ceiling, _employerRate), Money.Zero));
    }
  }
}
