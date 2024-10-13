using System;

namespace EmployeeAccountingSystem.Model
{
  /// <summary>
  /// Табель учета рабочего времени.
  /// </summary>
  public class Timesheet
  {
    #region Поля и свойства

    /// <summary>
    /// Табельный номер.
    /// </summary>
    public Employee Employee { get; private set; }

    /// <summary>
    /// Отработанное время в часах.
    /// </summary>
    public int TimeWorked { get; private set; }

    /// <summary>
    /// Месяц.
    /// </summary>
    private int month;

    /// <summary>
    /// Месяц.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если неверно задан месяц.</exception>
    public int Month
    {
      get
      {
        return month;
      }
      private set
      {
        if (value >= 1 && value <= 12)
        {
          month = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException("Неверно задан месяц. Возможные значения от 1 до 12.");
        }
      }
    }

    #endregion

    #region Методы

    /// <summary>
    /// Получить значение зарплаты сотрудника за месяц.
    /// </summary>
    /// <returns>Зарплата сотрудника за месяц.</returns>
    public double GetSalary()
    {
      double? result = 0;
      result = this.TimeWorked * Employee?.Profession?.TariffProfession?.Value;
      return result ?? 0;
    }

    /// <summary>
    /// Установить отработанное время.
    /// </summary>
    /// <param name="timeWorked">Новое отработанное время.</param>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если неверно задано отработанное время.</exception>
    public void SetTimeWorked(int timeWorked)
    {
      if (timeWorked >= 0 && timeWorked <= 250)
      {
        this.TimeWorked = timeWorked;
      }
      else
      {
        throw new ArgumentOutOfRangeException("Неверно задано отработанное время. Возможные значения от 0 до 250.");
      }
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="employee">Работник.</param>
    /// <param name="timeWorked">Отработанное время.</param>
    /// <param name="month">Месяц.</param>
    public Timesheet(Employee employee, int timeWorked, int month)
    {
      this.Employee = employee;
      this.SetTimeWorked(timeWorked);
      this.Month = month;
    }

    #endregion
  }
}
