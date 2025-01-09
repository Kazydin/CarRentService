using System.ComponentModel;

using CarRentService.Common.Attributes;
using CarRentService.Pages.Cars;
using CarRentService.Pages.Clients.EditClient;
using CarRentService.Pages.Welcome;
using ClientsTablePage = CarRentService.Pages.Clients.ClientsTable.ClientsTablePage;

namespace CarRentService.Common;

public enum PageTypeEnum
{
    [PageType(typeof(WelcomePage))]
    Welcome,

    [PageType(typeof(ClientsTablePage))]
    Clients,

    [PageType(typeof(EditClientPage))]
    EditClient,

    [PageType(typeof(CarsPage))]
    Cars,

    [Description("Договора")]
    Contracts,

    [Description("Платежи")]
    Payments,

    [Description("Страховка")]
    Insurances,

    [Description("Филиалы")]
    Branches,

    [Description("Управление персоналом")]
    ManageAdmins,

    [Description("Пользователь")]
    Account,

    [Description("Выход")]
    Exit,
}