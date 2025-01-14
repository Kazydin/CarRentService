using System.ComponentModel;

using CarRentService.Common.Attributes;
using CarRentService.Pages.Branches.ViewBranch;
using CarRentService.Pages.Cars.CarsTable;
using CarRentService.Pages.Clients.ClientsTable;
using CarRentService.Pages.Clients.ViewClient;
using CarRentService.Pages.Welcome;

namespace CarRentService.Common;

public enum PageTypeEnum
{
    [PageType(typeof(WelcomePage))]
    Welcome,

    [PageType(typeof(ClientsTablePage))]
    Clients,

    [PageType(typeof(ViewClientPage))]
    EditClient,

    [PageType(typeof(CarsTablePage))]
    Cars,

    EditCar,

    [Description("Договора")]
    Contracts,

    [Description("Платежи")]
    Payments,

    [Description("Страховка")]
    Insurances,

    [Description("Филиалы")]
    Branches,

    [PageType(typeof(ViewBranchPage))]
    EditBranch,

    [Description("Управление персоналом")]
    ManageAdmins,

    [Description("Пользователь")]
    Account,

    [Description("Выход")]
    Exit,
}