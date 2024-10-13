using System.Linq;

namespace TicTacToe
{
  /// <summary>
  /// Игра Крестики-нолики.
  /// </summary>
  public static class TicTacToe
  {
    /// <summary>
    /// Размерность поля для игры.
    /// </summary>
    public static int Dimension = 3;

    /// <summary>
    /// Поле с ходами игроков 1 и 2.
    /// </summary>
    public static string[,] PlayingField;

    /// <summary>
    /// Текущий игрок.
    /// </summary>
    public static Gamer CurrentGamer;

    /// <summary>
    /// Конструктор игры крестики-нолики.
    /// </summary>
    static TicTacToe()
    {
      PlayingField = CreatePlayingField();
      CurrentGamer = Gamer.First;
    }

    /// <summary>
    /// Создать игровое поле.
    /// </summary>
    /// <returns>Числовой массив строк. Заполнен строками, определенными для пустых ячеек поля.</returns>
    private static string[,] CreatePlayingField()
    {
      var result = new string[Dimension, Dimension];

      for (int i = 0; i < Dimension; i++)
      {
        for (int j = 0; j < Dimension; j++)
        {
          result[i, j] = Const.CharNull;
        }
      }

      return result;
    }

    /// <summary>
    /// Выполнить ход.
    /// </summary>
    /// <param name="inputString">Входные данные, полученные строкой из консоли в формате int,int через запятую.</param>
    /// <param name="message">Сообщение для пользователя.</param>
    /// <returns>True, при успешной обработке строки, введенной пользователем. Иначе - false.</returns>
    public static bool ExecuteMove(string inputString, out string message)
    {
      var result = false;
      message = "Неверно заданы координаты.";

      //Преобразование входной строки в массив строк с координатами - должен получиться массив из 2 элементов при правильном вводе данных пользователем.
      var coordinate = inputString.Split(',');

      if (coordinate.Count() != PlayingField.Rank)
      {
        return false;
      }

      //Проверка, правильно ли задана строка.
      if (int.TryParse(coordinate[0], out int row) && row > 0 && row <= Dimension)
      {
        //Проверка, правильно ли задана колонка.
        if (int.TryParse(coordinate[1], out int column) && column > 0 && column <= Dimension)
        {
          //Выполнение хода.
          result = SetCharToPlayingField(row, column, out message);
        }
      }
      return result;
    }

    /// <summary>
    /// Проставить символ в игровое поле.
    /// </summary>
    /// <param name="row">Номер строки.</param>
    /// <param name="column">Номер колонки.</param>
    /// <param name="message">Сообщение для пользователя.</param>
    /// <returns>True, при успешном выполнении хода. Иначе - false.</returns>
    public static bool SetCharToPlayingField(int row, int column, out string message)
    {
      var result = false;
      message = string.Empty;

      if (PlayingField[row - 1, column - 1] == Const.CharNull)
      {
        PlayingField[row - 1, column - 1] = (CurrentGamer == Gamer.First) ? Const.CharGamer1 : Const.CharGamer2;
        result = true;
      }
      else
      { 
        message = "В эту ячейку уже ввели данные. Выберите другую."; 
      }

      return result;
    }

    /// <summary>
    /// Сменить текущего пользователя.
    /// </summary>
    public static void ChangeGamer()
    {
      CurrentGamer = (CurrentGamer == Gamer.First) ? Gamer.Second : Gamer.First;
    }

    /// <summary>
    /// Проверить окончена ли игра.
    /// </summary>
    /// <param name="mess">Сообщение для вывода пользователю.</param>
    /// <returns>True, если игра окончена. Иначе - false.</returns>
    public static bool CheckForGameEnd(out string mess)
    {
      bool result = false;
      mess = string.Empty;

      //Если выиграл текущий игрок, то игра окончена
      if (IsWin(CurrentGamer))
      {
        mess = string.Format("Игра окончена победой {0} игрока.", (CurrentGamer == Gamer.First) ? 1 : 2);
        return true;
      }

      //Если не осталось пустых ячеек на игровом поле, то игра окончена.
      if (!ExistNullValue())
      {
        mess = "Игра окончена. Ничья.";
        return true;
      }

      return result;
    }

    /// <summary>
    /// Проверить на наличие пустых ячеек.
    /// </summary>
    /// <param name="mess">Сообщение для вывода пользователю.</param>
    /// <returns>True-если есть пустые ячейки. Иначе - false.</returns>
    private static bool ExistNullValue()
    {
      bool result = false;

      for (int i = 0; i < PlayingField.GetLength(0); i++)
      {
        for (int j = 0; j < PlayingField.GetLength(1); j++)
        {
          if (PlayingField[i, j] == Const.CharNull)
          {
            return true;
          }
        }
      }

      return result;
    }

    /// <summary>
    /// Проверить на выигрыш.
    /// </summary>
    /// <param name="gamer">Текущий игрок.</param>
    /// <returns>True, если текущий игрок выиграл. Иначе - false.</returns>
    private static bool IsWin(Gamer gamer)
    {
      var result = false;

      //Символ, которым ходит текущий игрок.
      string charGamer = (gamer == Gamer.First) ? Const.CharGamer1 : Const.CharGamer2;

      //Поиск выигрышной строки.
      for (int i = 0; i < PlayingField.GetLength(0); i++)
      {
        if (PlayingField[i, 0] == charGamer && PlayingField[i, 1] == charGamer && PlayingField[i, 2] == charGamer)
        {
          return true;
        }
      }

      //Поиск выигрышной колонки.
      for (int j = 0; j < PlayingField.GetLength(1); j++)
      {
        if (PlayingField[0, j] == charGamer && PlayingField[1, j] == charGamer && PlayingField[2, j] == charGamer)
        {
          return true;
        }
      }

      //Поиск выигрышной диагонали.
      if (PlayingField[0, 0] == charGamer && PlayingField[1, 1] == charGamer && PlayingField[2, 2] == charGamer)
      {
        return true;
      }

      if (PlayingField[0, 2] == charGamer && PlayingField[1, 1] == charGamer && PlayingField[2, 0] == charGamer)
      {
        return true;
      }

      return result;
    }
  }
}
