namespace CleanPayroll.Core.Taxes
{
  public sealed class TaxContext
  {
    public TaxContext(FilingStatus filingStatus, byte exemptions)
    {
      this.FilingStatus = filingStatus;
      this.Exemptions = exemptions;
    }

    public FilingStatus FilingStatus { get; }
    public byte Exemptions { get; }
  }
}
