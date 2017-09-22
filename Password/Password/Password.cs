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
            return yourPassword = smallLetters + capitalLetters.ToUpper() + digits + characters;
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
            string lowerCaseLetters = GenerateLetters(yourPassword.numberOfSmallLetters);
            string upperCaseLetters = GenerateLetters(yourPassword.numberOfCapitalLetters);
            string digits = GenerateDigits(yourPassword.numberOfDigits);
            string characters = GenerateCharacters(yourPassword.numberOfCharacters);
            string passwordToCheck = yourPassword.GeneratePassword(lowerCaseLetters, 
                upperCaseLetters, digits, characters);
            return true;
        }

        public string GenerateLetters(int numberOfLetters)
        {
            string letters = string.Empty;
            for (int i = 0; i < numberOfLetters; i++)
                letters = letters + GenerateRandom('a', 'z');
            return letters;
        }

        public string GenerateDigits(int numberOfDigits)
        {
            string digits = string.Empty;
            for (int i = 0; i < numberOfDigits; i++)
                digits = digits + GenerateRandom('0', '9');
            return digits;
        }

        public string GenerateCharacters(int numberOfCharacters)
        {
            string characters = string.Empty;
            char testChar;
            string ambiguousChar = "{}[]()/\"~,;.<>0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < numberOfCharacters; i++)
            {
                testChar = GenerateRandom('!', '|');
                string testString = testChar.ToString();
                while (ambiguousChar.Contains(testString))
                {
                    testChar = GenerateRandom('!', '|');
                    testString = testChar.ToString();
                }
                characters = characters + testString;
            }
            return characters;
        }

        public char GenerateRandom(char lowerChar, char upperChar)
        {
            Random random = new Random();
            return (char)(random.Next(lowerChar, upperChar));
        }

    }
}
