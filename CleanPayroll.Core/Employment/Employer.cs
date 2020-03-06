using System;
using System.Collections.Generic;

namespace CleanPayroll.Core
{
  public sealed class Employer : IEquatable<Employer>
  {
    public Employer(EmployerIdentificationNumber ein, string legalName, StreetAddress address)
    {
      this.EIN = ein;
      this.LegalName = legalName;
      this.Address = address;
    }

    public EmployerIdentificationNumber EIN { get; }
    public string LegalName { get; }
    public StreetAddress Address { get; }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as Employer);
    }

    public bool Equals(Employer other)
    {
      return other != null &&
             EqualityComparer<EmployerIdentificationNumber>.Default.Equals(this.EIN, other.EIN);
    }

    public override int GetHashCode()
    {
      return -87440617 + EqualityComparer<EmployerIdentificationNumber>.Default.GetHashCode(this.EIN);
    }

    public static bool operator ==(Employer left, Employer right)
    {
      return EqualityComparer<Employer>.Default.Equals(left, right);
    }

    public static bool operator !=(Employer left, Employer right)
    {
      return !(left == right);
    }
  }
}
