namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public readonly struct TaxBracket
  {
    public TaxBracket(Money floor, TaxRate rate)
    {
      this.Floor = floor;
      this.Rate = rate;
    }

    public Money Floor { get; }
    public TaxRate Rate { get; }
  }
}