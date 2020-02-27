using CleanPayroll.Core.Taxes;
using CleanPayroll.Core.Taxes.UnitedStates.Michigan;
using Xunit;

namespace CleanPayroll.Core.Tests.Taxes.UnitedStates.Michigan
{
  public class UnemploymentTaxesTests
  {
    [Fact]
    public void Calculate_NonMichiganResident_NoTaxes()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      Assert.Null(calc.Calculate(null, null, new Employee("John Q. Doe", new SocialSecurityNumber("123456789"), new StreetAddress("123 Main Street", "New York", "New York")), new Money(100m), Money.Zero));
    }

    [Fact]
    public void Calculate_MichiganResidentOver9000k_PaysNothing()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      TaxAssessment assessed = calc.Calculate(null, null,
        new Employee("John Q. Doe", new SocialSecurityNumber("123456789"),
          new StreetAddress("123 Main Street", "Detroit", "Michigan")), new Money(1_000m), new Money(10_000m)).Value;

      Assert.Equal(Money.Zero, assessed.EmployeeContribution);
      Assert.Equal(Money.Zero, assessed.EmployerContribution);
    }

    [Fact]
    public void Calculate_MichiganResidentFirstPay_PaysFullAmount()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      TaxAssessment assessed = calc.Calculate(null, null,
        new Employee("John Q. Doe", new SocialSecurityNumber("123456789"),
          new StreetAddress("123 Main Street", "Detroit", "Michigan")), new Money(1_000m), Money.Zero).Value;

      Assert.Equal(Money.Zero, assessed.EmployeeContribution);
      Assert.Equal(new Money(47.00m), assessed.EmployerContribution);
    }


    [Fact]
    public void Calculate_MichiganResidentNear9k_PaysPartialAmount()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      TaxAssessment assessed = calc.Calculate(null, null,
        new Employee("John Q. Doe", new SocialSecurityNumber("123456789"),
          new StreetAddress("123 Main Street", "Detroit", "Michigan")), new Money(1_000m), new Money(8_500m)).Value;

      Assert.Equal(Money.Zero, assessed.EmployeeContribution);
      Assert.Equal(new Money(23.50m), assessed.EmployerContribution);
    }
  }
}
