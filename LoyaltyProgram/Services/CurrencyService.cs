using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface CurrencyService
    {
        public List<Currency> GetCurrencies();
        public Currency GetCurrencyById(int id);
        public bool AddCurrency(Currency currency);
        public bool UpdateCurency(Currency currency, int id);
        public bool DeleteCurency(int id);
        public int GetCount();
    }
}
