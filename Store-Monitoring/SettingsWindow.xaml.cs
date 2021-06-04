using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Store_Monitoring
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            var main = (MainWindow)Application.Current.MainWindow;
            Username_textBox.Text = main.Username;
            Password_textBox.Text = main.Password;
            ServerAddress_textBox.Text = main.ServerAddress;
            ApplicationToken_textBox.Text = main.ApplicationToken;
            PalletUid_textBox.Text = main.PalletsUid;
            PacksUid_textBox.Text = main.PacksUid;
            CellsUid_textBox.Text = main.CellsUid;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool AllRequiredPropertiesFilled = true;

            if (String.IsNullOrEmpty(ServerAddress_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (String.IsNullOrEmpty(Username_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (String.IsNullOrEmpty(Password_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (String.IsNullOrEmpty(ApplicationToken_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (String.IsNullOrEmpty(CellsUid_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (String.IsNullOrEmpty(PalletUid_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (String.IsNullOrEmpty(PacksUid_textBox.Text))
            {
                AllRequiredPropertiesFilled = false;
            }

            if (AllRequiredPropertiesFilled)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Not all required properties filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }
        private void ChooseConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog().Value)
            {
                foreach (var str in File.ReadLines(openFileDialog.FileName))
                {
                    var lex = str.Split('=');
                    if (lex.Count() < 2) continue;

                    switch (lex[0])
                    {
                        case "ServerAddress":
                            ServerAddress_textBox.Text = lex[1];
                            break;
                        case "Username":
                            Username_textBox.Text = lex[1];
                            break;
                        case "Password":
                            Password_textBox.Text = lex[1];
                            break;
                        case "ApplicationToken":
                            ApplicationToken_textBox.Text = lex[1];
                            break;
                        case "CellsUid":
                            CellsUid_textBox.Text = lex[1];
                            break;
                        case "PacksUid":
                            PacksUid_textBox.Text = lex[1];
                            break;
                        case "PalletsUid":
                            PalletUid_textBox.Text = lex[1];
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void HintButton_Click(object sender, RoutedEventArgs e)
        {
            String hint =
@"Configuration file must be the same format:

<property1 name>=<property1 value>
<property2 name>=<property2 value>
etc...

For examlpe:
ServerAddress=127.0.0.1:8000
Username=ivan";
            MessageBox.Show(hint);
        }
    }
}
