using System.Collections.Generic;
using System.Threading.Tasks;
using CleanPayroll.Core.Taxes;
using CleanPayroll.Core.Taxes.UnitedStates;

namespace CleanPayroll.Data.SqlServer.Taxes.UnitedStates
{
  public sealed class IncomeTaxBracketRepository : IIncomeTaxBracketRepository
  {
    private readonly SqlServerConnectionFactory _factory;

    public IncomeTaxBracketRepository(SqlServerConnectionFactory factory)
    {
      _factory = factory;
    }

    public Task<IReadOnlyCollection<TaxBracket>> GetBracketsAsync(int year, FilingStatus filingStatus)
    {
      throw new System.NotImplementedException();
    }
  }
}
