using System.ComponentModel;

namespace CarRentService.Pages.Domain;

public enum PageTypeEnum
{
    [Description("Главная")]
    Welcome,

    [Description("Клиенты")]
    Clients,

    [Description("Автомобили")]
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