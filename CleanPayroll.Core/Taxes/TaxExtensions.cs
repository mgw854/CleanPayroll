namespace CleanPayroll.Core.Taxes
{
  public static class TaxExtensions
  {
    public static Money GetTaxOnPayWithCap(this Money grossPay, Money grossPayToDate, Money cap, TaxRate rate)
    {
      if (grossPayToDate > cap)
      {
        return Money.Zero;
      }

      Money remainingTaxablePay = Money.Min(cap - grossPayToDate, grossPay);

      return remainingTaxablePay * rate;
    }
  }
}
