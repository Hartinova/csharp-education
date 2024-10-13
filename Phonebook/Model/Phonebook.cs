using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Phonebook.Model
{
  /// <summary>
  /// Телефонная книга.
  /// </summary>
  public class Phonebook
  {
    /// <summary>
    /// Путь к текстовому файлу с телефонной книгой.
    /// </summary>
    public const string FilePhonebook = "phonebook.txt";

    /// <summary>
    /// Единственный экземпляр класса Телефонная книга.
    /// </summary>
    public static Phonebook phonebook;

    /// <summary>
    /// Список абонентов.
    /// </summary>
    public List<Abonent> Abonents { get; private set; }

    /// <summary>
    /// Получить единственный экземпляр класса Телефонная книга.
    /// </summary>
    /// <returns></returns>
    public static Phonebook Instance
    {
      get
      {
        if (phonebook == null)
        {
          phonebook = new Phonebook();
        }
        return phonebook;
      }
    }

    /// <summary>
    /// Считать абонентов из файла.
    /// </summary>
    /// <returns>Список абонентов.</returns>
    private List<Abonent> ReadAbonentsFromFile()
    {
      Abonents = new List<Abonent>();

      try
      {
        if (File.Exists(FilePhonebook))
        {
          StreamReader sr = new StreamReader(FilePhonebook);

          string line = sr.ReadLine().Trim();
          while (line != null)
          {
            var array = line.Split(';');
            if (int.TryParse(array[0], out int id))
            {
              Abonents.Add(new Abonent(id, array[1], array[2]));
            }
            else
            {
              Abonents.Add(new Abonent(GetNewAbonentId(), array[1], array[2]));
            }

            line = sr.ReadLine();
          }

          sr.Close();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      }

      return Abonents;
    }

    /// <summary>
    /// Получить новый идентификатор для абонента.
    /// </summary>
    /// <returns>Новый идентификатор для абонента.</returns>
    private int GetNewAbonentId()
    {
      if (Phonebook.Instance.Abonents == null || Phonebook.Instance.Abonents.Count() == 0)
      {
        return 1;
      }

      return Phonebook.Instance.Abonents.Max(e => e.Id) + 1;
    }

    /// <summary>
    /// Добавить абонента в список.
    /// </summary>
    /// <param name="name">Имя абонента.</param>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <exception cref="ArgumentException">Исключение вызывается, если абонент уже присутствует в списке абонентов.</exception>
    public void AddAbonent(string name, string phoneNumber)
    {
      if (!GetPhoneNumberByName(name, out string number))
      {
        var abonent = new Abonent(GetNewAbonentId(), name, phoneNumber);
        Abonents.Add(abonent);
      }
      else
      {
        throw new ArgumentException($"Абонент {name} уже есть в списке абонентов.");
      }
    }

    /// <summary>
    /// Удалить абонента из списка.
    /// </summary>
    /// <param name="abonent">Удаляемый абонент.</param>
    public void DeleteAbonent(Abonent abonent)
    {
      Abonents.Remove(abonent);
    }

    /// <summary>
    /// Найти номер телефона абонента по имени.
    /// </summary>
    /// <param name="name">Имя абонента.</param>
    /// <param name="number">Номер абонента.</param>
    /// <returns>Результат поиска - true если найден, иначе false</returns>
    public bool GetPhoneNumberByName(string name, out string number)
    {
      number = "";
      bool result = false;
      name = name.Trim().ToLower();

      var abonent = Abonents.Where(e => e.NameLower == name).FirstOrDefault();
      if (abonent != null)
      {
        number = abonent.PhoneNumber;
        result = true;
      }

      return result;
    }

    /// <summary>
    /// Найти имя абонента по номеру телефона.
    /// </summary>
    /// <param name="number">Номер телефона.</param>
    /// <param name="name">Имя абонента.</param>
    /// <returns>Результат поиска - true если найден, иначе false</returns>
    public bool GetNameByPhoneNumber(string number, out string name)
    {
      name = "";
      bool result = false;
      number = number.Trim();

      var abonent = Abonents.Where(e => e.PhoneNumber == number).FirstOrDefault();
      if (abonent != null)
      {
        name = abonent.Name;
        result = true;
      }

      return result;
    }

    /// <summary>
    /// Сохранить список абонентов в файл.
    /// </summary>
    public void SaveToFile()
    {
      try
      {
        StreamWriter sw = new StreamWriter(FilePhonebook);

        foreach (Abonent abonent in Abonents)
        {
          sw.WriteLine($"{abonent.Id};{abonent.Name};{abonent.PhoneNumber}");
        }

        sw.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      }
    }

    /// <summary>
    /// Распечатать абонентов.
    /// </summary>
    public void PrintAbonents()
    {
      foreach (var abonent in Abonents)
      {
        Console.WriteLine($"Идентификатор: {abonent.Id}; Имя: {abonent.Name}; Номер телефона: {abonent.PhoneNumber}");
      }
      Console.ReadLine();
    }

    private Phonebook()
    {
      Abonents = ReadAbonentsFromFile();
    }
  }
}
