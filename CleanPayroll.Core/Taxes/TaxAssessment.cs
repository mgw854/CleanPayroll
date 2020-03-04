namespace CleanPayroll.Core.Taxes
{
  public struct TaxAssessment
  {
    public TaxAssessment(string name, Money employerContribution, Money employeeContribution)
    {
      this.Name = name;
      this.EmployerContribution = employerContribution;
      this.EmployeeContribution = employeeContribution;
    }

    public string Name { get; }
    public Money EmployerContribution { get; }
    public Money EmployeeContribution { get; }
  }
}