using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface TransactionService
    {
        public PagedList<Transaction> GetTransactions(PagingParameters pagingParameters);
        public PagedList<Transaction> GetTransactionsByMember(int membershipId, PagingParameters pagingParameters);
        public List<Transaction> GetAllTransactionsByMember(int membershipId);
        public Transaction GetTransaction(int id);
        public bool Add(Transaction transaction);
        public bool Update(Transaction transaction,int id);
        public bool Delete(int id);
        public int GetCount();
        public int GetCount(int membershipId);
    }
}
