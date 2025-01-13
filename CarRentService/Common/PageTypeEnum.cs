using System.ComponentModel;

using CarRentService.Common.Attributes;
using CarRentService.Pages.Cars;
using CarRentService.Pages.Clients.ViewClient;
using CarRentService.Pages.Welcome;
using ClientsTablePage = CarRentService.Pages.Clients.ClientsTable.ClientsTablePage;

namespace CarRentService.Common;

public enum PageTypeEnum
{
    [PageType(typeof(WelcomePage))]
    Welcome,

    [PageType(typeof(ClientsTablePage))]
    Clients,

    [PageType(typeof(ViewClientPage))]
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