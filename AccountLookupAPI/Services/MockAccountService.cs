using AccountLookupAPI.Models;
using Bogus;

namespace AccountLookupAPI.Services
{
    public class MockAccountService
    {
        private readonly Faker<LookupResult> _faker;

        public MockAccountService()
        {
            var faker = new Faker();

            _faker = new Faker<LookupResult>()
                .RuleFor(a => a.AccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(a => a.Title, f => f.Random.Number(1, 4) == 1 ? f.Name.Prefix() : "")
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.MiddleName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.Suffix, f => f.Random.Number(1, 4) == 1 ? f.Name.Suffix() : "")
                .RuleFor(a => a.Address1, f => f.Address.StreetAddress())
                .RuleFor(a => a.Address2, f => f.Address.SecondaryAddress())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.StateAbbr())
                .RuleFor(a => a.Zip, f => f.Address.ZipCode())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.ReturnCode, f => 0)
                .RuleFor(a => a.ErrorMessage, f => "");
        }
    }
}
