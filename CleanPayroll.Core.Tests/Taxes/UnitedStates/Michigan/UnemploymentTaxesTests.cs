using System.Threading.Tasks;
using CleanPayroll.Core.Taxes;
using CleanPayroll.Core.Taxes.UnitedStates.Michigan;
using Xunit;

namespace CleanPayroll.Core.Tests.Taxes.UnitedStates.Michigan
{
  public class UnemploymentTaxesTests
  {
    [Fact]
    public async Task Calculate_NonMichiganResident_NoTaxes()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      Assert.Null(await calc.CalculateAsync(null, null, new Employee(new EmployerIdentificationNumber("123456789"), new SocialSecurityNumber("123456789"), "John Q. Doe", new StreetAddress("123 Main Street", "New York", "New York"), new NodaTime.LocalDate(2010, 01, 01), null, new Money(100_000m), new BiweeklyPayCycle(), FilingStatus.Single), new Money(100m), Money.Zero));
    }

    [Fact]
    public async Task Calculate_MichiganResidentOver9000k_PaysNothing()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      TaxAssessment assessed = (await calc.CalculateAsync(null, null,
        new Employee(new EmployerIdentificationNumber("123456789"), new SocialSecurityNumber("123456789"), "John Q. Doe",
          new StreetAddress("123 Main Street", "Detroit", "Michigan"), new NodaTime.LocalDate(2010, 01, 01), null, new Money(100_000m), new BiweeklyPayCycle(), FilingStatus.Single), new Money(1_000m), new Money(10_000m))).Value;

      Assert.Equal(Money.Zero, assessed.EmployeeContribution);
      Assert.Equal(Money.Zero, assessed.EmployerContribution);
    }

    [Fact]
    public async Task Calculate_MichiganResidentFirstPay_PaysFullAmount()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      TaxAssessment assessed = (await calc.CalculateAsync(null, null,
        new Employee(new EmployerIdentificationNumber("123456789"), new SocialSecurityNumber("123456789"), "John Q. Doe",
          new StreetAddress("123 Main Street", "Detroit", "Michigan"), new NodaTime.LocalDate(2010, 01, 01), null, new Money(100_000m), new BiweeklyPayCycle(), FilingStatus.Single), new Money(1_000m), new Money(10_000m))).Value;

      Assert.Equal(Money.Zero, assessed.EmployeeContribution);
      Assert.Equal(new Money(47.00m), assessed.EmployerContribution);
    }


    [Fact]
    public async Task Calculate_MichiganResidentNear9k_PaysPartialAmount()
    {
      UnemploymentTaxes calc = new UnemploymentTaxes(new TaxRate(0.047m));

      TaxAssessment assessed = (await calc.CalculateAsync(null, null,
        new Employee(new EmployerIdentificationNumber("123456789"), new SocialSecurityNumber("123456789"), "John Q. Doe",
          new StreetAddress("123 Main Street", "Detroit", "Michigan"), new NodaTime.LocalDate(2010, 01, 01), null, new Money(100_000m), new BiweeklyPayCycle(), FilingStatus.Single), new Money(1_000m), new Money(10_000m))).Value;

      Assert.Equal(Money.Zero, assessed.EmployeeContribution);
      Assert.Equal(new Money(23.50m), assessed.EmployerContribution);
    }
  }
}
