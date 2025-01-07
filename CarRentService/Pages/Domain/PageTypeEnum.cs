using System;
using System.ComponentModel;
using CarRentService.Common.Attributes;
using CarRentService.Pages.Cars;
using CarRentService.Pages.Clients;
using CarRentService.Pages.Welcome;

namespace CarRentService.Pages.Domain;

public enum PageTypeEnum
{
    [Description("Главная")]
    [PageType(typeof(WelcomePage))]
    Welcome,

    [Description("Клиенты")]
    [PageType(typeof(ClientsPage))]
    Clients,

    [Description("Редактирование клиента")]
    EditClient,

    [Description("Автомобили")]
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