using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanPayroll.Core
{
  public abstract class TaxIdentifier : IEquatable<TaxIdentifier>
  {
    protected readonly string _value;

    protected TaxIdentifier(string value)
    {
      _value = value;
    }

    public string GetMaskedValue() => this.ToString();
    public abstract string GetFormattedSecureValue();

    public override bool Equals(object obj)
    {
      return this.Equals(obj as TaxIdentifier);
    }

    public bool Equals(TaxIdentifier other)
    {
      return other != null &&
             _value == other._value;
    }

    public override int GetHashCode()
    {
      return -1939223833 + EqualityComparer<string>.Default.GetHashCode(_value);
    }

    public static bool operator ==(TaxIdentifier left, TaxIdentifier right)
    {
      return EqualityComparer<TaxIdentifier>.Default.Equals(left, right);
    }

    public static bool operator !=(TaxIdentifier left, TaxIdentifier right)
    {
      return !(left == right);
    }
  }

  public sealed class SocialSecurityNumber : TaxIdentifier
  {
    public SocialSecurityNumber(string ssn) : base(ssn)
    {
      if (string.IsNullOrEmpty(ssn))
      {
        throw new ArgumentException("The SSN cannot be null or empty.");
      }

      if (ssn.Length != 9 || ssn.ToCharArray().Any(c => c < '0' || c > '9'))
      {
        throw new ArgumentException("The SSN cannot contain anything but nine digits (0-9).");
      }
    }

    public override string ToString()
    {
      return "***-**-" + _value.Substring(5);
    }

    public override string GetFormattedSecureValue()
    {
      return _value.Substring(0, 3) + "-" + _value.Substring(3, 2) + "-" + _value.Substring(5);
    }
  }

  public sealed class EmployerIdentificationNumber : TaxIdentifier
  {
    public EmployerIdentificationNumber(string ein) : base(ein)
    {
      if (string.IsNullOrEmpty(ein))
      {
        throw new ArgumentException("The EIN cannot be null or empty.");
      }

      if (ein.Length != 9 || ein.ToCharArray().Any(c => c < '0' || c > '9'))
      {
        throw new ArgumentException("The EIN cannot contain anything but nine digits (0-9).");
      }
    }

    public override string ToString()
    {
      return "**-***" + _value.Substring(5);
    }

    public override string GetFormattedSecureValue()
    {
      return _value.Substring(0, 2) + "-" + _value.Substring(2);
    }
  }
}
