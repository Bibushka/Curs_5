using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Concurrent;

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
        public void UpperLettersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            if (passwordToCheck.noSimilarChars)
                Assert.IsTrue(CheckForUpperLettersNoSimilar(passwordToCheck));
            Assert.IsTrue(CheckForUpperLetters(passwordToCheck));
        }

        [TestMethod]
        public void LowerLettersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            if (passwordToCheck.noSimilarChars)
                Assert.IsTrue(CheckForLowerLettersNoSimilar(passwordToCheck));
            Assert.IsTrue(CheckForLowerLetters(passwordToCheck));
        }

        [TestMethod]
        public void CharactersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            if (passwordToCheck.noAmbiguousChars)
                Assert.IsTrue(CheckForCharactersNoAmbiguous(passwordToCheck));
            Assert.IsTrue(CheckForCharacters(passwordToCheck));
        }


        [TestMethod]
        public void NumbersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            if (passwordToCheck.noSimilarChars)
                Assert.IsTrue(CheckForNumbersNoSimilar(passwordToCheck));
            Assert.IsTrue(CheckForNumbers(passwordToCheck));
        }

        public bool CheckForUpperLetters(Password passwordToCheck)
        {
            string upperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int upperLettersToCheck = passwordToCheck.yourPassword.Count(l => upperLetters.Contains(l) ) ;
            if (upperLettersToCheck == passwordToCheck.numberOfCapitalLetters)  
                return true;
            return false;
        }

        public bool CheckForLowerLetters(Password passwordToCheck)
        {
            string lowerLetters = "abcdefghijklmnopqrstuvwxyz";
            int lowerLettersToCheck = passwordToCheck.yourPassword.Count(l => lowerLetters.Contains(l));
            if (lowerLettersToCheck == passwordToCheck.numberOfSmallLetters)
                return true;
            return false;
        }

        public bool CheckForUpperLettersNoSimilar(Password passwordToCheck)
        {
            string upperLetters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            int upperLettersToCheck = passwordToCheck.yourPassword.Count(l => upperLetters.Contains(l));
            if (upperLettersToCheck == passwordToCheck.numberOfCapitalLetters)
                return true;
            return false;
        }

        public bool CheckForLowerLettersNoSimilar(Password passwordToCheck)
        {
            string lowerLetters = "abcdefghijkmnpqrstuvwxyz";
            int lowerLettersToCheck = passwordToCheck.yourPassword.Count(l => lowerLetters.Contains(l));
            if (lowerLettersToCheck == passwordToCheck.numberOfSmallLetters)
                return true;
            return false;
        }

        public bool CheckForCharacters(Password passwordToCheck)
        {
            string characters = "#$%&'()*+,-./:;<=>?@[]^_`{|}~";
            int charactersToCheck = passwordToCheck.yourPassword.Count(l => characters.Contains(l));
            if (charactersToCheck == passwordToCheck.numberOfSimbols)
                return true;
            return false;
        }

        public bool CheckForCharactersNoAmbiguous(Password passwordToCheck)
        {
            string characters = "#$%&*+-:=?@^_`|";
            int charactersToCheck = passwordToCheck.yourPassword.Count(l => characters.Contains(l));
            if (charactersToCheck == passwordToCheck.numberOfSimbols)
                return true;
            return false;
        }

        public bool CheckForNumbers(Password passwordToCheck)
        {
            string numbers = "0123456789";
            int numbersToCheck = passwordToCheck.yourPassword.Count(l => numbers.Contains(l));
            if (numbersToCheck == passwordToCheck.numberOfDigits)
                return true;
            return false;
        }

        public bool CheckForNumbersNoSimilar(Password passwordToCheck)
        {
            string numbers = "23456789";
            int numbersToCheck = passwordToCheck.yourPassword.Count(l => numbers.Contains(l));
            if (numbersToCheck == passwordToCheck.numberOfDigits)
                return true;
            return false;
        }
    }
}