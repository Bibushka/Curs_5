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
            while (letters.Length != numberOfLetters)
            {
                testChar = GenerateRandom(firstChar, lastChar);
                testString = testChar.ToString();
                if (!badLetters.Contains(testString))
                    letters = letters + testString;
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
        public void UpperLettersCheckNoSimilar()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfCapitalLetters, 
                CheckPasswordFragments(passwordToCheck.yourPassword, 
                string.Join("", Enumerable.Range('A',26).Select(c => (char) c).
                Where(c => !"IO".Contains(c)))));
        }

        [TestMethod]
        public void UpperLettersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, false, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfCapitalLetters,
                CheckPasswordFragments(passwordToCheck.yourPassword,
                string.Join("", Enumerable.Range('A', 26).Select(c => (char)c))));
        }

        [TestMethod]
        public void LowerLettersCheckNoSimilar()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfSmallLetters,
                CheckPasswordFragments(passwordToCheck.yourPassword,
                string.Join("", Enumerable.Range('a', 26).Select(c => (char)c).
                Where(c => !"lo".Contains(c)))));
        }

        [TestMethod]
        public void LowerLettersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, false, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfSmallLetters,
                CheckPasswordFragments(passwordToCheck.yourPassword,
                string.Join("", Enumerable.Range('a', 26).Select(c => (char)c))));
        }

        [TestMethod]
        public void CharactersCheckNoAmbigouos()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfSimbols, CheckPasswordFragments(
                passwordToCheck.yourPassword, "#$%&*+-:=?@^_`|"));
        }

        [TestMethod]
        public void CharactersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, false);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfSimbols, CheckPasswordFragments(
                passwordToCheck.yourPassword, "#$%&'()*+,-./:;<=>?@[]^_`{|}~"));
        }

        [TestMethod]
        public void NumbersCheckNoSimilar()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, true, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfDigits,
                CheckPasswordFragments(passwordToCheck.yourPassword,
                string.Join("", Enumerable.Range(2, 8))));
        }

        [TestMethod]
        public void NumbersCheck()
        {
            var passwordToCheck = new Password(2, 3, 4, 1, false, true);
            passwordToCheck.GeneratePassword();
            Assert.AreEqual(passwordToCheck.numberOfDigits,
                CheckPasswordFragments(passwordToCheck.yourPassword,
                string.Join("", Enumerable.Range(0, 10))));
        }

        public int CheckPasswordFragments(string yourPassword, string badLetters)
        { 
            return yourPassword.Count(l => badLetters.Contains(l));
        }
    }
}