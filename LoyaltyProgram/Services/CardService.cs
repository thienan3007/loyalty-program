using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface CardService
    {
        public PagedList<Card> GetCards(PagingParameters pagingParameters);
        public Card GetCard(int id);
        public Card GetCardById(int memberId);
        public bool Add(Card card);
        public bool Update(Card card, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
