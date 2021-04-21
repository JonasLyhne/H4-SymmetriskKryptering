using System;
using System.Security.Cryptography;

namespace SymetriskKrypteringEncryption
{
    public static class EncrypterExtensions
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }

        }

        public static string ByteArrayToString(byte[] array)
        {
            return Convert.ToBase64String(array);
        }

        public static byte[] ConvertStringToByteArray(string input, out int length)
        {
            var result = Convert.FromBase64String(input);
            length = result.Length;
            return result;
        }
    }
}