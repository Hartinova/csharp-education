using System;

namespace EmployeeAccountingSystem.Model
{
  /// <summary>
  /// Тариф.
  /// </summary>
  public class Tariff
  {
    #region Поля и свойства

    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Тариф.
    /// </summary>
    public double Value { get; private set; }

    #endregion

    #region

    /// <summary>
    /// Установить новое значение для тарифа.
    /// </summary>
    /// <param name="value">Новое значение.</param>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если неверно задано значение тарифа.</exception>
    public void SetValue(double value)
    {
      if (value <= 0)
      {
        throw new ArgumentOutOfRangeException("Значение тарифа должно быть > 0.");
      }

      this.Value = value;
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конуструктор.
    /// </summary>
    /// <param name="value">Тариф.</param>
    public Tariff(int id, double value)
    {
      this.Id = id;
      this.SetValue(value);
    }

    #endregion
  }
}
