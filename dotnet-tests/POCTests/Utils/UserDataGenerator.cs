using Bogus;
using System;
using System.Collections.Generic;

namespace POCTests.Utils;

public static class UserDataGenerator
{
    private static Faker faker = new Faker("es");

    public static (string name, string email, string age, string phone, string address, string country, string gender, string birthDate, string company, string position, string experience, List<string> languages, string salary, string availability, string contractType, string bio, string skills, bool acceptTerms, bool subscribeNewsletter) GenerateUser()
    {
        return (
            faker.Name.FullName(),
            faker.Internet.Email(),
            faker.Random.Int(18, 65).ToString(),
            faker.Phone.PhoneNumber("##########"),
            faker.Address.StreetAddress(),
            "mexico",
            faker.PickRandom(new[] { "masculino", "femenino", "otro" }),
            faker.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd"),
            faker.Company.CompanyName(),
            faker.Name.JobTitle(),
            faker.Random.Int(1, 40).ToString(),
            new List<string> { "español", "inglés" },
            (faker.Random.Int(0, 40) * 5000).ToString(),
            faker.PickRandom(new[] { "inmediata", "1 mes", "2 meses" }),
            faker.PickRandom(new[] { "tiempo-completo", "medio-tiempo", "temporal" }),
            faker.Lorem.Sentence(),
            "C#, JavaScript, SQL, Docker, AWS",
            true,
            faker.Random.Bool()
        );
    }
}