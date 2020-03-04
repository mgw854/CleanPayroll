using NodaTime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanPayroll.Core
{
  public interface IEmployeeRepository
  {
    Task HireEmployeeAsync(Employee employee);
    Task FireEmployeeAsync(EmployerIdentificationNumber ein, SocialSecurityNumber ssn, LocalDate endDate);
    Task<Employee> GetEmployeeAsync(EmployerIdentificationNumber ein, SocialSecurityNumber ssn);
    Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(EmployerIdentificationNumber ein);
    Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(EmployerIdentificationNumber ein, DateInterval employedRange);
  }
}
