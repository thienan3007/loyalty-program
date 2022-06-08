using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class TransactionServiceImpl : TransactionService
    {
        private readonly DatabaseContext _databaseContext;
        public TransactionServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        public List<Transaction> GetTransactions()
        {
            return _databaseContext.Transactions.Where(b => b.Status == 1).ToList();
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
    }
}
