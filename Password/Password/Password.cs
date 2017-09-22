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
            return yourPassword = smallLetters + capitalLetters + digits + characters;
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
            string lowerCaseLetters = GenerateSmallLetters(yourPassword.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(yourPassword.numberOfCapitalLetters);
            string digits = GenerateDigits(yourPassword.numberOfDigits);
            string characters = GenerateCharacters(yourPassword.numberOfCharacters);
            string passwordToCheck = yourPassword.GeneratePassword(lowerCaseLetters, 
                upperCaseLetters, digits, characters);
            return true;
        }

        public string GenerateSmallLetters(int numberOfLetters)
        {
            string letters = string.Empty;
            char testLetter;
            string similarLetters = "lo";
            for (int i = 0; i < numberOfLetters; i++)
            {
                testLetter = GenerateRandom('a', 'z');
                string testString = testLetter.ToString();
                while (similarLetters.Contains(testString))
                {
                    testLetter = GenerateRandom('A', 'Z');
                    testString = testLetter.ToString();
                }
                letters = letters + testString;

            }
            return letters;
        }

        public string GenerateCapitalLetters(int numberOfLetters)
        {
            string letters = string.Empty;
            char testLetter;
            string similarLetters = "IO";
            for (int i = 0; i < numberOfLetters; i++)
            {
                testLetter = GenerateRandom('A', 'Z');
                string testString = testLetter.ToString();
                while (similarLetters.Contains(testString))
                {
                    testLetter = GenerateRandom('A', 'Z');
                    testString = testLetter.ToString();
                }
                letters = letters + testString;

            }
            return letters;
        }

        public string GenerateDigits(int numberOfDigits)
        {
            string digits = string.Empty;
            for (int i = 0; i < numberOfDigits; i++)
                digits = digits + GenerateRandom('2', '9');
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

        Random random = new Random();

        public char GenerateRandom(char lowerChar, char upperChar)
        {
            return (char)(random.Next(lowerChar, upperChar+1));
        }

    }
}
