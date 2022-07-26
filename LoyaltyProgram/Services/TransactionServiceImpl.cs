using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class TransactionServiceImpl : TransactionService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<Transaction> _sortHelper;
        public TransactionServiceImpl(DatabaseContext databaseContext, ISortHelper<Transaction> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
        }

        public bool Add(Transaction transaction)
        {

            _databaseContext.Transactions.Add(transaction);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var transaction = _databaseContext.Transactions.FirstOrDefault(b => b.Id == id);
            if (transaction != null)
            {
                if (transaction.Status == 1)
                {
                    transaction.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public Transaction GetTransaction(int id)
        {
            var transaction = _databaseContext.Transactions.FirstOrDefault(b => b.Id == id);
            if (transaction != null)
            {
                if (transaction.Status == 1)
                {
                    return transaction;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Transactions.Where(b => b.Status == 1).Count();
        }

        public PagedList<Transaction> GetTransactions(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Transaction> transactions;
            if (filterString != null)
            {
                transactions = _databaseContext.Transactions.Where(b => b.Status == 1 && _databaseContext.Memberships.First(m => m.AccountId == b.MembershipId).Email.Contains(filterString));
            }
            else
            {
                transactions = _databaseContext.Transactions.Where(b => b.Status == 1);
            }

            var sortedTransaction = _sortHelper.ApplySort(transactions, pagingParameters.OrderBy);
            if (transactions != null)
            {
                return PagedList<Transaction>.ToPagedList(sortedTransaction, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool Update(Transaction transaction, int id)
        {
            var transactionDb = GetTransaction(id);
            if (transaction != null)
            {
                if (transactionDb != null)
                {
                    if (transactionDb.Status == 1)
                    {
                        if (transaction.TransactionDate != null)
                            transactionDb.TransactionDate = transaction.TransactionDate;
                        if (transaction.Status != null)
                            transactionDb.Status = transaction.Status;
                        if (transaction.Description != null)
                            transactionDb.Description = transaction.Description;
                        if (transaction.CardId != null)
                            transactionDb.CardId = transaction.CardId;
                        if (transaction.TotalPrice != null)
                            transactionDb.TotalPrice = transaction.TotalPrice;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }

        public PagedList<Transaction> GetTransactionsByMember(int membershipId, PagingParameters pagingParameters)
        {
            var transactions = _databaseContext.Transactions.Where(t => t.MembershipId == membershipId);
            var sortedTransactions = _sortHelper.ApplySort(transactions, pagingParameters.OrderBy);
            if (transactions != null)
            {
                return PagedList<Transaction>.ToPagedList(sortedTransactions, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public int GetCount(int membershipId)
        {
            return _databaseContext.Transactions.Count(t => t.MembershipId == membershipId);
        }

        public List<Transaction> GetAllTransactionsByMember(int membershipId)
        {
            var transactions = _databaseContext.Transactions.Where(t => t.MembershipId == membershipId).ToList();
            if (transactions != null)
            {
                return transactions;
            }
            return null;
        }
    }
}
