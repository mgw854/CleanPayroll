namespace CleanPayroll.Core
{
  public sealed class Employee
  {
    public Employee(string fullName, SocialSecurityNumber ssn, StreetAddress residence)
    {
      this.FullName = fullName;
      this.SSN = ssn;
      this.Residence = residence;
    }

    public string FullName { get; }
    public SocialSecurityNumber SSN { get; }
    public StreetAddress Residence { get; }
  }
}
