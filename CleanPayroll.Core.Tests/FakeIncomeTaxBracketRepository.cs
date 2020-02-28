using System.Collections.Generic;
using System.Threading.Tasks;
using CleanPayroll.Core.Taxes;
using CleanPayroll.Core.Taxes.UnitedStates;

namespace CleanPayroll.Core.Tests
{
  public sealed class FakeIncomeTaxBracketRepository : IIncomeTaxBracketRepository
  {
    public async Task<IReadOnlyCollection<TaxBracket>> GetBracketsAsync(int year, FilingStatus filingStatus)
    {
      return new List<TaxBracket>()
      {
        new TaxBracket(Money.Zero, new TaxRate(0.1m)),
        new TaxBracket(new Money(9701m), new TaxRate(0.12m)),
        new TaxBracket(new Money(39476m), new TaxRate(0.22m)),
        new TaxBracket(new Money(84201m), new TaxRate(0.24m)),
        new TaxBracket(new Money(160726m), new TaxRate(0.32m)),
        new TaxBracket(new Money(204101m), new TaxRate(0.35m)),
        new TaxBracket(new Money(510301m), new TaxRate(0.37m))
      };
    }
  }
}
