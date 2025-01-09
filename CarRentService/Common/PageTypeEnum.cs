using System;
using System.ComponentModel;

using CarRentService.Common.Attributes;
using CarRentService.Pages.Cars;
using CarRentService.Pages.Clients;
using CarRentService.Pages.Clients.EditClient;
using CarRentService.Pages.Welcome;
using ClientsPage = CarRentService.Pages.Clients.ViewClients.ClientsPage;

namespace CarRentService.Common;

public enum PageTypeEnum
{
    [PageType(typeof(WelcomePage))]
    Welcome,

    [PageType(typeof(ClientsPage))]
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