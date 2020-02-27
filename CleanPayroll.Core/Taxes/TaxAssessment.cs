namespace CleanPayroll.Core.Taxes
{
  public struct TaxAssessment
  {
    public TaxAssessment(Money employerContribution, Money employeeContribution)
    {
      this.EmployerContribution = employerContribution;
      this.EmployeeContribution = employeeContribution;
    }

    public Money EmployerContribution { get; }
    public Money EmployeeContribution { get; }
  }
}