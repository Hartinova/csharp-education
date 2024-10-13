using EmployeeAccountingSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAccountingSystem
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var timeTableAccounting = TimeTableAccounting.Instance;
      CreateTestData(timeTableAccounting);

      timeTableAccounting.PrintSalaries(9);

      timeTableAccounting.SetTimesheet(1, 8, 80);
      timeTableAccounting.PrintSalaries(8);

      timeTableAccounting.SetTimesheet(1, 9, 80);
      timeTableAccounting.PrintSalaries(9);

     // timeTableAccounting.SetTimesheet(3, 9, 80);

     // timeTableAccounting.SetTimesheet(1, 0, 80);
      timeTableAccounting.SetTimesheet(1, 1, -10);
    }

    /// <summary>
    /// Создать тестовые данные.
    /// </summary>
    public static void CreateTestData(TimeTableAccounting timeTableAccounting)
    {
      var tarif1 = new Tariff(1, 100);
      var tarif2 = new Tariff(2, 50);
      timeTableAccounting.AddTariff(tarif1);
      timeTableAccounting.AddTariff(tarif2);

      var profession1 = new Profession("Программист", tarif1);
      var profession2 = new Profession("Тестировщик", tarif2);
      timeTableAccounting.AddProfession(profession1);
      timeTableAccounting.AddProfession(profession2);

      var employee1 = new Employee(1, "Иванов Иван", profession1);
      var employee2 = new Employee(2, "Петров Пётр", profession2);
      timeTableAccounting.AddEmployee(employee1);
      timeTableAccounting.AddEmployee(employee2);

      var timesheet1 = new Timesheet(employee1, 100, 9);
      var timesheet2=new Timesheet(employee2, 100, 9);
      timeTableAccounting.AddTimesheet(timesheet1);
      timeTableAccounting.AddTimesheet(timesheet2);


    }
  }
}
