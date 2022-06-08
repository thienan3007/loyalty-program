using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class CurrencyServiceImpl : CurrencyService
    {
        private readonly DatabaseContext _databaseContext;
        public CurrencyServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddCurrency(Currency currency)
        {

            _databaseContext.Currencies.Add(currency);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteCurency(int id)
        {
            var currency = _databaseContext.Currencies.FirstOrDefault(c => c.Id == id);
            if (currency != null)
            {
                if (currency.Status == 1)
                {
                    currency.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public Currency GetCurrencyById(int id)
        {
            var currency= _databaseContext.Currencies.FirstOrDefault(c => c.Id == id);
            if (currency != null)
            {
                if (currency.Status == 1)
                {
                    return currency;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Currencies.Where(c => c.Status == 1).Count();
        }

        public List<Currency> GetCurrencies()
        {
            return _databaseContext.Currencies.Where(c => c.Status == 1).ToList();
        }

        public bool UpdateCurency(Currency currency, int id)
        {
            if (currency != null)
            {
                var currencyDb = GetCurrencyById(id);
                if (currencyDb != null)
                {
                    if (currencyDb.Status == 1)
                    {
                        if (currency.Name != null)
                            currencyDb.Name = currency.Name;
                        if (currency.Status != null)
                            currencyDb.Status = currency.Status;
                        if (currency.Description != null)
                            currencyDb.Description = currency.Description;
                        if (currency.NextResetDate != null)
                            currencyDb.NextResetDate = currency.NextResetDate;
                        if (currency.LoyaltyProgramId != null)
                            currencyDb.LoyaltyProgramId = currency.LoyaltyProgramId;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
