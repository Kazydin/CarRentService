using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IDataStoreContext store) : ISeeder
{
    public void Seed()
    {
        store.Add(new Client
        {
            Fio = "Иванов Иван Иванович",
            Age = 34,
            Phone = "+79035356434",
            DriverLicenseNumber = "12 14 65789",
            Login = "ivanov",
            Password = "ivanov123"
        });

        store.Add(new Client
        {
            Fio = "Петров Петр Петрович",
            Age = 29,
            Phone = "+79145567823",
            DriverLicenseNumber = "45 67 23456",
            Login = "petrov",
            Password = "petrov123"
        });

        store.Add(new Client
        {
            Fio = "Сидоров Сидор Сидорович",
            Age = 41,
            Phone = "+79876543210",
            DriverLicenseNumber = "98 76 54321",
            Login = "sidorov",
            Password = "sidorov123"
        });

        store.Add(new Client
        {
            Fio = "Кузнецов Алексей Алексеевич",
            Age = 36,
            Phone = "+79001234567",
            DriverLicenseNumber = "01 23 45678",
            Login = "kuznetsov",
            Password = "kuznetsov123"
        });

        store.Add(new Client
        {
            Fio = "Васильев Василий Васильевич",
            Age = 28,
            Phone = "+79094561234",
            DriverLicenseNumber = "67 89 01234",
            Login = "vasiliev",
            Password = "vasiliev123"
        });

        store.Add(new Client
        {
            Fio = "Морозов Михаил Михайлович",
            Age = 45,
            Phone = "+79123456789",
            DriverLicenseNumber = "45 12 34567",
            Login = "morozov",
            Password = "morozov123"
        });

        store.Add(new Client
        {
            Fio = "Новиков Николай Николаевич",
            Age = 32,
            Phone = "+79219876543",
            DriverLicenseNumber = "23 45 67890",
            Login = "novikov",
            Password = "novikov123"
        });

        store.Add(new Client
        {
            Fio = "Фёдоров Фёдор Фёдорович",
            Age = 38,
            Phone = "+79304567891",
            DriverLicenseNumber = "34 56 78901",
            Login = "fedorov",
            Password = "fedorov123"
        });

        store.Add(new Client
        {
            Fio = "Михайлов Михаил Михайлович",
            Age = 27,
            Phone = "+79401234567",
            DriverLicenseNumber = "12 34 56789",
            Login = "mikhailov",
            Password = "mikhailov123"
        });

        store.Add(new Client
        {
            Fio = "Орлов Олег Олегович",
            Age = 30,
            Phone = "+79567890123",
            DriverLicenseNumber = "78 90 12345",
            Login = "orlov",
            Password = "orlov123"
        });

        store.Add(new Client
        {
            Fio = "Смирнов Сергей Сергеевич",
            Age = 40,
            Phone = "+79612345678",
            DriverLicenseNumber = "56 78 90123",
            Login = "smirnov",
            Password = "smirnov123"
        });

        store.Add(new Client
        {
            Fio = "Попов Павел Павлович",
            Age = 31,
            Phone = "+79789012345",
            DriverLicenseNumber = "89 01 23456",
            Login = "popov",
            Password = "popov123"
        });

        store.Add(new Client
        {
            Fio = "Григорьев Григорий Григорьевич",
            Age = 35,
            Phone = "+79801234567",
            DriverLicenseNumber = "34 56 78901",
            Login = "grigoriev",
            Password = "grigoriev123"
        });

        store.Add(new Client
        {
            Fio = "Зайцев Захар Захарович",
            Age = 33,
            Phone = "+79912345678",
            DriverLicenseNumber = "90 12 34567",
            Login = "zaytsev",
            Password = "zaytsev123"
        });

        store.Add(new Client
        {
            Fio = "Белозеров Борис Борисович",
            Age = 37,
            Phone = "+79023456789",
            DriverLicenseNumber = "12 34 56789",
            Login = "belozerov",
            Password = "belozerov123"
        });

        store.Add(new Client
        {
            Fio = "Егоров Евгений Евгеньевич",
            Age = 26,
            Phone = "+79101234567",
            DriverLicenseNumber = "45 67 89012",
            Login = "egorov",
            Password = "egorov123"
        });

        store.Add(new Client
        {
            Fio = "Дмитриев Дмитрий Дмитриевич",
            Age = 44,
            Phone = "+79234567890",
            DriverLicenseNumber = "67 89 01234",
            Login = "dmitriev",
            Password = "dmitriev123"
        });

        store.Add(new Client
        {
            Fio = "Николаев Николай Николаевич",
            Age = 39,
            Phone = "+79345678901",
            DriverLicenseNumber = "34 56 78901",
            Login = "nikolaev",
            Password = "nikolaev123"
        });

        store.Add(new Client
        {
            Fio = "Громов Григорий Громович",
            Age = 42,
            Phone = "+79456789012",
            DriverLicenseNumber = "12 34 56789",
            Login = "gromov",
            Password = "gromov123"
        });

        store.Add(new Client
        {
            Fio = "Савельев Станислав Станиславович",
            Age = 46,
            Phone = "+79567890123",
            DriverLicenseNumber = "78 90 12345",
            Login = "saveliev",
            Password = "saveliev123"
        });

        store.Add(new Client
        {
            Fio = "Ковалев Константин Константинович",
            Age = 48,
            Phone = "+79167890123",
            DriverLicenseNumber = "23 45 67890",
            Login = "kovalev",
            Password = "kovalev123"
        });

        store.Add(new Client
        {
            Fio = "Широков Сергей Викторович",
            Age = 50,
            Phone = "+79234567891",
            DriverLicenseNumber = "45 67 89012",
            Login = "shirokov",
            Password = "shirokov123"
        });

        store.Add(new Client
        {
            Fio = "Мартынов Максим Алексеевич",
            Age = 27,
            Phone = "+79345678912",
            DriverLicenseNumber = "67 89 01234",
            Login = "martynov",
            Password = "martynov123"
        });

        store.Add(new Client
        {
            Fio = "Захаров Захар Захарович",
            Age = 29,
            Phone = "+79456789023",
            DriverLicenseNumber = "34 56 78901",
            Login = "zaharov",
            Password = "zaharov123"
        });

        store.Add(new Client
        {
            Fio = "Тимофеев Тимофей Тимофеевич",
            Age = 31,
            Phone = "+79567890134",
            DriverLicenseNumber = "12 34 56789",
            Login = "timofeev",
            Password = "timofeev123"
        });

        store.Add(new Client
        {
            Fio = "Никифоров Николай Николаевич",
            Age = 33,
            Phone = "+79678901234",
            DriverLicenseNumber = "78 90 12345",
            Login = "nikiforov",
            Password = "nikiforov123"
        });

        store.Add(new Client
        {
            Fio = "Макаров Михаил Михайлович",
            Age = 40,
            Phone = "+79789012345",
            DriverLicenseNumber = "45 67 89012",
            Login = "makarov",
            Password = "makarov123"
        });

        store.Add(new Client
        {
            Fio = "Игнатьев Игорь Игнатьевич",
            Age = 37,
            Phone = "+79890123456",
            DriverLicenseNumber = "67 89 01234",
            Login = "ignatiev",
            Password = "ignatiev123"
        });

        store.Add(new Client
        {
            Fio = "Филатов Филипп Филиппович",
            Age = 36,
            Phone = "+79901234567",
            DriverLicenseNumber = "34 56 78901",
            Login = "filatov",
            Password = "filatov123"
        });

        store.Add(new Client
        {
            Fio = "Афанасьев Андрей Афанасьевич",
            Age = 45,
            Phone = "+79012345678",
            DriverLicenseNumber = "12 34 56789",
            Login = "afanasiev",
            Password = "afanasiev123"
        });

        store.Add(new Client
        {
            Fio = "Прохоров Павел Прохорович",
            Age = 42,
            Phone = "+79123456789",
            DriverLicenseNumber = "78 90 12345",
            Login = "prokhorov",
            Password = "prokhorov123"
        });

        store.Add(new Client
        {
            Fio = "Родионов Роман Родионович",
            Age = 39,
            Phone = "+79234567890",
            DriverLicenseNumber = "45 67 89012",
            Login = "rodionov",
            Password = "rodionov123"
        });

        store.Add(new Client
        {
            Fio = "Артамонов Александр Артемович",
            Age = 44,
            Phone = "+79345678901",
            DriverLicenseNumber = "67 89 01234",
            Login = "artamonov",
            Password = "artamonov123"
        });

        store.Add(new Client
        {
            Fio = "Рябов Роман Романович",
            Age = 38,
            Phone = "+79456789012",
            DriverLicenseNumber = "34 56 78901",
            Login = "ryabov",
            Password = "ryabov123"
        });

        store.Add(new Client
        {
            Fio = "Титов Тимур Тимурович",
            Age = 35,
            Phone = "+79567890123",
            DriverLicenseNumber = "12 34 56789",
            Login = "titov",
            Password = "titov123"
        });

        store.Add(new Client
        {
            Fio = "Лебедев Леонид Львович",
            Age = 32,
            Phone = "+79678901234",
            DriverLicenseNumber = "78 90 12345",
            Login = "lebedev",
            Password = "lebedev123"
        });

        store.Add(new Client
        {
            Fio = "Соловьев Сергей Сергеевич",
            Age = 30,
            Phone = "+79789012345",
            DriverLicenseNumber = "45 67 89012",
            Login = "soloviev",
            Password = "soloviev123"
        });

        store.Add(new Client
        {
            Fio = "Борисов Борис Борисович",
            Age = 28,
            Phone = "+79890123456",
            DriverLicenseNumber = "67 89 01234",
            Login = "borisov",
            Password = "borisov123"
        });
    }
}