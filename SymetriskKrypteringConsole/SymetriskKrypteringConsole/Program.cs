using System;
using SymetriskKrypteringEncryption;
using Terminal.Gui;

namespace SymetriskKrypteringConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var keyPlain = String.Empty;
            var ivPlain = String.Empty;
            
            // https://migueldeicaza.github.io/gui.cs/api/Terminal.Gui/Terminal.Gui.Rect.html
            // https://sirwan.info/archive/2018/05/02/Developing-Console-based-UI-in-C/
            var encrypter = new Encrypter();
            Application.Init();

            var top = Application.Top;
            
            // Create menu bar
            var menu = new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Quit", "", () => { top.Running = false;}), 
                })
            });
            top.Add(menu);
            
            var window = new Window(new Rect(0, 1, top.Frame.Width, top.Frame.Height-1), "Encrypter");
            top.Add(window);
            
            // Add controls here:
            // Dropdown - ComboBox
            var encoderTypeDropdown = new ComboBox(new Rect(3, 2, 15, 5), Enum.GetValues(typeof(EncryptionProtocols)));
            
            // Buttons
            var generateKeyAndIvButton = new Button();
            var encrypt = new Button();
            var decrypt = new Button();
            
            // Text Fields
            var keyTextField = new TextField();
            var ivTextField = new TextField();
            var AsciiPlainTextField = new TextField();
            var HexPlainTextField = new TextField();
            var AsciiCipherTextField = new TextField();
            var HexCipherTextField = new TextField();
            
            // Labels
            var keyTextLabel = new Label(80, 1, "Key");
            var ivTextLabel = new Label(80, 4, "IV");
            var timeToEncryptLabel = new Label();
            var timeToDecryptLabel = new Label();
            
            window.Add(encoderTypeDropdown, keyTextLabel, ivTextLabel);
            
            Application.Run();
        }
    }
}