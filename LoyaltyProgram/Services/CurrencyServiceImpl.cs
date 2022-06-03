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
                if (currency.Status == true)
                {
                    currency.Status = false;
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
                if (currency.Status == true)
                {
                    return currency;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Currencies.Where(c => c.Status == true).Count();
        }

        public List<Currency> GetCurrencies()
        {
            return _databaseContext.Currencies.Where(c => c.Status == true).ToList();
        }

        public bool UpdateCurency(Currency currency, int id)
        {
            var currencyDb = GetCurrencyById(id);
            if (currencyDb != null)
            {
                if (currencyDb.Status == true)
                {
                    //brandDb.Status = brand.Status;
                    //brandDb.Name = brand.Name;
                    //brandDb.Description = brand.Description;
                    //brandDb.OrganizationId = brand.OrganizationId;

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

            return false;
        }
    }
}
