using CarRentService.Common;
using CarRentService.Common.Abstract;

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
