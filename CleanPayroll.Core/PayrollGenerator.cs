using CleanPayroll.Core.Taxes;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanPayroll.Core
{
  public sealed class PayrollGenerator
  {
    private readonly IPayrollRepository _payrollRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IReadOnlyCollection<ITaxCalculator> _taxCalculators;

    public PayrollGenerator(IPayrollRepository payrollRepo, IEmployeeRepository employeeRepo, IReadOnlyCollection<ITaxCalculator> taxCalculators)
    {
      _payrollRepo = payrollRepo;
      _employeeRepo = employeeRepo;
      _taxCalculators = taxCalculators;
    }

    public async Task<IReadOnlyCollection<Paycheck>> GeneratePaychecksAsync(EmployerIdentificationNumber ein, LocalDate payDay)
    {
      // Get all employees that were active between the last check date and today
      (int checkNo, LocalDate lastPayrollRan) = await _payrollRepo.GetLastCheckDetailsAsync(ein);

      var employees = await _employeeRepo.GetEmployeesAsync(ein, new DateInterval(lastPayrollRan, payDay));

      DateInterval payInterval = new DateInterval(lastPayrollRan, payDay);

      List<Paycheck> paychecks = new List<Paycheck>();

      foreach (Employee employee in employees)
      {
        LocalDate firstDayOfPay = LocalDate.Max(lastPayrollRan, employee.StartDate);

        foreach (LocalDate payDate in employee.PayCycle.GetPayDates(payDay.Year, payInterval).Where(d => d > employee.StartDate))
        {
          if (employee.EndDate.HasValue && employee.EndDate > firstDayOfPay)
          {
            break;
          }

          // TODO how to handle firing in middle of cycle

          Money grossPay = employee.Salary / employee.PayCycle.PaysPerYear;
          Money netPay = grossPay;

          Money grossYtd = await _payrollRepo.GetGrossPayToDate(ein, employee.SSN, payDate.Year);

          List<TaxAssessment> taxes = new List<TaxAssessment>();

          // Figure out the taxes
          foreach(ITaxCalculator calc in _taxCalculators)
          {
            TaxAssessment? assess = await calc.CalculateAsync(new DateInterval(firstDayOfPay, payDate), new TaxContext(employee.FilingStatus, 0), employee, grossPay, grossYtd);
            if (assess.HasValue)
            {
              taxes.Add(assess.Value);
              netPay -= assess.Value.EmployeeContribution;
            }
          }

          // Cut the check
          Paycheck check = new Paycheck(++checkNo, payDay, employee.SSN, ein, firstDayOfPay, payDate, grossPay, netPay, taxes);

          await _payrollRepo.CutPaycheckAsync(check);

          paychecks.Add(check);

          firstDayOfPay = payDate;
        }
      }

      return paychecks;
    }
  }

  public interface IPayrollRepository
  {
    Task<(int, LocalDate)> GetLastCheckDetailsAsync(EmployerIdentificationNumber ein);
    Task<IReadOnlyDictionary<SocialSecurityNumber, LocalDate>> GetLastPayDateByEmployeeAsync(IReadOnlyCollection<Employee> employees);
    Task CutPaycheckAsync(Paycheck paycheck);
    Task<Money> GetGrossPayToDate(EmployerIdentificationNumber ein, SocialSecurityNumber ssn, int year);
  }

  public sealed class Paycheck
  {
    public Paycheck(int checkNo, LocalDate checkDate, SocialSecurityNumber employee, EmployerIdentificationNumber employer, LocalDate startDate, LocalDate endDate, Money grossPay, Money netPay, IReadOnlyCollection<TaxAssessment> taxes)
    {
      this.CheckNo = checkNo;
      this.CheckDate = checkDate;
      this.Employee = employee;
      this.Employer = employer;
      this.StartDate = startDate;
      this.EndDate = endDate;
      this.GrossPay = grossPay;
      this.NetPay = netPay;
      this.Taxes = taxes;
    }

    public int CheckNo { get; }
    public LocalDate CheckDate { get; }
    public SocialSecurityNumber Employee { get; }
    public EmployerIdentificationNumber Employer { get; }
    public LocalDate StartDate { get; }
    public LocalDate EndDate { get; }
    public Money GrossPay { get; }
    public Money NetPay { get; }
    public IReadOnlyCollection<TaxAssessment> Taxes { get; }
  }
}
