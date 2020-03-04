using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanPayroll.Core.Tests
{
  internal class FakePayrollRepository : IPayrollRepository
  {
    private readonly List<Paycheck> _paychecks = new List<Paycheck>();
    public Task CutPaycheckAsync(Paycheck paycheck)
    {
      _paychecks.Add(paycheck);
      return Task.CompletedTask;
    }

    public Task<Money> GetGrossPayToDate(EmployerIdentificationNumber ein, SocialSecurityNumber ssn, int year)
    {
      Money ytd = Money.Zero;

      foreach (Paycheck pay in _paychecks.Where(check => check.CheckDate.Year == year && check.Employer == ein && check.Employee == ssn))
      {
        ytd += pay.GrossPay;
      }

      return Task.FromResult(ytd);
    }

    public Task<(int, LocalDate)> GetLastCheckDetailsAsync(EmployerIdentificationNumber ein)
    {
      Paycheck last = _paychecks.Where(check => check.Employer == ein).OrderBy(c => c.CheckNo).LastOrDefault();

      if (last == null)
      {
        return Task.FromResult((0, new LocalDate(2000, 01, 01)));
      }

      return Task.FromResult((last.CheckNo, last.CheckDate));
    }

    public Task<IReadOnlyDictionary<SocialSecurityNumber, LocalDate>> GetLastPayDateByEmployeeAsync(IReadOnlyCollection<Employee> employees)
    {
      HashSet<SocialSecurityNumber> employeeSSNs = employees.Select(e => e.SSN).ToHashSet();
      EmployerIdentificationNumber ein = employees.First().Employer;

      Dictionary<SocialSecurityNumber, LocalDate> lastDates = new Dictionary<SocialSecurityNumber, LocalDate>();

      foreach (var g in _paychecks.Where(c => c.Employer == ein && employeeSSNs.Contains(c.Employee)).GroupBy(c => c.Employee))
      {
        lastDates[g.Key] = g.Select(c => c.CheckDate).Max();
      }

      return Task.FromResult<IReadOnlyDictionary<SocialSecurityNumber, LocalDate>>(lastDates);
    }
  }
}