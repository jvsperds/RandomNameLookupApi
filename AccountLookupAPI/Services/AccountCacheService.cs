using AccountLookupAPI.Interfaces;
using AccountLookupAPI.Models;

namespace AccountLookupAPI.Services
{
    public class AccountCacheService : IAccountCacheService
    {
        private readonly Dictionary<string, LookupResult> _cache;

        public AccountCacheService()
        {
            _cache = new Dictionary<string, LookupResult>();
        }

        public bool TryGet(string accountNumber, out LookupResult result)
        {
            return _cache.TryGetValue(accountNumber, out result);
        }

        public void Set(string accountNumber, LookupResult result)
        {
            _cache[accountNumber] = result;
        }
    }
}
