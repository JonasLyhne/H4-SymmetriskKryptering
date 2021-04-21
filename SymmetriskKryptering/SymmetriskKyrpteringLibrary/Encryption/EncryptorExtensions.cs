using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace SymmetriskKyrpteringLibrary.Encryption
{
    public static class EncryptorExtensions
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

        public static byte[] ConvertStringToByteArray(string input)
        {
            return Convert.FromBase64String(input);
        }

        public static byte[] StringToAscii(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }
        public static string StringToHex(string input)
        {
            string result = string.Empty;
            char[] values = input.ToCharArray();
            foreach (var value in values)
            {
                int v = Convert.ToInt32(value);
                result += string.Format("{0:x}", v);
            }
            return result;
        }
    }
}
