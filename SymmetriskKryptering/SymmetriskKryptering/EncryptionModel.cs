using SymmetriskKyrpteringLibrary.Encryption;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymmetriskKryptering
{
    public class EncryptionModel
    {
        private EncryptionProtocolType protocolType;

        public EncryptionProtocolType ProtocolType
        {
            get { return protocolType; }
            set { protocolType = value; }
        }

        private byte[] key;

        public byte[] Key
        {
            get { return key; }
            set { key = value; }
        }

        private byte[] iv;

        public byte[] Iv
        {
            get { return iv; }
            set { iv = value; }
        }

        private string keyAsString;

        public string KeyAsString
        {
            get { return keyAsString; }
            set { keyAsString = value; }
        }

        private string ivAsString;

        public string IvAsString
        {
            get { return ivAsString; }
            set { ivAsString = value; }
        }

        private string textToEncrypt;

        public string TextToEncrypt
        {
            get { return textToEncrypt; }
            set { textToEncrypt = value; }
        }

        private string encryptedText;

        public string EncryptedText
        {
            get { return encryptedText; }
            set { encryptedText = value; }
        }

        private string timeToEncrypt;

        public string TimeToEncrypt
        {
            get { return timeToEncrypt; }
            set { timeToEncrypt = value; }
        }

        private string timeToDecrypt;

        public string TimeToDecrypt
        {
            get { return timeToDecrypt; }
            set { timeToDecrypt = value; }
        }


    }
}
