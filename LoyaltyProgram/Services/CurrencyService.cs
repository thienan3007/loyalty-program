using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface CurrencyService
    {
        public PagedList<Currency> GetCurrencies(PagingParameters pagingParameters);
        public Currency GetCurrencyById(int id);
        public bool AddCurrency(Currency currency);
        public bool UpdateCurency(Currency currency, int id);
        public bool DeleteCurency(int id);
        public int GetCount();
    }
}
