using System;
using Qrist.Interfaces;

namespace Qrist.Infrastructure
{
    public class CodeGenerator : ICodeGenerator
    {
        private const string AllCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public string Generate() => GenerateRandomString(AllCharacters);

        private static string GenerateRandomString(string characters, int length = 128)
        {
            var stringChars = new char[length];

            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = characters[random.Next(characters.Length)];
            }

            return new string(stringChars);
        }
    }
}
