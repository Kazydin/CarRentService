using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IClientService service) : ISeeder
{
    public void Seed()
    {
        service.Add(new Client
        {
            Fio = "Иванов Иван Иванович",
            Age = 34,
            Phone = "+79035356434",
            DriverLicenseNumber = "12 14 657890",
            Login = "ivanov",
            Password = "Ivanov123!"
        });

        service.Add(new Client
        {
            Fio = "Петров Петр Петрович",
            Age = 25,
            Phone = "+79213456789",
            DriverLicenseNumber = "23 45 123456",
            Login = "petrov",
            Password = "Petrov123!"
        });

        service.Add(new Client
        {
            Fio = "Сидоров Сидр Сидорович",
            Age = 30,
            Phone = "+79001234567",
            DriverLicenseNumber = "34 56 789012",
            Login = "sidorov",
            Password = "Sidorov123!"
        });

        service.Add(new Client
        {
            Fio = "Алексеев Алексей Алексеевич",
            Age = 40,
            Phone = "+79161234567",
            DriverLicenseNumber = "45 67 123456",
            Login = "alexeev",
            Password = "Alexeev123!"
        });

        service.Add(new Client
        {
            Fio = "Смирнов Сергей Иванович",
            Age = 50,
            Phone = "+79271234567",
            DriverLicenseNumber = "56 78 345678",
            Login = "smirnov",
            Password = "Smirnov123!"
        });

        service.Add(new Client
        {
            Fio = "Кузнецов Николай Петрович",
            Age = 28,
            Phone = "+79171234567",
            DriverLicenseNumber = "67 89 567890",
            Login = "kuznetsov",
            Password = "Kuznetsov123!"
        });

        service.Add(new Client
        {
            Fio = "Иванова Анна Сергеевна",
            Age = 22,
            Phone = "+79876543210",
            DriverLicenseNumber = "12 34 876543",
            Login = "ivanova",
            Password = "Ivanova123!"
        });

        service.Add(new Client
        {
            Fio = "Петрова Мария Ивановна",
            Age = 27,
            Phone = "+79034567890",
            DriverLicenseNumber = "23 45 654321",
            Login = "petrova",
            Password = "Petrova123!"
        });

        service.Add(new Client
        {
            Fio = "Сидорова Елена Викторовна",
            Age = 32,
            Phone = "+79167895432",
            DriverLicenseNumber = "34 56 123098",
            Login = "sidorova",
            Password = "Sidorova123!"
        });

        service.Add(new Client
        {
            Fio = "Алексеева Екатерина Павловна",
            Age = 36,
            Phone = "+79261239876",
            DriverLicenseNumber = "45 67 908765",
            Login = "alekseeva",
            Password = "Alekseeva123!"
        });

        service.Add(new Client
        {
            Fio = "Смирнова Татьяна Сергеевна",
            Age = 33,
            Phone = "+79991234567",
            DriverLicenseNumber = "56 78 456789",
            Login = "smirnova",
            Password = "Smirnova123!"
        });

        service.Add(new Client
        {
            Fio = "Кузнецова Ирина Андреевна",
            Age = 29,
            Phone = "+79601234567",
            DriverLicenseNumber = "67 89 098765",
            Login = "kuznetsova",
            Password = "Kuznetsova123!"
        });

        service.Add(new Client
        {
            Fio = "Морозов Алексей Владимирович",
            Age = 41,
            Phone = "+79112233445",
            DriverLicenseNumber = "34 12 567890",
            Login = "morozov",
            Password = "Morozov123!"
        });

        service.Add(new Client
        {
            Fio = "Васильев Михаил Иванович",
            Age = 38,
            Phone = "+79874563210",
            DriverLicenseNumber = "23 67 123456",
            Login = "vasiliev",
            Password = "Vasiliev123!"
        });

        service.Add(new Client
        {
            Fio = "Фёдоров Олег Николаевич",
            Age = 42,
            Phone = "+79654321098",
            DriverLicenseNumber = "12 78 345678",
            Login = "fedorov",
            Password = "Fedorov123!"
        });

        service.Add(new Client
        {
            Fio = "Михайлова Юлия Александровна",
            Age = 26,
            Phone = "+79107894567",
            DriverLicenseNumber = "67 23 456789",
            Login = "mikhailova",
            Password = "Mikhailova123!"
        });

        service.Add(new Client
        {
            Fio = "Воронова Наталья Викторовна",
            Age = 31,
            Phone = "+79206789456",
            DriverLicenseNumber = "34 45 678901",
            Login = "voronova",
            Password = "Voronova123!"
        });

        service.Add(new Client
        {
            Fio = "Зайцев Виктор Павлович",
            Age = 37,
            Phone = "+79125678901",
            DriverLicenseNumber = "23 45 678902",
            Login = "zaitsev",
            Password = "Zaitsev123!"
        });

        service.Add(new Client
        {
            Fio = "Егоров Артём Андреевич",
            Age = 28,
            Phone = "+79016784532",
            DriverLicenseNumber = "12 34 678943",
            Login = "egorov",
            Password = "Egorov123!"
        });

        service.Add(new Client
        {
            Fio = "Николаева Марина Олеговна",
            Age = 24,
            Phone = "+79213456781",
            DriverLicenseNumber = "34 56 123456",
            Login = "nikolaeva",
            Password = "Nikolaeva123!"
        });

        service.Add(new Client
        {
            Fio = "Романов Дмитрий Сергеевич",
            Age = 35,
            Phone = "+79125467832",
            DriverLicenseNumber = "45 67 678954",
            Login = "romanov",
            Password = "Romanov123!"
        });

        service.Add(new Client
        {
            Fio = "Григорьева Ольга Алексеевна",
            Age = 29,
            Phone = "+79874562103",
            DriverLicenseNumber = "67 89 098754",
            Login = "grigoreva",
            Password = "Grigoreva123!"
        });

        service.Add(new Client
        {
            Fio = "Кириллов Кирилл Кириллович",
            Age = 39,
            Phone = "+79651237809",
            DriverLicenseNumber = "12 34 567845",
            Login = "kirillov",
            Password = "Kirillov123!"
        });

        service.Add(new Client
        {
            Fio = "Павлов Павел Павлович",
            Age = 45,
            Phone = "+79016789453",
            DriverLicenseNumber = "45 67 678943",
            Login = "pavlov",
            Password = "Pavlov123!"
        });

        service.Add(new Client
        {
            Fio = "Степанова Анастасия Викторовна",
            Age = 30,
            Phone = "+79217894567",
            DriverLicenseNumber = "34 56 567890",
            Login = "stepanova",
            Password = "Stepanova123!"
        });

        service.Add(new Client
        {
            Fio = "Климов Игорь Владимирович",
            Age = 36,
            Phone = "+79124567890",
            DriverLicenseNumber = "56 78 123456",
            Login = "klimov",
            Password = "Klimov123!"
        });

        service.Add(new Client
        {
            Fio = "Савельева Елизавета Николаевна",
            Age = 28,
            Phone = "+79634567890",
            DriverLicenseNumber = "67 89 234567",
            Login = "saveleva",
            Password = "Saveleva123!"
        });

        service.Add(new Client
        {
            Fio = "Семенов Антон Алексеевич",
            Age = 33,
            Phone = "+79874567890",
            DriverLicenseNumber = "12 34 567890",
            Login = "semenov",
            Password = "Semenov123!"
        });

        service.Add(new Client
        {
            Fio = "Чернова Виктория Сергеевна",
            Age = 32,
            Phone = "+79124567890",
            DriverLicenseNumber = "45 67 678912",
            Login = "chernova",
            Password = "Chernova123!"
        });
    }
}