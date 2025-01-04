using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IDataStoreContext store) : ISeeder
{
    public void Seed()
    {
        store.Add(new Client { Fio = "Иванов Иван Иванович", Age = 34, DriverLicenseNumber = "12 14 65789", Login = "ivanov", Password = "ivanov123" });
        store.Add(new Client { Fio = "Петров Петр Петрович", Age = 28, DriverLicenseNumber = "13 25 12345", Login = "petrov", Password = "petrov123" });
        store.Add(new Client { Fio = "Сидоров Сидор Сидорович", Age = 40, DriverLicenseNumber = "14 36 54321", Login = "sidorov", Password = "sidorov123" });
        store.Add(new Client { Fio = "Александров Александр Александрович", Age = 29, DriverLicenseNumber = "15 47 11111", Login = "alexandr", Password = "alexandr123" });
        store.Add(new Client { Fio = "Кузнецов Михаил Иванович", Age = 31, DriverLicenseNumber = "16 58 22222", Login = "kuznetsov", Password = "kuznetsov123" });
        store.Add(new Client { Fio = "Федоров Алексей Петрович", Age = 25, DriverLicenseNumber = "17 69 33333", Login = "fedorov", Password = "fedorov123" });
        store.Add(new Client { Fio = "Григорьев Игорь Сергеевич", Age = 32, DriverLicenseNumber = "18 70 44444", Login = "grigoryev", Password = "grigoryev123" });
        store.Add(new Client { Fio = "Лебедев Сергей Владимирович", Age = 27, DriverLicenseNumber = "19 81 55555", Login = "lebedev", Password = "lebedev123" });
        store.Add(new Client { Fio = "Морозов Павел Дмитриевич", Age = 30, DriverLicenseNumber = "20 92 66666", Login = "morozov", Password = "morozov123" });
        store.Add(new Client { Fio = "Николаев Виктор Аркадьевич", Age = 35, DriverLicenseNumber = "21 03 77777", Login = "nikolaev", Password = "nikolaev123" });

        store.Add(new Client { Fio = "Андреев Андрей Иванович", Age = 26, DriverLicenseNumber = "22 14 88888", Login = "andreev", Password = "andreev123" });
        store.Add(new Client { Fio = "Васильев Василий Петрович", Age = 33, DriverLicenseNumber = "23 25 99999", Login = "vasilyev", Password = "vasilyev123" });
        store.Add(new Client { Fio = "Дмитриев Дмитрий Александрович", Age = 24, DriverLicenseNumber = "24 36 10101", Login = "dmitriev", Password = "dmitriev123" });
        store.Add(new Client { Fio = "Егоров Егор Игоревич", Age = 28, DriverLicenseNumber = "25 47 20202", Login = "egorov", Password = "egorov123" });
        store.Add(new Client { Fio = "Захаров Захар Алексеевич", Age = 31, DriverLicenseNumber = "26 58 30303", Login = "zaharov", Password = "zaharov123" });
        store.Add(new Client { Fio = "Игнатьев Игнат Сергеевич", Age = 29, DriverLicenseNumber = "27 69 40404", Login = "ignatiev", Password = "ignatiev123" });
        store.Add(new Client { Fio = "Кириллов Кирилл Петрович", Age = 30, DriverLicenseNumber = "28 70 50505", Login = "kirillov", Password = "kirillov123" });
        store.Add(new Client { Fio = "Максимов Максим Игоревич", Age = 35, DriverLicenseNumber = "29 81 60606", Login = "maksimov", Password = "maksimov123" });
        store.Add(new Client { Fio = "Новиков Николай Владимирович", Age = 34, DriverLicenseNumber = "30 92 70707", Login = "novikov", Password = "novikov123" });
        store.Add(new Client { Fio = "Орлов Олег Дмитриевич", Age = 27, DriverLicenseNumber = "31 03 80808", Login = "orlov", Password = "orlov123" });

        store.Add(new Client { Fio = "Попов Павел Андреевич", Age = 32, DriverLicenseNumber = "32 14 90909", Login = "popov", Password = "popov123" });
        store.Add(new Client { Fio = "Романов Роман Васильевич", Age = 28, DriverLicenseNumber = "33 25 11111", Login = "romanov", Password = "romanov123" });
        store.Add(new Client { Fio = "Семенов Семен Дмитриевич", Age = 30, DriverLicenseNumber = "34 36 22222", Login = "semenov", Password = "semenov123" });
        store.Add(new Client { Fio = "Тарасов Тарас Игоревич", Age = 29, DriverLicenseNumber = "35 47 33333", Login = "tarasov", Password = "tarasov123" });
        store.Add(new Client { Fio = "Ушаков Ульянов Александрович", Age = 33, DriverLicenseNumber = "36 58 44444", Login = "ushakov", Password = "ushakov123" });
        store.Add(new Client { Fio = "Филиппов Филипп Викторович", Age = 26, DriverLicenseNumber = "37 69 55555", Login = "filippov", Password = "filippov123" });
        store.Add(new Client { Fio = "Харитонов Харитон Петрович", Age = 34, DriverLicenseNumber = "38 70 66666", Login = "haritonov", Password = "haritonov123" });
        store.Add(new Client { Fio = "Чернов Чернов Александр", Age = 25, DriverLicenseNumber = "39 81 77777", Login = "chernov", Password = "chernov123" });
        store.Add(new Client { Fio = "Шестаков Шестак Васильевич", Age = 31, DriverLicenseNumber = "40 92 88888", Login = "shestakov", Password = "shestakov123" });
        store.Add(new Client { Fio = "Щербаков Щербак Дмитриевич", Age = 29, DriverLicenseNumber = "41 03 99999", Login = "sherbakov", Password = "sherbakov123" });

        store.Add(new Client { Fio = "Юрьев Юрий Николаевич", Age = 28, DriverLicenseNumber = "42 14 10101", Login = "yuriev", Password = "yuriev123" });
        store.Add(new Client { Fio = "Яковлев Яков Викторович", Age = 32, DriverLicenseNumber = "43 25 20202", Login = "yakovlev", Password = "yakovlev123" });
        store.Add(new Client { Fio = "Афанасьев Афанасий Андреевич", Age = 27, DriverLicenseNumber = "44 36 30303", Login = "afanasiev", Password = "afanasiev123" });
        store.Add(new Client { Fio = "Беликов Белик Иванович", Age = 33, DriverLicenseNumber = "45 47 40404", Login = "belikov", Password = "belikov123" });
        store.Add(new Client { Fio = "Виноградов Виноград Алексей", Age = 29, DriverLicenseNumber = "46 58 50505", Login = "vinogradov", Password = "vinogradov123" });
        store.Add(new Client { Fio = "Голубев Голуб Михаил", Age = 31, DriverLicenseNumber = "47 69 60606", Login = "golubev", Password = "golubev123" });
        store.Add(new Client { Fio = "Денисов Денис Павлович", Age = 30, DriverLicenseNumber = "48 70 70707", Login = "denisov", Password = "denisov123" });
        store.Add(new Client { Fio = "Еремеев Еремей Максимович", Age = 34, DriverLicenseNumber = "49 81 80808", Login = "eremeev", Password = "eremeev123" });
        store.Add(new Client { Fio = "Жуков Жук Олегович", Age = 28, DriverLicenseNumber = "50 92 90909", Login = "zhukov", Password = "zhukov123" });

    }
}