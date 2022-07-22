using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class CurrencyServiceImpl : CurrencyService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<Currency> _sortHelper;
        public CurrencyServiceImpl(DatabaseContext databaseContext, ISortHelper<Currency> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
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

        public PagedList<Currency> GetCurrencies(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Currency> currencies;
            if (filterString != null)
            {
                currencies = _databaseContext.Currencies.Where(c => c.Status == 1 && c.Name.Contains(filterString));
            }
            else
            {
                currencies =_databaseContext.Currencies.Where(c => c.Status == 1);
            }

            var sortedCurrencies = _sortHelper.ApplySort(currencies, pagingParameters.OrderBy);
            if (currencies != null)
            {
                return PagedList<Currency>.ToPagedList(sortedCurrencies, pagingParameters.PageNumber, pagingParameters.PageSize); ;
            }
            return null;
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
