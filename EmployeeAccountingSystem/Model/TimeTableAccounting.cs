using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAccountingSystem.Model
{
  /// <summary>
  /// Учет табельного времени.
  /// </summary>
  public class TimeTableAccounting
  {
    #region Поля и свойства

    /// <summary>
    /// Единственный экземпляр класса.
    /// </summary>
    private static TimeTableAccounting instance;

    /// <summary>
    /// Список тарифов.
    /// </summary>
    private List<Tariff> tariffList;

    /// <summary>
    /// Список профессий.
    /// </summary>
    private List<Profession> professions;

    /// <summary>
    /// Список сотрудников.
    /// </summary>
    private List<Employee> employees;

    /// <summary>
    /// Список табелей учетного времени.
    /// </summary>
    private List<Timesheet> timesheets;

    /// <summary>
    /// Получить единственный экземпляр класса.
    /// </summary>
    /// <returns>Единственный экземпляр класса.</returns>
    public static TimeTableAccounting Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new TimeTableAccounting();
        }
        return instance;
      }
    }

    /// <summary>
    /// Список табелей учета рабочего времени.
    /// </summary>
    public static List<Timesheet> Timesheets { get; private set; }

    #endregion

    #region Методы

    /// <summary>
    /// Добавить тариф.
    /// </summary>
    /// <param name="tariff">Тариф.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задан тариф для ввода в справочник тарифов.</exception>
    /// <exception cref="ArgumentException">Ошибка добавления в справочник тарифов - такой тариф уже существует.</exception>
    public void AddTariff(Tariff tariff)
    {
      if (tariff == null)
      {
        throw new ArgumentNullException("Не задан тариф для ввода в справочник тарифов.");
      }

      if (tariffList.Where(e => e.Id == tariff.Id).FirstOrDefault() == null)
      {
        this.tariffList.Add(tariff);
      }
      else
      {
        throw new ArgumentException($"Тариф {tariff.Id} уже есть в справочнике.");
      }
    }

    /// <summary>
    /// Добавить профессию.
    /// </summary>
    /// <param name="profession">Профессия.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задана профессия для добавления в справочник профессий.</exception>
    /// <exception cref="ArgumentException">Ошибка добавления в справочник профессий - такая профессия уже существует.</exception>
    public void AddProfession(Profession profession)
    {
      if (profession == null)
      {
        throw new ArgumentNullException("Не задана профессия для ввода в справочник профессий.");
      }

      if (professions.Where(e => e.Name == profession.Name).FirstOrDefault() == null)
      {
        this.professions.Add(profession);
      }
      else
      {
        throw new ArgumentException($"Профессия {profession.Name} уже есть в справочнике.");
      }
    }

    /// <summary>
    /// Добавить сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задан работник для добавления в справочник работников.</exception>
    /// <exception cref="ArgumentException">Ошибка добавления в справочник сотрудников - сотрудник с таким табельным номером уже существует.</exception>
    public void AddEmployee(Employee employee)
    {
      if (employee == null)
      {
        throw new ArgumentNullException("Не задан работник для добавления в справочник работников.");
      }

      if (employees.Where(e => e.TabelNumber == employee.TabelNumber).FirstOrDefault() == null)
      {
        this.employees.Add(employee);
      }
      else
      {
        throw new ArgumentException($"Сотрудник с табельным номером {employee.TabelNumber} уже есть в справочнике.");
      }
    }

    /// <summary>
    /// Добавить табель учета рабочего времени.
    /// </summary>
    /// <param name="timesheet"></param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задан табель учета рабочего времени для добавления в справочник табелей.</exception>
    /// <exception cref="ArgumentException">Ошибка возникает, если табель уже заведен.</exception>
    public void AddTimesheet(Timesheet timesheet)
    {
      if (timesheet == null)
      {
        throw new ArgumentNullException("Не задан табель учета рабочего времени.");
      }

      if (timesheets.Where(e => e.Employee.TabelNumber == timesheet.Employee.TabelNumber
        && e.Month == timesheet.Month).FirstOrDefault() == null)
      {
        timesheets.Add(timesheet);
      }
      else
      {
        throw new ArgumentException($"Табель на сотрудника {timesheet.Employee.Name} за {timesheet.Month} месяц уже заведен.");
      }
    }

    /// <summary>
    /// Установить отработанное время работника за месяц.
    /// </summary>
    /// <param name="tabelNumber">Табельный номер.</param>
    /// <param name="month">Месяц.</param>
    /// <param name="timeWorked">Отработанное время.</param>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если не найден работник по идентификатору либо неверно задано отработанное время либо неверно задан месяц.</exception>
    public void SetTimesheet(int tabelNumber, int month, int timeWorked)
    {
      var employee = employees.Where(e => e.TabelNumber == tabelNumber).FirstOrDefault();

      if (employee == null)
      {
        throw new ArgumentOutOfRangeException($"Работник с табельным номером {tabelNumber} не найден.");
      }

      var timesheet = timesheets.Where(e => e.Employee.TabelNumber == tabelNumber && e.Month == month).FirstOrDefault();
      if (timesheet == null)
      {
        try
        {
          timesheet = new Timesheet(employee, timeWorked, month);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          throw;
        }
        timesheets.Add(timesheet);
      }
      else
      {
        try
        {
          timesheet.SetTimeWorked(timeWorked);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          throw;
        }
      }
    }

    /// <summary>
    /// Изменить профессию работника.
    /// </summary>
    /// <param name="tabelNumber">Табельный номер работника.</param>
    /// <param name="newProfession">Новая профессия.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задана новая профессия.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если работник или профессия не найдены в справочнике.</exception>
    public void SetProfessionEmployee(int tabelNumber, Profession newProfession)
    {
      if (newProfession == null)
      {
        throw new ArgumentNullException("Не задана профессия.");
      }

      var employee = employees.Where(e => e.TabelNumber == tabelNumber).FirstOrDefault();

      if (employee == null)
      {
        throw new ArgumentOutOfRangeException($"Работник с табельным номером {tabelNumber} не найден.");
      }

      if (professions.Where(e => e.Name == newProfession.Name).FirstOrDefault() == null)
      {
        throw new ArgumentOutOfRangeException($"Профессия {newProfession.Name} не найдена в справочнике.");
      }

      employee.SetProfession(newProfession);
    }

    /// <summary>
    /// Задать новый тариф для профессии.
    /// </summary>
    /// <param name="profession">Профессия.</param>
    /// <param name="newTariff">Новый тариф.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задана профессия или тариф.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если профессия или тариф не найдены в справочнике.</exception>
    public void SetTariffProfession(Profession profession, Tariff newTariff)
    {
      if (profession == null)
      {
        throw new ArgumentNullException("Не задана профессия.");
      }

      if (newTariff == null)
      {
        throw new ArgumentNullException("Не задан новый тариф.");
      }

      if (professions.Where(e => e.Name == profession.Name) == null)
      {
        throw new ArgumentOutOfRangeException($"Профессия {profession.Name} не найдена в справочнике.");
      }

      if (tariffList.Where(e => e.Id == newTariff.Id) == null)
      {
        throw new ArgumentOutOfRangeException($"Тариф {newTariff.Id} не найден в справочнике.");
      }

      profession.SetTariff(newTariff);
    }

    /// <summary>
    /// Установить новое значение тарифа.
    /// </summary>
    /// <param name="tariff">Тариф.</param>
    /// <param name="newValue">Новое значение.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задан тариф.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если указанный тариф не найден в справочнике либо неверно задано значение тарифа.</exception>
    public void SetValueTariff(Tariff tariff, double newValue)
    {
      if (tariff == null)
      {
        throw new ArgumentNullException("Не задан тариф.");
      }

      if (tariffList.Where(e => e.Id == tariff.Id) == null)
      {
        throw new ArgumentOutOfRangeException($"Тариф {tariff.Id} не найден в справочнике.");
      }

      try
      {
        tariff.SetValue(newValue);
      }
      catch (ArgumentOutOfRangeException)
      {
        throw;
      }
    }

    /// <summary>
    /// Распечатать зарплаты сотрудников за месяц.
    /// </summary>
    /// <param name="month">Месяц.</param>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если неверно задан месяц.</exception>
    public void PrintSalaries(int month)
    {
      if (month <= 0 || month > 12)
      {
        throw new ArgumentOutOfRangeException("Неверно задан месяц.");
      }

      Console.WriteLine($"Зарплата сотрудников за {month} месяц.");

      foreach (var item in employees)
      {
        double salary = 0;
        var timesheet = timesheets.Where((e) => e.Employee.TabelNumber == item.TabelNumber && e.Month == month).FirstOrDefault();
        if (timesheet != null)
        {
          salary = timesheet.GetSalary();
        }

        Console.WriteLine($"Табельный номер: {item.TabelNumber}; Сотрудник: {item.Name}; Зарплата: {salary}");
      }

      Console.ReadLine();
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    private TimeTableAccounting()
    {
      tariffList = new List<Tariff>();
      professions = new List<Profession>();
      employees = new List<Employee>();
      timesheets = new List<Timesheet>();
    }

    #endregion
  }
}
