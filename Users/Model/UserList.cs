using System;
using System.Collections.Generic;
using System.Linq;

namespace Users.Model
{
  /// <summary>
  /// Список персон.
  /// </summary>
  public class UserList
  {
    #region Поля и свойства

    /// <summary>
    /// Список пользователей.
    /// </summary>
    private List<Person> list;

    /// <summary>
    /// Индексатор.
    /// </summary>
    /// <param name="index">Индекс элемента.</param>
    /// <returns>Пользователь из списка пользователей, находящийся под указанным индексом.</returns>
    /// <exception cref="Exception">Ошибка возникает, если список пользователей пуст.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Ошибка возникает, если задан недопустимый индекс.</exception>
    public Person this[int index]
    {
      get
      {
        if (list == null || list.Count == 0)
        {
          throw new Exception("Список пользователей пуст.");
        }
        if (index < 0 || index >= list.Count())
        {
          throw new ArgumentOutOfRangeException($"Индекс находится за пределами диапазона списка пользователей.");
        }

        return this.list[index];
      }
      set
      {
        if (list == null || list.Count == 0)
        {
          throw new Exception("Список пользователей пуст.");
        }
        if (index < 0 || index >= list.Count())
        {
          throw new ArgumentOutOfRangeException($"Индекс находится за пределами диапазона списка пользователей.");
        }
        this.list[index] = value;
      }
    }

    #endregion

    #region Методы

    /// <summary>
    /// Добавить персону в список.
    /// </summary>
    /// <param name="user">Добавляемая персона.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задана персона для добавления.</exception>
    /// <exception cref="ArgumentException">Ошибка возникает, если пользователь уже существует в списке пользователей.</exception>
    public void Add(Person user)
    {
      if (user == null)
      {
        throw new ArgumentNullException("Не задан пользователь для добавления.");
      }
      if (this.list.Contains(user))
      {
        throw new ArgumentException($"Пользователь {user.Name} уже заведен.");
      }

      this.list.Add(user);
      Console.WriteLine($"Пользователь {user.Name} добавлен в список пользователей.");
    }

    /// <summary>
    /// Удалить пользователя из списка пользователей.
    /// </summary>
    /// <param name="user">Удаляемый пользователь.</param>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если не задан пользователь для удаления.</exception>
    /// <exception cref="ArgumentException">Ошибка возникает, если в списке пользователей не найден удаляемый пользователь.</exception>
    public void Delete(Person user)
    {
      if (user == null)
      {
        throw new ArgumentNullException("Не задан пользователь для удаления.");
      }
      if (!this.list.Contains(user))
      {
        throw new ArgumentException($"В списке пользователей не найден пользователь {user.Name}.");
      }

      this.list.Remove(user);
      Console.WriteLine($"Пользователь {user.Name} удален из списка пользователей.");
    }

    /// <summary>
    /// Распечатать список пользователей.
    /// </summary>
    /// <exception cref="ArgumentNullException">Ошибка возникает, если список пользователей пуст.</exception>
    public void Print()
    {
      if (this.list == null || this.list.Count() == 0)
      {
        throw new ArgumentNullException("Список пользователей пуст.");
      }

      Console.WriteLine("Список пользователей:");
      foreach (var item in this.list)
      {
        Console.WriteLine(item.Name);
      }

    }

    #endregion 

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public UserList()
    {
      this.list = new List<Person>();
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="users">Список пользователей.</param>
    public UserList(List<Person> users)
    {
      this.list = users;
    }

    #endregion
  }
}
