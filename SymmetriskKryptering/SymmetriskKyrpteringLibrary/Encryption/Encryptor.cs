using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace SymmetriskKyrpteringLibrary.Encryption
{
    public class Encryptor
    {
        public byte[] Encrypt(byte[] inputToEncrypt, EncryptionProtocolType protocolType, byte[] key, byte[] iv, out string time)
        {
            var watch = new Stopwatch();
            using (var encrypter = GetEncryptionServiceProvider(protocolType))
            {
                watch.Start();
                encrypter.Mode = CipherMode.CBC;
                encrypter.Padding = PaddingMode.PKCS7;

                encrypter.Key = key;
                encrypter.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, encrypter.CreateEncryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(inputToEncrypt, 0, inputToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    time = watch.ElapsedTicks.ToString();
                    watch.Stop();
                    watch.Reset();

                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] inputToDecrypt, EncryptionProtocolType protocolType, byte[] key, byte[] iv, out string time)
        {
            var watch = new Stopwatch();
            using (var encrypter = GetEncryptionServiceProvider(protocolType))
            {
                watch.Start();
                encrypter.Mode = CipherMode.CBC;
                encrypter.Padding = PaddingMode.PKCS7;


                encrypter.Key = key;
                encrypter.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, encrypter.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(inputToDecrypt, 0, inputToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    time = watch.ElapsedTicks.ToString();
                    watch.Stop();
                    watch.Reset();

                    return memoryStream.ToArray();
                }
            }
        }

        private SymmetricAlgorithm GetEncryptionServiceProvider(EncryptionProtocolType protocolType)
        {
            switch (protocolType)
            {
                case EncryptionProtocolType.AES:
                    return new AesCryptoServiceProvider();
                case EncryptionProtocolType.DES:
                    return new DESCryptoServiceProvider();
                case EncryptionProtocolType.TripleDES:
                    return new TripleDESCryptoServiceProvider();
            }

            return null;
        }

        public int GetKeySize(EncryptionProtocolType protocolType)
        {
            switch (protocolType)
            {
                case EncryptionProtocolType.AES:
                    return (int)32;
                case EncryptionProtocolType.DES:
                    return (int)8;
                case EncryptionProtocolType.TripleDES:
                    return (int)24;
            }

            return 0;
        }

        public int GetIvSize(EncryptionProtocolType protocolType)
        {
            switch (protocolType)
            {
                case EncryptionProtocolType.AES:
                    return (int)16;
                case EncryptionProtocolType.DES:
                case EncryptionProtocolType.TripleDES:
                    return (int)8;
            }

            return 0;
        }
    }
}
