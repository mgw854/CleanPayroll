using Xunit;

namespace CleanPayroll.Core.Tests
{
  public class MoneyTests
  {
    [Fact]
    public void Money_Addition()
    {
      Assert.Equal(new Money(8.01m), new Money(5.00m) + new Money(3.01m));
    }

    [Fact]
    public void Money_Subtract()
    {
      Assert.Equal(new Money(1.99m), new Money(5.00m) - new Money(3.01m));
    }

    [Fact]
    public void Money_MultiplyTaxRate()
    {
      Assert.Equal(new Money(0.06m), new Money(1.00m) * new TaxRate(0.06m));
    }
  }
}
