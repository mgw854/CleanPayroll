using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanPayroll.Core;
using NodaTime;

namespace CleanPayroll.Data.SqlServer.Employment
{
  public sealed class EmployeeRepository : IEmployeeRepository
  {
    private readonly SqlServerConnectionFactory _factory;

    public EmployeeRepository(SqlServerConnectionFactory factory)
    {
      _factory = factory;
    }

    public Task HireEmployeeAsync(Employee employee)
    {
      throw new NotImplementedException();
    }

    public Task FireEmployeeAsync(EmployerIdentificationNumber ein, SocialSecurityNumber ssn, LocalDate endDate)
    {
      throw new NotImplementedException();
    }

    public Task<Employee> GetEmployeeAsync(EmployerIdentificationNumber ein, SocialSecurityNumber ssn)
    {
      throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(EmployerIdentificationNumber ein)
    {
      throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(EmployerIdentificationNumber ein, DateInterval employedRange)
    {
      throw new NotImplementedException();
    }
  }
}
