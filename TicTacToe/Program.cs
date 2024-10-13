using System;

namespace TicTacToe
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var flagFinish = false;

      Console.WriteLine("Начата игра Крестики-нолики.");
      PrintPlayingField(TicTacToe.PlayingField);

      do
      {
        Console.WriteLine(string.Format("Игрок {0}, введите строку и столбец (от 1 до 3) через запятую:",
          (TicTacToe.CurrentGamer == Gamer.First) ? 1 : 2));

        var inputString = Console.ReadLine().Trim().ToLower();

        if (TicTacToe.ExecuteMove(inputString, out string messageExecute))
        {
          if (TicTacToe.CheckForGameEnd(out string messageFinish))
          {
            Console.WriteLine(messageFinish);
            flagFinish = true;
          }
          else
          { 
            TicTacToe.ChangeGamer(); 
          }
        }
        else
        {
          Console.WriteLine(messageExecute);
        }
        PrintPlayingField(TicTacToe.PlayingField);

      }
      while (!flagFinish);

      Console.ReadLine();
    }

    /// <summary>
    /// Вывести на экран поле для игры.
    /// </summary>
    /// <param name="playingField">Поле для игры.</param>
    private static void PrintPlayingField(string[,] playingField)
    {
      Console.WriteLine("-------");

      for (int i = 0; i < playingField.GetLength(0); i++)
      {
        string s = "|";

        for (int j = 0; j < playingField.GetLength(1); j++)
        {
          s += playingField[i, j] + "|";
        }

        Console.WriteLine(s);
      }

      Console.WriteLine("-------");
    }


  }
}
