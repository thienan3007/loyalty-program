using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class CardServiceImpl : CardService
    {
        private readonly DatabaseContext _databaseContext;
        public CardServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(Card card)
        {

            _databaseContext.Cards.Add(card);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var card = _databaseContext.Cards.FirstOrDefault(b => b.Id == id);
            if (card != null)
            {
                if (card.Status == 1)
                {
                    card.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public Card GetCard(int id)
        {
            var card = _databaseContext.Cards.FirstOrDefault(b => b.Id == id);
            if (card != null)
            {
                if (card.Status == 1)
                {
                    return card;
                }
            } 
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Cards.Where(b => b.Status == 1).Count();
        }

        public List<Card> GetCards()
        {
            return _databaseContext.Cards.Where(b => b.Status == 1).ToList();
        }

        public bool Update(Card card, int id)
        {
            var cardDb = GetCard(id);
            if (card != null)
            {
                if (cardDb != null)
                {
                    if (cardDb.Status == 1)
                    {
                        if (card.Type != null)
                            cardDb.Type = card.Type;
                        if (card.Discount != null)
                            cardDb.Discount = card.Discount;
                        if (card.CardholderName != null)
                            cardDb.CardholderName = card.CardholderName;
                        if (card.Status != null)
                            cardDb.Status = card.Status;
                        if (card.Amount != null)
                            cardDb.Amount = card.Amount;
                        if (card.CreatedAt != null)
                            cardDb.CreatedAt = card.CreatedAt;
                        if (card.Description != null)
                            cardDb.Description = card.Description;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
