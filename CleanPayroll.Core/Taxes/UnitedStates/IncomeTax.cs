using System.Collections.Generic;
using NodaTime;

namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public sealed class IncomeTax : ITaxCalculator
  {
    private static readonly IReadOnlyList<(Money floor, TaxRate rate)> Rates2019 = new List<(Money floor, TaxRate rate)>()
    {
      {(Money.Zero, new TaxRate(0.1m)) },
      {(new Money(9701m), new TaxRate(0.12m)) },
      {(new Money(39476m), new TaxRate(0.22m)) },
      {(new Money(84201m), new TaxRate(0.24m)) },
      {(new Money(160726m), new TaxRate(0.32m)) },
      {(new Money(204101m), new TaxRate(0.35m)) },
      {(new Money(510301m), new TaxRate(0.37m)) }
    };

    private readonly MarginalEffectiveRateCalculator _rateCalculator = new MarginalEffectiveRateCalculator();
    private readonly PayEstimator _estimator = new PayEstimator();

    public TaxAssessment? Calculate(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate)
    {
      // TODO rates differ by context and year (we should look this up)
      // TODO don't assume bi-weekly
      TaxRate effective = _rateCalculator.CalculateEffectiveRate(Rates2019,
        _estimator.EstimateSalaryOverYear(grossPayToDate, interval, new BiweeklyPayCycle(), grossPay));

      return new TaxAssessment(Money.Zero, grossPay * effective);
    }
  }
}
