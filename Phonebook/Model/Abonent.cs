using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Model
{
  /// <summary>
  /// Абонент.
  /// </summary>
  public class Abonent
  {
    #region Поля и свойства

    /// <summary>
    /// Максимальное количество знаков в телефоне.
    /// </summary>
    private const short MaxLengthPhone = 11;

    /// <summary>
    /// Идентификатор абонента.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Имя абонента.
    /// </summary>
    private string name;

    /// <summary>
    /// Имя абонента.
    /// </summary>
    /// <exception cref="ArgumentNullException">Исключение возникает, если не введено имя абонента.</exception>
    /// <exception cref="ArgumentException">Исключение возникает, если введено пустое имя абонента.</exception>
    public string Name
    {
      get
      {
        return name;
      }
      private set
      {
        if (value == null)
        {
          throw new ArgumentNullException(Constants.ArgumentNullExceptionMessage);
        }
        if (value.Trim() == "")
        {
          throw new ArgumentException(Constants.EmptyExceptionMessage);
        }
        name = value.Trim();
      }
    }

    /// <summary>
    /// Имя в нижней раскладке.
    /// </summary>
    public string NameLower
    {
      get
      {
        return name.ToLower();
      }
    }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    private string phoneNumber;

    /// <summary>
    /// Номер телефона.
    /// </summary>
    /// <exception cref="ArgumentNullException">Исключение возникает, если не введен номер телефона.</exception>
    /// <exception cref="ArgumentException">Исключение возникает, если введен пустой номер телефона либо введен некорректно.</exception>
    public string PhoneNumber
    {
      get
      {
        return phoneNumber;
      }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException(Constants.ArgumentNullExceptionMessage);
        }
        if (value.Trim() == "")
        {
          throw new ArgumentException(Constants.EmptyExceptionMessage);
        }
        if (value.Length <= MaxLengthPhone && long.TryParse(value, out long phone))
        {
          phoneNumber = value.Trim();
        }
        else
        {
          throw new ArgumentException(String.Format(Constants.InvalidNumberPhone, value));
        }
      }
    }

    #endregion

    #region Методы

    /// <summary>
    /// Изменить имя абонента.
    /// </summary>
    /// <param name="name">Присваимое значение имени.</param>
    public void ChangeName(string name)
    {
      this.Name = name;
    }

    /// <summary>
    /// Изменить номер телефона абонента.
    /// </summary>
    /// <param name="phoneNumber">Присваиваемое значение номера телефона.</param>
    public void ChangeNumber(string phoneNumber)
    {
      this.PhoneNumber = phoneNumber;
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор абонента.</param>
    /// <param name="name">Имя.</param>
    /// <param name="phone">Номер телефона.</param>
    public Abonent(int id, string name, string phone)
    {
      this.Id = id;
      this.Name = name;
      this.PhoneNumber = phone;
    }

    #endregion
  }
}
