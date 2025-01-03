using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Home.Pages.Domain;
using Microsoft.UI.Xaml;

namespace CarRentService.Home.Pages.Clients
{
    public class Man
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public sealed partial class ClientsPage : InjectedHomePage
    {
        private ObservableCollection<Man> data;

        public ClientsPage() : base(HomePageTypeEnum.Clients)
        {
            InitializeComponent();

            // Пример данных для таблицы
            data = new ObservableCollection<Man>
            {
                new Man { Id = 1, Name = "Alice", Age = 30 },
                new Man { Id = 2, Name = "Bob", Age = 25 },
                new Man { Id = 3, Name = "Charlie", Age = 35 }
            };

            // Привязываем коллекцию данных к DataGrid
            DataGrid.ItemsSource = data;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var maxId = data.Max(p => p.Id);

            data.Add(new Man()
            {
                Id = maxId,
                Name = "Иван " + maxId,
                Age = 20
            });
        }
    }
}
