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
    }
}
