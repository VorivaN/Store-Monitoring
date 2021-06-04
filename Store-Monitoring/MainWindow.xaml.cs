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
        public String ConfigFilePath = @"..\..\settings.txt";

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

        /// <summary>
        /// Вызывает окно настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Скрывает все поля свойств на главном экране
        /// </summary>
        private void HideAllProperties()
        {
            ArticlePropertyGrid.Visibility = Visibility.Collapsed;
            NamePropertyGrid.Visibility = Visibility.Collapsed;
            FullNamePropertyGrid.Visibility = Visibility.Collapsed;
            GuidPropertyGrid.Visibility = Visibility.Collapsed;
            TypePropertyGrid.Visibility = Visibility.Collapsed;
        }
        private void CellsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllProperties();

            var cell = CellsListView.SelectedItem as Cell;
            if (cell != null)
            {
                NamePropertyGrid.Visibility = Visibility.Visible;
                GuidPropertyGrid.Visibility = Visibility.Visible;
                TypePropertyGrid.Visibility = Visibility.Visible;

                NameProperty.Text = cell.Name;
                GuidProperty.Text = cell.Guid.ToString();
                TypeProperty.Text = cell.Type.ToString();
            }
        }
        private void PalletsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllProperties();

            var pallet = PalletsListView.SelectedItem as Pallet;
            if (pallet != null)
            {
                NamePropertyGrid.Visibility = Visibility.Visible;
                GuidPropertyGrid.Visibility = Visibility.Visible;
                TypePropertyGrid.Visibility = Visibility.Visible;

                NameProperty.Text = pallet.Name;
                GuidProperty.Text = pallet.Guid.ToString();
                TypeProperty.Text = pallet.Type.ToString();
            }
        }
        private void PacksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllProperties();

            var pack = PacksListView.SelectedItem as Pack;
            if (pack != null) {
                ArticlePropertyGrid.Visibility = Visibility.Visible;
                NamePropertyGrid.Visibility = Visibility.Visible;
                FullNamePropertyGrid.Visibility = Visibility.Visible;
                GuidPropertyGrid.Visibility = Visibility.Visible;
                TypePropertyGrid.Visibility = Visibility.Visible;

                NameProperty.Text = pack.Name;
                GuidProperty.Text = pack.Guid.ToString();
                TypeProperty.Text = pack.Type.ToString();
                FullNameProperty.Text = pack.FullName;
                ArticleProperty.Text = pack.Article;
            }
        }
      
        /// <summary>
        /// Получает настройки из файла конфигурации по умолчанию
        /// </summary>
        /// <returns></returns>
        private bool LoadSettings()
        {
            if (!File.Exists(ConfigFilePath))
            {
                return false;
            }

            foreach (var str in File.ReadLines(ConfigFilePath))
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            if (!LoadSettings())
            {
                SettingsButton_Click(null, null);
            }
            GetDataFromServerAsync();
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CellsListView.ItemsSource = null;
            GetDataFromServerAsync();
        }

        /// <summary>
        /// Асинхронно получает данные с сервера
        /// </summary>
        async void GetDataFromServerAsync()
        {
            var cellTask = GetCellsFromServerAsync();
            await cellTask;
            CellsListView.ItemsSource = cellTask.Result;

            var packTask = GetPacksFromServerAsync();
            var palletTask = GetPalletsFromServerAsync();
            await Task.WhenAll(new[] { packTask, palletTask });
            PacksListView.ItemsSource = packTask.Result;
            PalletsListView.ItemsSource = palletTask.Result;
        }

        /// <summary>
        /// Асинхронно получает ячейки с сервера
        /// </summary>
        /// <returns></returns>
        async Task<List<IEntity>> GetCellsFromServerAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var api = new APIRequest(ServerAddress, Username, Password, ApplicationToken);
                    return Parser.ParseCells(api.GetEntitiesByTableUid(CellsUid));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            });
        }

        /// <summary>
        /// Асинхронно получает упаковки с сервера
        /// </summary>
        /// <returns></returns>
        async Task<List<IEntity>> GetPacksFromServerAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var api = new APIRequest(ServerAddress, Username, Password, ApplicationToken);
                    return Parser.ParsePacks(api.GetEntitiesByTableUid(PacksUid));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            });
        }

        /// <summary>
        /// Асинхронно получает паллеты с сервера
        /// </summary>
        /// <returns></returns>
        async Task<List<IEntity>> GetPalletsFromServerAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var api = new APIRequest(ServerAddress, Username, Password, ApplicationToken);
                    return Parser.ParsePallets(api.GetEntitiesByTableUid(PalletsUid));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            });
        }
    }
}
