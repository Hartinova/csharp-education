using Phonebook.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book = Phonebook.Model.Phonebook;

namespace Phonebook
{
  internal class Program
  {
    static void Main(string[] args)
    {
      CreateFileForTest(); 

      Book phonebook = Book.Instance;

      Console.WriteLine("Cписок абонентов из файла:");
      phonebook.PrintAbonents();

      string nameForFind = "тимур";
      if (phonebook.GetPhoneNumberByName(nameForFind, out string number))
      {
        Console.WriteLine($"Номер телефона искомого абонента {nameForFind}: {number}");
      }
      else
      {
        Console.WriteLine($"Абонент {nameForFind} не найден.");
      }
      Console.ReadLine();

      string numberForFind = "81221234569";
      if (phonebook.GetNameByPhoneNumber(numberForFind, out string name))
      {
        Console.WriteLine($"Имя абонента с номером телефона {numberForFind}: {name}");
      }
      else
      {
        Console.WriteLine($"Абонент с номером {numberForFind} не найден.");
      }
      Console.ReadLine();

      string nameForAdd = "Влад";
      string numberForAdd = "12333612322";
      phonebook.AddAbonent(nameForAdd, numberForAdd);
      Console.WriteLine($"Добавлен абонент {nameForAdd}:");
      phonebook.PrintAbonents();

      var abonent2 = phonebook.Abonents[2];
      phonebook.DeleteAbonent(abonent2);
      Console.WriteLine($"Удален абонент {abonent2.Id}:");
      phonebook.PrintAbonents();

      var abonent = phonebook.Abonents[1];
      abonent.ChangeName("Лена");
      Console.WriteLine($"У абонента {abonent.Id} изменено имя:");
      phonebook.PrintAbonents();

      abonent.ChangeNumber("99999999999");
      Console.WriteLine($"У абонента {abonent.Id} изменен номер:");
      phonebook.PrintAbonents();

      phonebook.SaveToFile();
    }

    /// <summary>
    /// Создать текстовый файл абонентской книги для тестирования приложения.
    /// </summary>
    public static void CreateFileForTest()
    {
      if (File.Exists(Book.FilePhonebook))
      {
        File.Delete(Book.FilePhonebook);
      }

      try
      {
        StreamWriter sw = new StreamWriter(Book.FilePhonebook);

        sw.WriteLine($"1;Камилла;81221234569");
        sw.WriteLine($"2;Оксана;81221111169");
        sw.WriteLine($"3;Тимур;81221232222");

        sw.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      }
    }
  }
}
