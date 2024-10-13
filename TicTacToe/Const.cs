namespace TicTacToe
{
  /// <summary>
  /// Константы.
  /// </summary>
  public class Const
  {
    /// <summary>
    /// Символ, которым ходит 1й игрок.
    /// </summary>
    public static string CharGamer1 = "X";

    /// <summary>
    /// Символ, которым ходит 2й игрок.
    /// </summary>
    public static string CharGamer2 = "O";

    /// <summary>
    /// Символ пустого поля.
    /// </summary>
    public static string CharNull = " ";
  }

  /// <summary>
  /// Игрок.
  /// </summary>
  public enum Gamer
  {
    /// <summary>
    /// 1й игрок.
    /// </summary>
    First,

    /// <summary>
    /// 2й игрок.
    /// </summary>
    Second
  }
}
