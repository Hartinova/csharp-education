using System;
using Users.Model;


namespace Users
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var users = new UserList();

      try
      {
        users.Print();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.WriteLine();

      users.Add(new Person("Oksana"));
      users.Add(new Person("Timur"));
      try
      {
        users.Print();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.WriteLine();

      try
      {
        users.Add(new Person("Oksana"));
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message + "\n");
      }

      try
      {
        users.Delete(new Person("Alena"));
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message + "\n");
      }

      try
      {
        users.Delete(new Person("Oksana"));
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message + "\n");
      }

      try
      {
        users.Print();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.WriteLine();

      var t = users[0];

      Console.ReadLine();
    }
  }
}
