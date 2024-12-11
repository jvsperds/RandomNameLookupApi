using AccountLookupAPI.Interfaces;
using AccountLookupAPI.Models;
using AccountLookupAPI.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace AccountLookupAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCacheService _cacheService;

        public AccountController(IAccountCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [Produces("application/xml")]
        [HttpPost("SearchByAccountNumber")]
        public ActionResult<Root> SearchByAccountNumber([FromBody] ExactLookupRequest request)
        {
            HttpContext.Request.ContentType = "application/xml";

            if (string.IsNullOrWhiteSpace(request.AccountNumber))
            {
                return new Root
                {
                    LookupResponse = new LookupResponse
                    {
                        LookupResults = new ResultWrapper(new List<LookupResult>())
                    }
                };
            }

            if (_cacheService.TryGet(request.AccountNumber, out var cachedResult))
            {
                return new Root
                {
                    LookupResponse = new LookupResponse
                    {
                        LookupResults = new ResultWrapper(new List<LookupResult> { cachedResult })
                    }
                };
            }

            return GenerateRandomLookupResult(request.AccountNumber);
        }
        [Produces("application/xml")]
        [HttpPost("SearchByAccountNumberTA")]
        public ActionResult<Root> SearchByAccountNumberTA([FromBody] ExactLookupRequest request)
        {
            HttpContext.Request.ContentType = "application/xml";

            if (string.IsNullOrWhiteSpace(request.AccountNumber) || request.AccountNumber == "1234567890")
            {
                return new Root
                {
                    LookupResponse = new LookupResponse
                    {
                        LookupResults = new ResultWrapper(new List<LookupResult>())
                    }
                };
            }

            if (_cacheService.TryGet(request.AccountNumber, out var cachedResult))
            {
                return new Root
                {
                    LookupResponse = new LookupResponse
                    {
                        LookupResults = new ResultWrapper(new List<LookupResult> { cachedResult })
                    }
                };
            }

            return GenerateRandomLookupResultTA(request.AccountNumber);
        }

        private ActionResult<Root> GenerateRandomLookupResultTA(string accountNumber)
        {
            var faker = new Faker();
            var results = new List<LookupResult>();
            var result = new LookupResult
            {
                AccountNumber = accountNumber,
                Title = "Mr.",
                FirstName = "John",
                MiddleName = "Hopkins",
                LastName = "Smith",
                Suffix = "Jr.",
                Address1 = "7740 Painter Ave",
                Address2 = "#100",
                City = "Whittier",
                State = "California",
                Zip = "90602",
                Country = "United States of America",
                ReturnCode = 0,
                ErrorMessage = ""
            };
            results.Add(result);
            _cacheService.Set(accountNumber, result);

            var limitedResults = results.ToList();

            return new Root
            {
                LookupResponse = new LookupResponse
                {
                    LookupResults = new ResultWrapper(limitedResults)
                }
            };
        }

        private ActionResult<Root> GenerateRandomLookupResult(string accountNumber)
        {
            var faker = new Faker();
            var results = new List<LookupResult>();
            var result = new LookupResult
            {
                AccountNumber = accountNumber,
                Title = faker.Random.Number(4) == 1 ? faker.Name.Prefix() : "",
                FirstName = faker.Name.FirstName(),
                MiddleName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Suffix = faker.Random.Number(4) == 1 ? faker.Name.Suffix() : "",
                Address1 = faker.Address.StreetAddress(),
                Address2 = faker.Address.SecondaryAddress(),
                City = faker.Address.City(),
                State = faker.Address.State(),
                Zip = faker.Address.ZipCode(),
                Country = "USA",
                ReturnCode = 0,
                ErrorMessage = ""
            };
            results.Add(result);
            _cacheService.Set(accountNumber, result);

            var limitedResults = results.Take(50).ToList();

            return new Root
            {
                LookupResponse = new LookupResponse
                {
                    LookupResults = new ResultWrapper(limitedResults)
                }
            };
        }

        [Produces("application/xml")]
        [HttpPost("SearchByName")]
        public ActionResult<Root> SearchByName([FromBody] WildLookupRequest name)
        {
            HttpContext.Request.ContentType = "application/xml";

            // Generate all results first
            var faker = new Faker();
            var allResults = new List<LookupResult>();

            for (int i = 0; i < 100000; i++)
            {
                var result = new LookupResult
                {
                    Title = faker.Random.Number(4) == 1 ? faker.Name.Prefix() : "",
                    AccountNumber = string.Join("", faker.Random.Digits(10)),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    MiddleName = faker.Name.FirstName(),
                    Suffix = faker.Random.Number(4) == 1 ? faker.Name.Suffix() : "",
                    Address1 = faker.Address.StreetAddress(),
                    Address2 = faker.Address.SecondaryAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    Zip = faker.Address.ZipCode(),
                    Country = "USA",
                    ReturnCode = 0,
                    ErrorMessage = ""
                };

                allResults.Add(result);
            }

            // Filter the generated results based on the input
            var filteredResults = allResults.Where(result =>
                (string.IsNullOrEmpty(name.FirstName) || result.FirstName.Contains(name.FirstName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(name.LastName) || result.LastName.Contains(name.LastName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(name.City) || result.City.Contains(name.City, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(name.State) || result.State.Contains(name.State, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(name.Zip) || result.Zip.Contains(name.Zip, StringComparison.OrdinalIgnoreCase))
            ).Take(50).ToList();

            // Return empty result if no input is provided
            if (string.IsNullOrWhiteSpace(name.FirstName) && string.IsNullOrWhiteSpace(name.LastName))
            {
                return new Root
                {
                    LookupResponse = new LookupResponse
                    {
                        LookupResults = new ResultWrapper(new List<LookupResult>())
                    }
                };
            }

            return new Root
            {
                LookupResponse = new LookupResponse
                {
                    LookupResults = new ResultWrapper(filteredResults)
                }
            };
        }
        [Produces("application/xml")]
        [HttpPost("SearchByNameTA")]
        public ActionResult<Root> SearchByNameTA([FromBody] WildLookupRequest name)
        {
            HttpContext.Request.ContentType = "application/xml";

            if (string.IsNullOrWhiteSpace(name.FirstName) && string.IsNullOrWhiteSpace(name.LastName))
            {
                return new Root
                {
                    LookupResponse = new LookupResponse
                    {
                        LookupResults = new ResultWrapper(new List<LookupResult>())
                    }
                };
            }

            var faker = new Faker();

            var mockData = new LookupResult[]
            {
                new LookupResult
                {
                    Title = "Mr.",
                    Suffix = "Sr.",
                    AccountNumber = "1234567890",
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "A",
                    Address1 = "123 Main St",
                    Address2 = "Apt 4B",
                    City = "Springfield",
                    State = "IL",
                    Zip = "62701",
                    Country = "USA",
                    ReturnCode = 0,
                    ErrorMessage = ""
                },
                new LookupResult
                {
                    Title = "Mr.",
                    Suffix = "Sr.",
                    AccountNumber = "9876543210",
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "B",
                    Address1 = "456 Elm St",
                    Address2 = "Suite 100",
                    City = "Chicago",
                    State = "IL",
                    Zip = "60601",
                    Country = "USA",
                    ReturnCode = 0,
                    ErrorMessage = ""
                },
                new LookupResult
                {
                    Title = "Mr.",
                    Suffix = "Sr.",
                    AccountNumber = "1122334455",
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "C",
                    Address1 = "789 Pine St",
                    Address2 = "Floor 3",
                    City = "Seattle",
                    State = "WA",
                    Zip = "98101",
                    Country = "USA",
                    ReturnCode = 0,
                    ErrorMessage = ""
                },
                new LookupResult
                {
                    Title = "Mr.",
                    Suffix = "Sr.",
                    AccountNumber = "5566778899",
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "D",
                    Address1 = "321 Oak St",
                    Address2 = "",
                    City = "Austin",
                    State = "TX",
                    Zip = "73301",
                    Country = "USA",
                    ReturnCode = 0,
                    ErrorMessage = ""
                },
                new LookupResult
                {
                    Title = "Mr.",
                    Suffix = "Sr.",
                    AccountNumber = "4455667788",
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "E",
                    Address1 = "654 Maple Ave",
                    Address2 = "Unit 5",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94101",
                    Country = "USA",
                    ReturnCode = 0,
                    ErrorMessage = ""
                }
            };

            // Filter the mock data based on the input name
            var results = mockData.Where(result =>
                (string.IsNullOrWhiteSpace(name.FirstName) || result.FirstName.Contains(name.FirstName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(name.LastName) || result.LastName.Contains(name.LastName, StringComparison.OrdinalIgnoreCase)))
                .Take(5)
                .ToList();

            return new Root
            {
                LookupResponse = new LookupResponse
                {
                    LookupResults = new ResultWrapper(results)
                }
            };
        }
    }
}
