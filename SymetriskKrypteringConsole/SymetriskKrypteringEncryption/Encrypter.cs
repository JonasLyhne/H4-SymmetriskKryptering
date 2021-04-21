using System.IO;
using System.Security.Cryptography;

namespace SymetriskKrypteringEncryption
{
    public class Encrypter
    {
        public byte[] Encrypt(string inputToEncrypt, EncryptionProtocols protocol)
        {
            using (var encrypter = GetEncryptionServiceProvider(protocol))
            {
                encrypter.Mode = CipherMode.CBC;
                encrypter.Padding = PaddingMode.PKCS7;

                encrypter.Key = EncrypterExtensions.GenerateRandomNumber(this.GetKeySize(protocol));
                encrypter.IV = EncrypterExtensions.GenerateRandomNumber(this.GetIvSize(protocol));

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, encrypter.CreateEncryptor(), CryptoStreamMode.Write);
                    
                    cryptoStream.Write(
                        EncrypterExtensions.ConvertStringToByteArray(
                            inputToEncrypt, out var length), 0, length);
                    
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Decrypt(string inputToDecrypt, EncryptionProtocols protocol)
        {
            using (var encrypter = GetEncryptionServiceProvider(protocol))
            {
                encrypter.Mode = CipherMode.CBC;
                encrypter.Padding = PaddingMode.PKCS7;

                encrypter.Key = EncrypterExtensions.GenerateRandomNumber(this.GetKeySize(protocol));
                encrypter.IV = EncrypterExtensions.GenerateRandomNumber(this.GetIvSize(protocol));

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, encrypter.CreateDecryptor(), 
                        CryptoStreamMode.Write);
                    
                    cryptoStream.Write(
                        EncrypterExtensions.ConvertStringToByteArray(
                            inputToDecrypt, out var length), 0, length);
                    
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        private SymmetricAlgorithm GetEncryptionServiceProvider(EncryptionProtocols protocol)
        {
            switch (protocol)
            {
                case EncryptionProtocols.AES:
                    return new AesCryptoServiceProvider();
                case EncryptionProtocols.DES:
                    return new DESCryptoServiceProvider();
                case EncryptionProtocols.TripleDES:
                    return new TripleDESCryptoServiceProvider();
            }

            return null;
        }

        private int GetKeySize(EncryptionProtocols protocol)
        {
            switch (protocol)
            {
                case EncryptionProtocols.AES:
                    return (int) 32;
                case EncryptionProtocols.DES:
                    return (int) 8;
                case EncryptionProtocols.TripleDES:
                    return (int) 24;
            }

            return 0;
        }
        
        private int GetIvSize(EncryptionProtocols protocol)
        {
            switch (protocol)
            {
                case EncryptionProtocols.AES:
                    return (int) 16;
                case EncryptionProtocols.DES:
                case EncryptionProtocols.TripleDES:
                    return (int) 8;
            }

            return 0;
        }
    }
}