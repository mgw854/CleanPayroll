namespace CleanPayroll.Core
{
  public sealed class StreetAddress
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
  }
}