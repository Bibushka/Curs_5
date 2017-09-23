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

        public Password(int numberOfSmallLetters, int numberOfCapitalLetters,
            int numberOfDigits, int numberOfCharacters)
        {
            this.numberOfSmallLetters = numberOfSmallLetters;
            this.numberOfCapitalLetters = numberOfCapitalLetters;
            this.numberOfDigits = numberOfDigits;
            this.numberOfCharacters = numberOfCharacters;
            yourPassword = string.Empty;
        }

        public void GeneratePassword(string smallLetters, string capitalLetters,
            string digits, string characters)
        {
            yourPassword = smallLetters + capitalLetters + digits + characters;
        }
    }

    [TestClass]
    public class Passwords
    {
        [TestMethod]
        public void IsPasswordCorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            string lowerCaseLetters = GenerateSmallLetters(passwordToCheck.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(passwordToCheck.numberOfCapitalLetters);
            string digits = GenerateDigits(passwordToCheck.numberOfDigits);
            string characters = GenerateCharacters(passwordToCheck.numberOfCharacters);
            passwordToCheck.GeneratePassword(lowerCaseLetters,upperCaseLetters, digits, characters);
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void IsPasswordInorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            string lowerCaseLetters = GenerateSmallLetters(passwordToCheck.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(passwordToCheck.numberOfCapitalLetters);
            string digits = GenerateDigits(passwordToCheck.numberOfDigits);
            string characters = GenerateCharacters(passwordToCheck.numberOfCharacters);
            passwordToCheck.GeneratePassword(lowerCaseLetters, upperCaseLetters, digits, characters);
            passwordToCheck.yourPassword = RuinPassword(passwordToCheck.yourPassword, '0');
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            string lowerCaseLetters = GenerateSmallLetters(passwordToCheck.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(passwordToCheck.numberOfCapitalLetters);
            string digits = GenerateDigits(passwordToCheck.numberOfDigits);
            string characters = GenerateCharacters(passwordToCheck.numberOfCharacters);
            passwordToCheck.GeneratePassword(lowerCaseLetters, upperCaseLetters, digits, characters);
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            string lowerCaseLetters = GenerateSmallLetters(passwordToCheck.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(passwordToCheck.numberOfCapitalLetters);
            string digits = GenerateDigits(passwordToCheck.numberOfDigits);
            string characters = GenerateCharacters(passwordToCheck.numberOfCharacters);
            passwordToCheck.GeneratePassword(lowerCaseLetters, upperCaseLetters, digits, characters);
            passwordToCheck.yourPassword = RuinPassword(passwordToCheck.yourPassword, '1');
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            string lowerCaseLetters = GenerateSmallLetters(passwordToCheck.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(passwordToCheck.numberOfCapitalLetters);
            string digits = GenerateDigits(passwordToCheck.numberOfDigits);
            string characters = GenerateCharacters(passwordToCheck.numberOfCharacters);
            passwordToCheck.GeneratePassword(lowerCaseLetters, upperCaseLetters, digits, characters);
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            string lowerCaseLetters = GenerateSmallLetters(passwordToCheck.numberOfSmallLetters);
            string upperCaseLetters = GenerateCapitalLetters(passwordToCheck.numberOfCapitalLetters);
            string digits = GenerateDigits(passwordToCheck.numberOfDigits);
            string characters = GenerateCharacters(passwordToCheck.numberOfCharacters);
            passwordToCheck.GeneratePassword(lowerCaseLetters, upperCaseLetters, digits, characters);
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

        public string RuinPassword(string yourPassword, char badCharacter)
        {
            char[] passwordToRuing = yourPassword.ToCharArray();
            passwordToRuing[5] = badCharacter;
            return yourPassword = new string(passwordToRuing);
        }
    }
}
