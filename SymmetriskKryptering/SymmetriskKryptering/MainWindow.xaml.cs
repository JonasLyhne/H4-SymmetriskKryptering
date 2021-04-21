using SymmetriskKyrpteringLibrary.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymmetriskKryptering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selectedProtocolType;
        private EncryptionModel model;
        private Encryptor encryptor;

        public string SelectedProtocolType
        {
            get { return selectedProtocolType; }
            set { selectedProtocolType = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            model = new EncryptionModel();
            encryptor = new Encryptor();
        }

        private void generateKeyAndIv_Click(object sender, RoutedEventArgs e)
        {
            if (selectEncryptionType.SelectedIndex < 0)
            {
                model.ProtocolType = EncryptionProtocolType.AES;
            }
            else
            {
                model.ProtocolType = (EncryptionProtocolType)selectEncryptionType.SelectedItem;
            }
            model.Key = EncryptorExtensions.GenerateRandomNumber(encryptor.GetKeySize(model.ProtocolType));
            model.Iv = EncryptorExtensions.GenerateRandomNumber(encryptor.GetIvSize(model.ProtocolType));
            keyText.Text = EncryptorExtensions.ByteArrayToString(model.Key);
            ivText.Text = EncryptorExtensions.ByteArrayToString(model.Iv);
        }
        private void encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (selectEncryptionType.SelectedIndex < 0)
            {
                model.ProtocolType = EncryptionProtocolType.AES;
            }
            else
            {
                model.ProtocolType = (EncryptionProtocolType)selectEncryptionType.SelectedItem;
            }
            model.TextToEncrypt = textToEncrypt.Text;
            textToEncryptInHex.Text = EncryptorExtensions.StringToHex(model.TextToEncrypt);
            model.EncryptedText = EncryptorExtensions.ByteArrayToString(encryptor.Encrypt(EncryptorExtensions.ConvertStringToByteArray(model.TextToEncrypt), model.ProtocolType, model.Key, model.Iv, out var time));
            timeToEncryptText.Text = "Ticks: " + time;
            asciiCipherText.Text = model.EncryptedText;
            hexCipherText.Text = EncryptorExtensions.StringToHex(model.EncryptedText);
        }
        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (selectEncryptionType.SelectedIndex < 0)
            {
                model.ProtocolType = EncryptionProtocolType.AES;
            }
            else
            {
                model.ProtocolType = (EncryptionProtocolType)selectEncryptionType.SelectedItem;
            }
            model.TextToEncrypt = EncryptorExtensions.ByteArrayToString(encryptor.Encrypt(EncryptorExtensions.ConvertStringToByteArray(model.TextToEncrypt), model.ProtocolType, model.Key, model.Iv, out var time));
            textToEncrypt.Text = model.TextToEncrypt;
            textToEncryptInHex.Text = EncryptorExtensions.StringToHex(model.TextToEncrypt);
            timeToDecryptText.Text = "Ticks: " + time;


        }
    }
}
