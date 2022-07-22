using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class CardServiceImpl : CardService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly MembershipService membershipService;
        private ISortHelper<Card> _sortHelper;
        public CardServiceImpl(DatabaseContext databaseContext, MembershipService membershipService, ISortHelper<Card> sortHelper)
        {
            _databaseContext = databaseContext;
            this.membershipService = membershipService;
            _sortHelper = sortHelper;
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
                    card.Membership = this.membershipService.GetMembershipById((int)card.MembershipId);
                    return card;
                }
            }
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.Cards.Where(b => b.Status == 1).Count();
        }

        public PagedList<Card> GetCards(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Card> cards;
            if (filterString != null)
            {
                cards = _databaseContext.Cards.Where(b =>
                    b.Status == 1 && b.CardholderName.Contains(filterString));
            }
            else
            {
                cards = _databaseContext.Cards.Where(b => b.Status == 1);
            }

            var sortedCards = _sortHelper.ApplySort(cards, pagingParameters.OrderBy);
            if (cards != null)
            {
                return PagedList<Card>.ToPagedList(sortedCards, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
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

        public Card GetCardById(int memberId)
        {
            var card = _databaseContext.Cards.FirstOrDefault(c => c.MembershipId == memberId);
            if (card != null)
            {
                if (card.Status == 1)
                {
                    card.Membership = membershipService.GetMembershipById(memberId);
                    return card;
                }
            }
            return null;
        }
    }
}
