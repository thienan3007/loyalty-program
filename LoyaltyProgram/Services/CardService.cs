using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface CardService
    {
        public List<Card> GetCards();
        public Card GetCard(int id);
        public bool Add(Card card);
        public bool Update(Card card, int id);
        public bool Delete(int id);
        public int GetCount();
    }
}
