using System.Threading.Tasks;
using NodaTime;

namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public sealed class IncomeTax : ITaxCalculator
  {
    private readonly MarginalEffectiveRateCalculator _rateCalculator = new MarginalEffectiveRateCalculator();
    private readonly IPayEstimator _estimator;
    private readonly IIncomeTaxBracketRepository _taxBracketRepository;

    public IncomeTax(IIncomeTaxBracketRepository taxBracketRepository, IPayEstimator estimator)
    {
      _taxBracketRepository = taxBracketRepository;
      _estimator = estimator;
    }

    public async Task<TaxAssessment?> CalculateAsync(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate)
    {
      TaxRate effective = _rateCalculator.CalculateEffectiveRate(await _taxBracketRepository.GetBracketsAsync(interval.End.Year, context.FilingStatus),
        await _estimator.EstimateSalaryOverYearAsync(grossPayToDate, interval, employee.PayCycle, grossPay));

      return new TaxAssessment("Federal Income Tax", Money.Zero, grossPay * effective);
    }
  }
}
