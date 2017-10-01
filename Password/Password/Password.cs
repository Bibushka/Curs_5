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
        string smallLetters;
        string capitalLetters;
        string digits;
        string characters;
        Random random;

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
            return CreatePasswordSegment(numberOfCapitalLetters, 'a', 'z');
        }

        public string GenerateCapitalLetters()
        {
            return CreatePasswordSegment(numberOfCapitalLetters, 'A', 'Z');
        }
        
        public string GenerateDigits()
        {
            return CreatePasswordSegment(numberOfDigits, '0', '9');
        }

        public string GenerateCharacters()
        {
            if (numberOfCharacters == 0)
                return "";
            char testChar;
            string badChar = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < numberOfCharacters; i++)
            {
                testChar = GenerateRandom('!', '|');
                string testString = testChar.ToString();
                while (badChar.Contains(testString))
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

        public char GenerateRandom(char firstChar, char lastChar)
        {
            return (char)(random.Next(firstChar, lastChar + 1));
        }

        public string CreatePasswordSegment(int numberOfCharacters, char firstChar, char lastChar)
        {
            if (numberOfCharacters == 0)
                return "";
            string characters = string.Empty;
            for (int i = 0; i < numberOfCharacters; i++)
                characters = characters + GenerateRandom(firstChar, lastChar);
            return characters;
        }

        public void EliminateSimilarCharacters()
        {
            string similarCharacters = "l1Io0O";
            string badChar = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char testChar;
            for (int i = 0; i < yourPassword.Length; i++)
                while (similarCharacters.Contains(yourPassword[i].ToString()))
                {
                    if (i < numberOfSmallLetters)
                        yourPassword = 
                            ChangeCharacterInString(yourPassword, GenerateRandom('a', 'z'), i);
                    if (i < numberOfSmallLetters + numberOfCapitalLetters)
                        yourPassword = 
                            ChangeCharacterInString(yourPassword, GenerateRandom('A', 'Z'), i);
                    if (i < numberOfSmallLetters + numberOfCharacters + numberOfDigits)
                        yourPassword = 
                            ChangeCharacterInString(yourPassword, GenerateRandom('0', '9'), i);
                    while (badChar.Contains(yourPassword[i].ToString()))
                    {
                        testChar = GenerateRandom('!', '|');
                        yourPassword = ChangeCharacterInString(yourPassword, testChar, i);
                    }
                }
        }

        public void EliminateAmbiguousCharacters()
        {
            string ambiguousChar = "{}[]()/\"~,;.<>";
            string badChar = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char testChar;
            for (int i = 0; i < yourPassword.Length; i++)
                while (ambiguousChar.Contains(yourPassword[i].ToString()))
                {
                    if (i < numberOfSmallLetters)
                        yourPassword =
                            ChangeCharacterInString(yourPassword, GenerateRandom('a', 'z'), i);
                    if (i < numberOfSmallLetters + numberOfCapitalLetters)
                        yourPassword =
                            ChangeCharacterInString(yourPassword, GenerateRandom('A', 'Z'), i);
                    if (i < numberOfSmallLetters + numberOfCharacters + numberOfDigits)
                        yourPassword =
                            ChangeCharacterInString(yourPassword, GenerateRandom('0', '9'), i);
                    while (badChar.Contains(yourPassword[i].ToString()))
                    {
                        testChar = GenerateRandom('!', '|');
                        yourPassword = ChangeCharacterInString(yourPassword, testChar, i);
                    }
                }
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
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void IsPasswordInorrectlyComposed()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = 
                passwordToCheck.ChangeCharacterInString(passwordToCheck.yourPassword, '0', 5);
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.EliminateSimilarCharacters();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForSimilarCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.yourPassword = 
                passwordToCheck.ChangeCharacterInString(passwordToCheck.yourPassword, '1', 5);
            Assert.IsFalse(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersTrue()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
            passwordToCheck.GeneratePassword();
            passwordToCheck.EliminateAmbiguousCharacters();
            Assert.IsTrue(ValidatePassword(passwordToCheck));
        }

        [TestMethod]
        public void CheckForAmbiguousCharactersFalse()
        {
            var passwordToCheck = new Password(2, 3, 4, 1);
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
                else
                    countOfCharacters++;
            }
            if (countOfLowerCaseLetters == passwordToCheck.numberOfSmallLetters &&
                countOfUpperCaseLetters == passwordToCheck.numberOfCapitalLetters &&
                countOfDigits == passwordToCheck.numberOfDigits &&
                countOfCharacters == passwordToCheck.numberOfCharacters)
                return true;
            return false;
        }
    }
}