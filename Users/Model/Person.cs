using System;
using System.Data;

namespace Users.Model
{
  /// <summary>
  /// Персона.
  /// </summary>
  public class Person : IEquatable<Person>
  {
    #region Поля и свойства

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; private set; }

    #endregion

    #region IEquatable

    /// <summary>
    /// Сравнить с персоной (Реализация интерфейса IEquatable).
    /// </summary>
    /// <param name="other">Персона, с которой сравниваем.</param>
    /// <returns>Результат сравнения: true - один и тот же пользователь, иначе - false.</returns>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задана персона для сравнения.</exception>
    public bool Equals(Person other)
    {
      if (other == null)
      {
        throw new ArgumentNullException("Не задана персона для сравнения.");
      }
      return this.Name == other.Name;
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    public Person(string name)
    {
      this.Name = name;
      Console.WriteLine($"Создан пользователь {this.Name}.");
    }

    #endregion
  }
}
