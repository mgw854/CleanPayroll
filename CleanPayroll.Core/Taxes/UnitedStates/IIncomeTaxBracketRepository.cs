using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanPayroll.Core.Taxes.UnitedStates
{
  public interface IIncomeTaxBracketRepository
  {
    Task<IReadOnlyCollection<TaxBracket>> GetBracketsAsync(int year, FilingStatus filingStatus);
  }
}
