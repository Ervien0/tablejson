using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace tablejson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class user
        {
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }

        string filepatch = "Users.json";
        ObservableCollection<user> _users = new ObservableCollection<user>();
        public MainWindow()
        {
            InitializeComponent();
            Loaddata();
            main_datagrid.ItemsSource = _users;
        }
        void Loaddata()
        {
            string jsonstring = File.ReadAllText(filepatch);
            if (String.IsNullOrWhiteSpace(jsonstring))
            {
                user user = new user()
                {
                    name = "111",
                    password = "111",
                    email = "111"
                };
                _users.Add(user);
            }
            else
            {
                _users = JsonSerializer.Deserialize<ObservableCollection<user>>(jsonstring);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var option = new JsonSerializerOptions { WriteIndented = true };
            string jsonstring = JsonSerializer.Serialize(_users, option);
            File.WriteAllText(filepatch, jsonstring);
        }
    }
}