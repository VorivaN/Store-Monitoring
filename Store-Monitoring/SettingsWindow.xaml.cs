using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

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
            this.DialogResult = true;
            this.Close();
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
    }
}
