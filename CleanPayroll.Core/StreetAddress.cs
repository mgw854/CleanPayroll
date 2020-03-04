using System;
using System.Collections.Generic;

namespace CleanPayroll.Core
{
  public sealed class StreetAddress : IEquatable<StreetAddress>
  {
    public StreetAddress(string address, string city, string state)
    {
      this.Address = address;
      this.City = city;
      this.State = state;
    }

    public string Address { get; }
    public string City { get; }
    public string State { get; }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as StreetAddress);
    }

    public bool Equals(StreetAddress other)
    {
      return other != null &&
             this.Address == other.Address &&
             this.City == other.City &&
             this.State == other.State;
    }

    public override int GetHashCode()
    {
      var hashCode = -71070229;
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Address);
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.City);
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.State);
      return hashCode;
    }

    public static bool operator ==(StreetAddress left, StreetAddress right)
    {
      return EqualityComparer<StreetAddress>.Default.Equals(left, right);
    }

    public static bool operator !=(StreetAddress left, StreetAddress right)
    {
      return !(left == right);
    }
  }
}