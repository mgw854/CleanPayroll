using System.Collections.Generic;
using System.Linq;
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

    public abstract int PaysPerYear { get; }
    public IReadOnlyList<LocalDate> GetPayDates(int year, DateInterval interval)
    {
      return this.GetPayDates(year).Where(date => date > interval.Start && date <= interval.End).ToList();
    }
  }

  public sealed class BiweeklyPayCycle : PayCycle
  {
    public BiweeklyPayCycle() : base("Bi-weekly")
    {
    }

    public override int PaysPerYear => 26;

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

    public override int PaysPerYear => 24;

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

    public override int PaysPerYear => 12;

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
