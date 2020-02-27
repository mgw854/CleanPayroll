using System.Collections.Generic;
using NodaTime;

namespace CleanPayroll.Core
{
  public abstract class PayCycle
  {
    internal PayCycle(string name)
    {
      this.Name = name;
    }

    public string Name { get; }

    public abstract IReadOnlyList<LocalDate> GetPayDates(int year);
  }

  public sealed class BiweeklyPayCycle : PayCycle
  {
    public BiweeklyPayCycle() : base("Bi-weekly")
    {
    }

    public override IReadOnlyList<LocalDate> GetPayDates(int year)
    {
      List<LocalDate> dates = new List<LocalDate>();
      LocalDate date = DateAdjusters.NextOrSame(IsoDayOfWeek.Friday)(new LocalDate(year, 1, 1));

      while (date.Year == year)
      {
        dates.Add(date);
        date = date.PlusWeeks(2);
      }

      return dates;
    }
  }

  public sealed class SemimonthlyPayCycle : PayCycle
  {
    public SemimonthlyPayCycle() : base("Semi-monthly")
    {
    }

    public override IReadOnlyList<LocalDate> GetPayDates(int year)
    {
      List<LocalDate> dates = new List<LocalDate>();

      for (int month = 1; month <= 12; month++)
      {
        LocalDate date = new LocalDate(year, month, 1);

        if (date.DayOfWeek == IsoDayOfWeek.Saturday || date.DayOfWeek == IsoDayOfWeek.Sunday)
        {
          dates.Add(DateAdjusters.Previous(IsoDayOfWeek.Friday)(date));
        }
        else
        {
          dates.Add(date);
        }

        date = new LocalDate(year, month, 15);
        if (date.DayOfWeek == IsoDayOfWeek.Saturday || date.DayOfWeek == IsoDayOfWeek.Sunday)
        {
          dates.Add(DateAdjusters.Previous(IsoDayOfWeek.Friday)(date));
        }
        else
        {
          dates.Add(date);
        }
      }

      return dates;
    }
  }

  public sealed class MonthlyPayCycle : PayCycle
  {
    public MonthlyPayCycle() : base("Monthly")
    {
    }

    public override IReadOnlyList<LocalDate> GetPayDates(int year)
    {
      List<LocalDate> dates = new List<LocalDate>();

      for (int month = 1; month <= 12; month++)
      {
        LocalDate date = DateAdjusters.EndOfMonth(new LocalDate(year, month, 1));

        if (date.DayOfWeek == IsoDayOfWeek.Saturday || date.DayOfWeek == IsoDayOfWeek.Sunday)
        {
          dates.Add(DateAdjusters.Previous(IsoDayOfWeek.Friday)(date));
        }
        else
        {
          dates.Add(date);
        }
      }

      return dates;
    }
  }
}
