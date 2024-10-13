using System;

namespace EmployeeAccountingSystem.Model
{
  /// <summary>
  /// Профессия.
  /// </summary>
  public class Profession
  {
    #region Поля и свойства

    /// <summary>
    /// Наименование профессии.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Тариф.
    /// </summary>
    public Tariff TariffProfession { get; private set; }

    #endregion

    #region Методы

    /// <summary>
    /// Установить новый тариф для профессии.
    /// </summary>
    /// <param name="tariff">Новый тариф.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задан новый тариф.</exception>
    public void SetTariff(Tariff tariff)
    {
      if (tariff == null)
      {
        throw new ArgumentNullException("Не задан новый тариф.");
      }

      this.TariffProfession = tariff;
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="name">Наименование профессии.</param>
    public Profession(string name, Tariff tariff)
    {
      this.Name = name;
      this.SetTariff(tariff);
    }

    #endregion
  }
}
