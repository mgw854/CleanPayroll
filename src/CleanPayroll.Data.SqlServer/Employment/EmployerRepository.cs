using System.Collections.Generic;
using System.Threading.Tasks;
using CleanPayroll.Core;

namespace CleanPayroll.Data.SqlServer.Employment
{
  public sealed class EmployerRepository : IEmployerRepository
  {
    private readonly SqlServerConnectionFactory _factory;

    public EmployerRepository(SqlServerConnectionFactory factory)
    {
      _factory = factory;
    }

    public Task<IReadOnlyCollection<Employer>> GetEmployersAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task AddEmployerAsync(Employer employer)
    {
      throw new System.NotImplementedException();
    }
  }
}
