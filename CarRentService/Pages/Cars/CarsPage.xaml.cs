using CarRentService.Common.Abstract;
using CarRentService.Pages.Domain;

namespace CarRentService.Pages.Cars
{
    public sealed partial class CarsPage : NavigationPage
    {
        public CarsPage() : base(PageTypeEnum.Cars, "Автомобили")
        {
            InitializeComponent();
        }
    }
}
