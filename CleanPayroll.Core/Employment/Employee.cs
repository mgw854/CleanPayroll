using CleanPayroll.Core.Taxes;
using NodaTime;
using System;
using System.Collections.Generic;

namespace CleanPayroll.Core
{
  public sealed class Employee : IEquatable<Employee>
  {
    public Employee(EmployerIdentificationNumber employer, SocialSecurityNumber ssn, string fullName, StreetAddress address, LocalDate startDate, LocalDate? endDate, Money salary, PayCycle payCycle, FilingStatus filingStatus)
    {
      this.Employer = employer;
      this.SSN = ssn;
      this.FullName = fullName;
      this.Residence = address;
      this.StartDate = startDate;
      this.EndDate = endDate;
      this.Salary = salary;
      this.PayCycle = payCycle;
      this.FilingStatus = filingStatus;
    }

    public EmployerIdentificationNumber Employer { get; }
    public SocialSecurityNumber SSN { get; }
    public string FullName { get; }
    public StreetAddress Residence { get; }
    public LocalDate StartDate { get; }
    public LocalDate? EndDate { get; }
    public Money Salary { get; }
    public PayCycle PayCycle { get; }
    public FilingStatus FilingStatus { get; }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as Employee);
    }

    public bool Equals(Employee other)
    {
      return other != null &&
             EqualityComparer<EmployerIdentificationNumber>.Default.Equals(this.Employer, other.Employer) &&
             EqualityComparer<SocialSecurityNumber>.Default.Equals(this.SSN, other.SSN);
    }

    public override int GetHashCode()
    {
      var hashCode = -1021537125;
      hashCode = hashCode * -1521134295 + EqualityComparer<EmployerIdentificationNumber>.Default.GetHashCode(this.Employer);
      hashCode = hashCode * -1521134295 + EqualityComparer<SocialSecurityNumber>.Default.GetHashCode(this.SSN);
      return hashCode;
    }

    public static bool operator ==(Employee left, Employee right)
    {
      return EqualityComparer<Employee>.Default.Equals(left, right);
    }

    public static bool operator !=(Employee left, Employee right)
    {
      return !(left == right);
    }
  }
}
