using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanPayroll.Core
{
  public interface IEmployerRepository
  {
    Task<IReadOnlyCollection<Employer>> GetEmployersAsync();
    Task AddEmployerAsync(Employer employer);
  }
}
