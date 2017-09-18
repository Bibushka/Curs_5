using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Password
{
    public struct Password
    {
        public int numberOfSmallLetters;
        public int numberOfCapitalLetters;
        public int numberOfDigits;
        public int numberOfCharacters;

        public Password(int numberOfSmallLetters, int numberOfCapitalLetters,
            int numberOfDigits, int numberOfCharacters)
        {
            this.numberOfSmallLetters = numberOfSmallLetters;
            this.numberOfCapitalLetters = numberOfCapitalLetters;
            this.numberOfDigits = numberOfDigits;
            this.numberOfCharacters = numberOfCharacters;
        }

        public string GeneratePassword(string smallLetters, string capitalLetters,
            string digits, string characters)
        {
            string yourPassword = string.Empty;
            return yourPassword = smallLetters + capitalLetters + characters;
        }
    }

    [TestClass]
    public class Passwords
    {
        [TestMethod]
        public void IsPasswordCorrectlyComposed()
        {
            var yourPassword = new Password(2, 3, 4, 1);
            Assert.IsTrue(ValidatePassword(yourPassword));
        }

        public bool ValidatePassword(Password yourPassword)
        {
            return false;
        }
    }
}
