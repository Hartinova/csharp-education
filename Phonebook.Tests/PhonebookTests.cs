using Phonebook.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phonebook.Tests
{
  public class PhonebookTests
  {
    private static Phonebook.Model.Phonebook phonebook;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      phonebook = Phonebook.Model.Phonebook.Instance;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
      phonebook = null;
    }

    [SetUp]
    public void SetUp()
    {
      phonebook.RemoveAbonents();

      var name = "Abonent";
      var phoneNumber = "1111122";
      phonebook.AddAbonent(name, phoneNumber);
    }

    [Test]
    public void AddAbonent()
    {
      var name = "Ivan";
      var phoneNumber = "1111122";

      phonebook.AddAbonent(name, phoneNumber);
      var abonent = phonebook.GetAbonent(name);

      Assert.IsNotNull(abonent);
    }

    [Test]
    public void AddAbonentWithNullName()
    {
      string name = null;
      var phoneNumber = "1111122";

      Assert.Throws<ArgumentNullException>(() => phonebook.AddAbonent(name, phoneNumber));
    }

    [Test]
    public void AddAbonentWithNullPhone()
    {
      string name = "Alena111";
      string phoneNumber = null;

      Assert.Throws<ArgumentNullException>(() => phonebook.AddAbonent(name, phoneNumber));
    }

    [Test]
    public void AddAbonentWithEmptyName()
    {
      string name = "";
      var phoneNumber = "1111122";

      Assert.Throws<ArgumentException>(() => phonebook.AddAbonent(name, phoneNumber));
    }

    [Test]
    public void AddAbonentWithEmptyPhone()
    {
      string name = "Alena1113";
      string phoneNumber = "";

      Assert.Throws<ArgumentException>(() => phonebook.AddAbonent(name, phoneNumber));
    }

    [Test]
    public void AddExistAbonent()
    {
      string name = "Alena1113";
      string phoneNumber = "11111";

      phonebook.AddAbonent(name, phoneNumber);

      Assert.Throws<ArgumentException>(() => phonebook.AddAbonent(name, phoneNumber));
    }

    [Test]
    public void GetAbonentWithNullName()
    {
      string name = null;

      Assert.Throws<ArgumentNullException>(() => phonebook.GetAbonent(name));
    }

    [Test]
    public void GetAbonentWithEmptyName()
    {
      string name = "";

      Assert.Throws<ArgumentException>(() => phonebook.GetAbonent(name));
    }

    [Test]
    public void DeleteAbonent()
    {
      var name = "Ivan1111";
      var phoneNumber = "1111122";

      phonebook.AddAbonent(name, phoneNumber);
      var abonent = phonebook.GetAbonent(name);
      phonebook.DeleteAbonent(abonent);
      var newAbonent = phonebook.GetAbonent(name);

      Assert.IsNull(newAbonent);
    }

    [Test]
    public void DeleteNullAbonent()
    {
      Abonent abonent = null;

      Assert.Throws<ArgumentNullException>(() => phonebook.DeleteAbonent(abonent));
    }

    [Test]
    public void GetPhoneNumberByName()
    {
      string name = "Abonent";
      Abonent abonent = phonebook.GetAbonent(name);

      phonebook.GetPhoneNumberByName(name, out string findedPhone);

      Assert.That(abonent.PhoneNumber, Is.EqualTo(findedPhone));
    }

    [Test]
    public void GetPhoneNumberByNotExistedName()
    {
      string name = "AbonentNotExisted";

      var result = phonebook.GetPhoneNumberByName(name, out string findedPhone);

      Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void GetPhoneNumberByNullName()
    {
      string name = null;

      Assert.Throws<ArgumentNullException>(() => phonebook.GetPhoneNumberByName(name, out string phonenumber));
    }

    [Test]
    public void GetPhoneNumberByEmptyName()
    {
      string name = "";

      Assert.Throws<ArgumentException>(() => phonebook.GetPhoneNumberByName(name, out string phonenumber));
    }

    [Test]
    public void GetNameByPhoneNumber()
    {
      string number = "1111122";

      var result = phonebook.GetNameByPhoneNumber(number, out string findedName);

      Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public void GetNameByNotExistedNumber()
    {
      string number = "0000000";

      var result = phonebook.GetNameByPhoneNumber(number, out string findedName);

      Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void GetNameByNullNumber()
    {
      string number = null;

      Assert.Throws<ArgumentNullException>(() => phonebook.GetNameByPhoneNumber(number, out string phoneName));
    }

    [Test]
    public void GetNameByEmptyNumber()
    {
      string number = "";

      Assert.Throws<ArgumentException>(() => phonebook.GetNameByPhoneNumber(number, out string phoneName));
    }

    [Test]
    public void SaveToFile()
    {
      var fileName = "TestFile";
      phonebook.SaveToFile(fileName);

      var result = File.Exists(fileName);
      if (result)
      {
        File.Delete(fileName);
      }

      Assert.IsTrue(result);
    }
  }
}
