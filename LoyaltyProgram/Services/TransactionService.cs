using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface TransactionService
    {
        public List<Transaction> GetTransactions();
        public Transaction GetTransaction(int id);
        public bool Add(Transaction transaction);
        public bool Update(Transaction transaction,int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
