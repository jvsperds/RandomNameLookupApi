using AccountLookupAPI.Models;

namespace AccountLookupAPI.Interfaces
{
    public interface IAccountCacheService
    {
        bool TryGet(string accountNumber, out LookupResult result);
        void Set(string accountNumber, LookupResult result);
    }
}
