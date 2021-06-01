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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Store_Monitoring
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String ServerAddress;
        public String CellsUid;
        public String PacksUid;
        public String PalletsUid;
        public String Username;
        public String Password;
        public String ApplicationToken;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;
            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (settingsWindow.ShowDialog() == true)
            {
                ServerAddress = settingsWindow.ServerAddress_textBox.Text;
                Username = settingsWindow.Username_textBox.Text;
                Password = settingsWindow.Password_textBox.Text;
                ApplicationToken = settingsWindow.ApplicationToken_textBox.Text;
                PalletsUid = settingsWindow.PalletUid_textBox.Text;
                CellsUid = settingsWindow.CellsUid_textBox.Text;
                PacksUid = settingsWindow.PacksUid_textBox.Text;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private bool LoadSettings()
        {
            if (!File.Exists(@"..\..\settings.config"))
            {
                return false;
            }

            foreach (var str in File.ReadLines(@"..\..\settings.config"))
            {
                var lex = str.Split('=');
                if (lex.Count() < 2) continue;
                switch (lex[0])
                {
                    case "ServerAddress":
                        ServerAddress = lex[1];
                        break;
                    case "Username":
                        Username = lex[1];
                        break;
                    case "Password":
                        Password = lex[1];
                        break;
                    case "ApplicationToken":
                        ApplicationToken = lex[1];
                        break;
                    case "CellsUid":
                        CellsUid = lex[1];
                        break;
                    case "PacksUid":
                        PacksUid = lex[1];
                        break;
                    case "PalletsUid":
                        PalletsUid = lex[1];
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        async Task<List<IEntity>> GetCellsFromServerAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var api = new APIRequest(ServerAddress, Username, Password, ApplicationToken, CellsUid, PacksUid, PalletsUid);
                    return Parser.GetCells(api.GetCells());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            });
        }

        async void GetDataFromServerAsync()
        {
            var cellTask = GetCellsFromServerAsync();
            await Task.WhenAll(new[] { cellTask });
            CellsListView.ItemsSource = cellTask.Result;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            if (!LoadSettings())
            {
                SettingsButton_Click(null, null);
            }

            List<Task<List<IEntity>>> tasks = new List<Task<List<IEntity>>> { GetCellsFromServerAsync() };
            Task.WhenAll(tasks);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetDataFromServerAsync();
        }
    }
}
