using Newtonsoft.Json.Linq;
using Phonebook.Model;
using System;
using System.Numerics;
using System.Xml.Linq;

namespace Phonebook.Tests
{
  public class AbonentTests
  {
    private Abonent abonent;

    [SetUp]
    public void SetUp()
    {
      this.abonent = new Abonent(1, "Boris", "123456789");
    }

    [TearDown]
    public void TearDown()
    {
      this.abonent = null;
    }

    [Test]
    public void SetValidName()
    {
      string newName = "ALeksey";

      abonent.ChangeName(newName);

      Assert.That(abonent.Name, Is.EqualTo(newName));
    }

    [Test]
    public void SetNullName()
    {
      string newName = null;

      Assert.Throws<ArgumentNullException>(() => abonent.ChangeName(newName));
    }

    [Test]
    public void SetEmptyName()
    {
      string newName = "";

      Assert.Throws<ArgumentException>(() => abonent.ChangeName(newName));
    }

    [Test]
    public void SetValidNumber()
    {
      var newPhone = "222222";

      abonent.ChangeNumber(newPhone);

      Assert.That(abonent.PhoneNumber, Is.EqualTo(newPhone));
    }

    [Test]
    public void SetNullPhone()
    {
      string newPhone = null;

      Assert.Throws<ArgumentNullException>(() => abonent.ChangeNumber(newPhone));
    }

    [Test]
    public void SetEmptyPhone()
    {
      var newPhone = "";

      Assert.Throws<ArgumentException>(() => abonent.ChangeNumber(newPhone));
    }

    [Test]
    public void ChangeInvalidNumberThrow()
    {
      var newPhone = "dd";

      Assert.Throws<ArgumentException>(() => abonent.ChangeNumber(newPhone));
    }
  }
}