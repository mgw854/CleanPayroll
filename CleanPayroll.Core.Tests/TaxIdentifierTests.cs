using System;
using Xunit;

namespace CleanPayroll.Core.Tests
{
  public class TaxIdentifierTests
  {
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("0123")]
    [InlineData("123-456-789")]
    [InlineData("x12345678z")]
    public void SocialSecurityNumber_InvalidInput_ThrowsException(string ssn)
    {
      Assert.Throws<ArgumentException>(() => new SocialSecurityNumber(ssn));
    }

    [Fact]
    public void SocialSecurityNumber_ValidInput_ReturnsCorrectStrings()
    {
      SocialSecurityNumber ssn = new SocialSecurityNumber("123456789");

      Assert.Equal("***-**-6789", ssn.ToString());
      Assert.Equal("***-**-6789", ssn.GetMaskedValue());
      Assert.Equal("123-45-6789", ssn.GetFormattedSecureValue());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("0123")]
    [InlineData("12-3456789")]
    [InlineData("x12345678z")]
    public void EmployerIdentificationNumber_InvalidInput_ThrowsException(string ein)
    {
      Assert.Throws<ArgumentException>(() => new EmployerIdentificationNumber(ein));
    }

    [Fact]
    public void EmployerIdentificationNumber_ValidInput_ReturnsCorrectStrings()
    {
      EmployerIdentificationNumber ein = new EmployerIdentificationNumber("891234567");

      Assert.Equal("**-***4567", ein.ToString());
      Assert.Equal("**-***4567", ein.GetMaskedValue());
      Assert.Equal("89-1234567", ein.GetFormattedSecureValue());
    }
  }
}
