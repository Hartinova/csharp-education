using System;

namespace EmployeeAccountingSystem.Model
{
  /// <summary>
  /// Сотрудник.
  /// </summary>
  public class Employee
  {
    #region Поля и свойства

    /// <summary>
    /// Табельный номер.
    /// </summary>
    public int TabelNumber { get; private set; }

    /// <summary>
    /// Имя сотрудника.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Профессия.
    /// </summary>
    public Profession Profession { get; private set; }

    #endregion

    #region Методы

    /// <summary>
    /// Изменить профессию.
    /// </summary>
    /// <param name="profession">Новая профессия.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задана профессия.</exception>
    public void SetProfession(Profession profession)
    {
      if (profession == null)
      {
        throw new ArgumentException("Не задана профессия.");
      }

      this.Profession = profession;
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="tabelNumber">Табельный номер.</param>
    /// <param name="name">Имя.</param>
    /// <param name="profession">Профессия.</param>
    public Employee(int tabelNumber, string name, Profession profession)
    {
      this.TabelNumber = tabelNumber;
      this.Name = name;
      this.SetProfession(profession);
    }

    #endregion
  }
}
