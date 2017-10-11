using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Password
{
    public struct Password
    {
        public int numberOfSmallLetters;
        public int numberOfCapitalLetters;
        public int numberOfDigits;
        public int numberOfSimbols;
        public string yourPassword;
        public bool noSimilarChars;
        public bool noAmbiguousChars;
        Random random;

        public Password(int numberOfSmallLetters, int numberOfCapitalLetters,
            int numberOfDigits, int numberOfSimbols, bool noSimilarChars, bool noAmbiguousChars)
        {
            this.numberOfSmallLetters = numberOfSmallLetters;
            this.numberOfCapitalLetters = numberOfCapitalLetters;
            this.numberOfDigits = numberOfDigits;
            this.numberOfSimbols = numberOfSimbols;
            this.noSimilarChars = noSimilarChars;
            this.noAmbiguousChars = noAmbiguousChars;
            yourPassword = string.Empty;
            random = new Random();
        }

        public void GeneratePassword()
        {
            yourPassword = GenerateSmallLetters() + GenerateCapitalLetters() +
                GenerateDigits() + GenerateSimbols();
        }

        public string GenerateSmallLetters()
        {
            if (noSimilarChars)
                return CreateNoSimilarPasswordSegment(numberOfSmallLetters, 'a', 'z', "lo");
            return CreatePasswordSegment(numberOfSmallLetters, 'a', 'z');
        }

        public string GenerateCapitalLetters()
        {
            if (noSimilarChars)
                return CreateNoSimilarPasswordSegment(numberOfCapitalLetters, 'A', 'Z', "IO");
            return CreatePasswordSegment(numberOfCapitalLetters, 'A', 'Z');
        }

        public string GenerateDigits()
        {
            if (noSimilarChars)
                return CreatePasswordSegment(numberOfDigits, '2', '9');
            return CreatePasswordSegment(numberOfDigits, '0', '9');
        }

        public string GenerateSimbols()
        {
            string simbols = string.Empty;
            string testString = string.Empty;
            char testChar;
            string badChar;
            if (noAmbiguousChars)
                badChar = "{}[]()/\"~,;.<>0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            else
                badChar = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < numberOfSimbols; i++)
            {
                do
                {
                    testChar = GenerateRandom('!', '|');
                    testString = testChar.ToString();
                }
                while (badChar.Contains(testString));
                simbols = simbols + testChar;
            }
            return simbols;
        }

        public char GenerateRandom(char firstChar, char lastChar)
        {
            return (char)(random.Next(firstChar, lastChar + 1));
        }

        public string CreatePasswordSegment(int numberOfCharacters, char firstChar, char lastChar)
        {
            string characters = string.Empty;
            for (int i = 0; i < numberOfCharacters; i++)
                characters = characters + GenerateRandom(firstChar, lastChar);
            return characters;
        }

        public string CreateNoSimilarPasswordSegment(int numberOfLetters, char firstChar,
            char lastChar, string  badLetters)
        {
            string letters = string.Empty;
            char testChar;
            string testString = string.Empty;
            while (numberOfLetters != 0)
            {
                testChar = GenerateRandom(firstChar, lastChar);
                testString = testChar.ToString();
                while (badLetters.Contains(testString))
                {
                    testChar = GenerateRandom(firstChar, lastChar);
                    testString = testChar.ToString();
                }
                letters = letters + testString;
                numberOfLetters--;
            }
            return letters;
        }

        public string ChangeCharacterInString(string yourPassword, char Character, int position)
        {
            char[] passwordToRuing = yourPassword.ToCharArray();
            passwordToRuing[position] = Character;
            return yourPassword = new string(passwordToRuing);
        }
    }

    [TestClass]
    public class Passwords
    {
        [TestMethod]
        public void IsPasswordCorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void IsPasswordInorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = 
                passwordToCheck.ChangeCharacterInString(passwordToCheck.yourPassword, '0', 3);
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = 
                passwordToCheck.ChangeCharacterInString(passwordToCheck.yourPassword, 'o', 5);
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = 
                passwordToCheck.ChangeCharacterInString(passwordToCheck.yourPassword, '>', 5);
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
                if ("abcdefghijklmnopqrstuvwxyz".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfLowerCaseLetters++;
                if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfUpperCaseLetters++;
                if ("0123456789".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfDigits++;
                if ("!#$%&'()*+,-./:;<=>?@[]^_`{|}~".Contains(passwordToCheck.yourPassword[i].ToString()))
                    countOfCharacters++;
            }
            if (countOfLowerCaseLetters == passwordToCheck.numberOfSmallLetters &&
                countOfUpperCaseLetters == passwordToCheck.numberOfCapitalLetters &&
                countOfDigits == passwordToCheck.numberOfDigits &&
                countOfCharacters == passwordToCheck.numberOfSimbols)
                return true;
            return false;
        }
    }
}