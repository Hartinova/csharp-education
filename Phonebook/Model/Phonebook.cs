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
    #region Поля и свойства

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

    #endregion

    #region Методы

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
    /// <exception cref="ArgumentNullException">Исключение вызывается, если не введено имя или номер телефона для ввода в справочник.</exception>
    /// <exception cref="ArgumentException">Исключение вызывается, если ввдено пустое имя или пустой номер или абонент уже присутствует в списке абонентов.</exception>
    public void AddAbonent(string name, string phoneNumber)
    {
      if (name == null)
      {
        throw new ArgumentNullException("Не введено имя абонента");
      }
      if (!GetPhoneNumberByName(name, out string number))
      {
        Abonent abonent;

        abonent = new Abonent(GetNewAbonentId(), name, phoneNumber);

        Abonents.Add(abonent);
      }
      else
      {
        throw new ArgumentException($"Абонент {name} уже есть в списке абонентов.");
      }
    }

    /// <summary>
    /// Получить абонента из списка.
    /// </summary>
    /// <param name="name">Имя абонента.</param>
    /// <returns>Найденный абонент.</returns>
    /// <exception cref="ArgumentNullException">Исключение возникает, если не введено имя абонента для поиска.</exception>
    /// <exception cref="ArgumentException">Исключение вызывается, если введено пустое имя или абонент уже присутствует в списке абонентов.</exception>
    public Abonent GetAbonent(string name)
    {
      if (name == null)
      {
        throw new ArgumentNullException("Не введено имя для поиска абонента.");
      }
      if (name.Trim() == "")
      {
        throw new ArgumentException("Введено пустое имя при поиске абонента.");
      }
      if (this.Abonents == null)
      {
        return null;
      }
      else
      {
        return this.Abonents.Where(e => e.Name == name).FirstOrDefault();
      }
    }

    /// <summary>
    /// Удалить всех абонентов из списка;
    /// </summary>
    public void RemoveAbonents()
    {
      this.Abonents = new List<Abonent>();
    }

    /// <summary>
    /// Удалить абонента из списка.
    /// </summary>
    /// <param name="abonent">Удаляемый абонент.</param>
    /// <exception cref="ArgumentNullException">Исключение возникает, если не ввден абонент для удаления.</exception>
    public void DeleteAbonent(Abonent abonent)
    {
      if (abonent == null)
      {
        throw new ArgumentNullException("Не введен абонент для удаления.");
      }
      Abonents.Remove(abonent);
    }

    /// <summary>
    /// Найти номер телефона абонента по имени.
    /// </summary>
    /// <param name="name">Имя абонента.</param>
    /// <param name="number">Номер абонента.</param>
    /// <returns>Результат поиска - true если найден, иначе false</returns>
    /// <exception cref="ArgumentNullException">Исключение возникает, если не введено имя абонента для поиска.</exception>
    /// <exception cref="ArgumentException">Исключение вызывается, если введено пустое имя.</exception>
    public bool GetPhoneNumberByName(string name, out string number)
    {
      if (name == null)
      {
        throw new ArgumentNullException("Не введено имя для поиска абонента.");
      }
      if (name.Trim() == "")
      {
        throw new ArgumentException("Введено пустое имя при поиске абонента.");
      }
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
    /// <exception cref="ArgumentNullException">Исключение возникает, если не введен номер абонента для поиска.</exception>
    /// <exception cref="ArgumentException">Исключение вызывается, если введен пустой номер.</exception>
    public bool GetNameByPhoneNumber(string number, out string name)
    {
      if (number == null)
      {
        throw new ArgumentNullException("Не введен номер для поиска абонента.");
      }
      if (number.Trim() == "")
      {
        throw new ArgumentException("Введен пустой номер при поиске абонента.");
      }
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
    /// <param name="fileName">Имя файла.</param>
    /// <exception cref="ArgumentNullException">Исключение возникает, если не введено имя файла.</exception>
    /// <exception cref="ArgumentException">Исключение вызывается, если введено пустое имя файла.</exception>
    ///<exception cref="Exception">Исключение возникает, если возникла ошибка при сохранении файла.</exception>
    public void SaveToFile(string fileName)
    {
      if (fileName == null)
      {
        throw new ArgumentNullException("Не введено имя файла.");
      }
      if (fileName.Trim() == "")
      {
        throw new ArgumentException("Введено пустое имя файла.");
      }
      try
      {
        StreamWriter sw = new StreamWriter(fileName);

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

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    private Phonebook()
    {
      Abonents = ReadAbonentsFromFile();
    }

    #endregion
  }
}
