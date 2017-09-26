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
        public string yourPassword;
        public string smallLetters;
        public string capitalLetters;
        public string digits;
        public string characters;
        public Random random;

        public Password(int numberOfSmallLetters, int numberOfCapitalLetters,
            int numberOfDigits, int numberOfCharacters)
        {
            this.numberOfSmallLetters = numberOfSmallLetters;
            this.numberOfCapitalLetters = numberOfCapitalLetters;
            this.numberOfDigits = numberOfDigits;
            this.numberOfCharacters = numberOfCharacters;
            smallLetters = string.Empty;
            capitalLetters = string.Empty;
            digits = string.Empty;
            characters = string.Empty;
            yourPassword = string.Empty;
            random = new Random();
        }

        public string GenerateSmallLetters()
        {
            if (numberOfSmallLetters==0)
                return "";
            char testLetter;
            string similarLetters = "lo";
            for (int i = 0; i < numberOfSmallLetters; i++)
            {
                testLetter = GenerateRandom('a', 'z');
                string testString = testLetter.ToString();
                while (similarLetters.Contains(testString))
                {
                    testLetter = GenerateRandom('a', 'z');
                    testString = testLetter.ToString();
                }
                smallLetters = smallLetters + testString;
            }
            return smallLetters;
        }

        public string GenerateCapitalLetters()
        {
            if (numberOfCapitalLetters == 0)
                return "";
            char testLetter;
            string similarLetters = "IO";
            for (int i = 0; i < numberOfCapitalLetters; i++)
            {
                testLetter = GenerateRandom('A', 'Z');
                string testString = testLetter.ToString();
                while (similarLetters.Contains(testString))
                {
                    testLetter = GenerateRandom('A', 'Z');
                    testString = testLetter.ToString();
                }
                capitalLetters = capitalLetters + testString;
            }
            return capitalLetters;
        }
        
        public string GenerateDigits()
        {
            if (numberOfDigits == 0)
                return "";
            for (int i = 0; i < numberOfDigits; i++)
                digits = digits + GenerateRandom('2', '9');
            return digits;
        }

        public string GenerateCharacters()
        {
            if (numberOfCharacters == 0)
                return "";
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

        public void GeneratePassword()
        {
            yourPassword = GenerateSmallLetters() + GenerateCapitalLetters() + 
                GenerateDigits() + GenerateCharacters();
        }

        public char GenerateRandom(char lowerChar, char upperChar)
        {
            return (char)(random.Next(lowerChar, upperChar + 1));
        }
    }

    [TestClass]
    public class Passwords
    {
        [TestMethod]
        public void IsPasswordCorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void IsPasswordInorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = RuinPassword(passwordToCheck.yourPassword, '0');
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = RuinPassword(passwordToCheck.yourPassword, '1');
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = RuinPassword(passwordToCheck.yourPassword, '>');
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        public bool ValidatePassword(Password passwordToCheck)
        {
            int countOfLowerCaseLetters = 0;
            int countOfUpperCaseLetters = 0;
            int countOfDigits = 0;
            int countOfCharacters = 0;
            for (int i = 0; i < passwordToCheck.yourPassword.Length; i++)
            {
                if ("abcdefghijkmnpqrstuvwxyz".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfLowerCaseLetters++;
                if ("ABCDEFGHJKLMNPQRSTUVWXYZ".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfUpperCaseLetters++;
                if ("23456789".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfDigits++;
                if (!"{ }[]()/\"~,;.<>0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfCharacters++;
            }
            if (countOfLowerCaseLetters == passwordToCheck.numberOfSmallLetters &&
                countOfUpperCaseLetters == passwordToCheck.numberOfCapitalLetters &&
                countOfDigits == passwordToCheck.numberOfDigits &&
                countOfCharacters == passwordToCheck.numberOfCharacters)
                return true;
            return false;
        }

        public string RuinPassword(string yourPassword, char badCharacter)
        {
            char[] passwordToRuing = yourPassword.ToCharArray();
            passwordToRuing[5] = badCharacter;
            return yourPassword = new string(passwordToRuing);
        }
    }
}
