using CleanPayroll.Core.Taxes;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanPayroll.Core.Taxes.UnitedStates;
using CleanPayroll.Core.Taxes.UnitedStates.Michigan;
using Xunit;

namespace CleanPayroll.Core.Tests
{
  public class EndToEndTests
  {
    [Fact]
    public async Task HireEmployeeAndPay()
    {
      // Employer setup
      Employer employer = new Employer(new EmployerIdentificationNumber("111222345"), "End-to-End Test Employer", new StreetAddress("123 Main Street", "Troy", "Michigan"));
      IEmployerRepository employerRepository = new FakeEmployerRepository();
      await employerRepository.AddEmployerAsync(employer);

      // Employee setup
      Employee employee = new Employee(employer.EIN, new SocialSecurityNumber("123456789"), "Joe Bethersonton", new StreetAddress("11454 Pruder Street Apt. 23-R", "Fargo", "Michigan"), new NodaTime.LocalDate(2020, 01, 01), null, new Money(100_000m), new BiweeklyPayCycle(), Core.Taxes.FilingStatus.Single);
      IEmployeeRepository employeeRepository = new FakeEmployeeRepository();
      await employeeRepository.HireEmployeeAsync(employee);

      // Pay employee
      LocalDate today = new LocalDate(2020, 02, 01);

      // Generate pay checks per employee
      List<ITaxCalculator> taxCalculators = new List<ITaxCalculator>(){
        new IncomeTax(new FakeIncomeTaxBracketRepository(), new CeterisParibusPayEstimator()),
        new UnemploymentTaxes(new TaxRate(0.05m)),
        new MedicareTaxes(),
        new SocialSecurityTaxes()
      };
      PayrollGenerator generator = new PayrollGenerator(new FakePayrollRepository(), employeeRepository, taxCalculators);
      IReadOnlyCollection<Paycheck> checks = await generator.GeneratePaychecksAsync(employer.EIN, today);
    }
  }

  public class FakeEmployerRepository : IEmployerRepository
  {
    private readonly List<Employer> _employers = new List<Employer>();

    public Task AddEmployerAsync(Employer employer)
    {
      _employers.Add(employer);
      return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Employer>> GetEmployersAsync()
    {
      return Task.FromResult<IReadOnlyCollection<Employer>>(_employers);
    }
  }

  public class FakeEmployeeRepository : IEmployeeRepository
  {
    private readonly List<Employee> _employees = new List<Employee>();

    public Task FireEmployeeAsync(EmployerIdentificationNumber ein, SocialSecurityNumber ssn, LocalDate endDate)
    {
      Employee e = _employees.FirstOrDefault(e => e.Employer == ein && e.SSN == ssn);
      _employees.Remove(e);
      _employees.Add(new Employee(e.Employer, e.SSN, e.FullName, e.Residence, e.StartDate, endDate, e.Salary, e.PayCycle, e.FilingStatus));
      return Task.CompletedTask;
    }

    public Task<Employee> GetEmployeeAsync(EmployerIdentificationNumber ein, SocialSecurityNumber ssn)
    {
      return Task.FromResult(_employees.FirstOrDefault(e => e.Employer == ein && e.SSN == ssn));
    }

    public Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(EmployerIdentificationNumber ein)
    {
      return Task.FromResult<IReadOnlyCollection<Employee>>(_employees.Where(e => e.Employer == ein).ToList());
    }

    public Task<IReadOnlyCollection<Employee>> GetEmployeesAsync(EmployerIdentificationNumber ein, DateInterval employedRange)
    {
      var filter = _employees.Where(e =>
      {
        if (e.Employer != ein)
        {
          return false;
        }

        // TODO clarify this

        if (e.EndDate.HasValue)
        {
          return e.EndDate <= employedRange.End;
        }

        return e.StartDate < employedRange.End;
      }).ToList();
      return Task.FromResult<IReadOnlyCollection<Employee>>(filter);
    }

    public Task HireEmployeeAsync(Employee employee)
    {
      _employees.Add(employee);
      return Task.CompletedTask;
    }
  }
}
