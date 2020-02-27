using NodaTime;

namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public sealed class MedicareTaxes : ITaxCalculator
  {
    public static readonly TaxRate Rate = new TaxRate(0.017m);
    public static readonly TaxRate AdditionalRate = new TaxRate(0.009m);
    public static readonly Money AdditionalRateStart = new Money(200_000m);

    public TaxAssessment? Calculate(DateInterval interval, TaxContext context, Employee employee, Money grossPay, Money grossPayToDate)
    {
      Money tax = grossPay * MedicareTaxes.Rate;
      Money amt = Money.Zero;

      if (grossPayToDate > MedicareTaxes.AdditionalRateStart)
      {
        amt = grossPay * AdditionalRate;
      }
      else if (grossPayToDate + grossPay > MedicareTaxes.AdditionalRateStart)
      {
        Money diff = grossPayToDate + grossPay - MedicareTaxes.AdditionalRateStart;
        amt = diff * MedicareTaxes.AdditionalRate;
      }

      return new TaxAssessment(tax, tax + amt);
    }
  }
}
