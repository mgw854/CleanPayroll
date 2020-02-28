using CleanPayroll.Core.Taxes;

namespace CleanPayroll.Core
{
  public sealed class Employee
  {
    public Employee(string fullName, SocialSecurityNumber ssn, StreetAddress residence, PayCycle payCycle)
    {
      this.FullName = fullName;
      this.SSN = ssn;
      this.Residence = residence;
      this.PayCycle = payCycle;
    }

    public string FullName { get; }
    public SocialSecurityNumber SSN { get; }
    public StreetAddress Residence { get; }
    public PayCycle PayCycle { get; }
  }
}
