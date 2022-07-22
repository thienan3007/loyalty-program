using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class MemberTierServiceImpl : MemberTierService
    {

        private readonly DatabaseContext databaseContext;
        private readonly MembershipService membershipService;
        private ISortHelper<MemberTier> _sortHelper;
        public MemberTierServiceImpl(DatabaseContext databaseContext, MembershipService membershipService, ISortHelper<MemberTier> sortHelper)
        {
            this.databaseContext = databaseContext;
            this.membershipService = membershipService;
            this._sortHelper = sortHelper;
        }

        public bool AddMemberTier(MemberTier memberTier)
        {
            if (memberTier != null)
            {
                databaseContext.MemberTiers.Add(memberTier);
                return databaseContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool DeleteMemberTier(int loyaltyMemberId, int loyaltyTierID)
        {
            var memberTierDb = databaseContext.MemberTiers.FirstOrDefault(m => (m.LoyaltyMemberId == loyaltyMemberId && m.LoyaltyTierId == loyaltyTierID));
            if (memberTierDb != null)
            {
                if (memberTierDb.Status == 1)
                {
                    memberTierDb.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;

        }

        public int GetCount()
        {
            return databaseContext.MemberTiers.Where(m => m.Status == 1).Count();
        }

        public MemberTier GetMemberTier(int loyaltyMemberId, int loyaltyTierID)
        {
            var memberTierDb = databaseContext.MemberTiers.FirstOrDefault(m => (m.LoyaltyMemberId == loyaltyMemberId && m.LoyaltyTierId == loyaltyTierID));
            if (memberTierDb != null)
            {
                if (memberTierDb.Status == 1)
                {
                    memberTierDb.LoyaltyMember = this.membershipService.GetMembershipById(loyaltyMemberId);
                    return memberTierDb;
                }
            }
            return null;
        }

        public MemberTier GetMemberTierCurrent(int loyaltyMemberId)
        {
            var memberTier = databaseContext.MemberTiers.Where(m => m.LoyaltyMemberId == loyaltyMemberId).OrderByDescending(m => m.Id).First();
            if (memberTier != null)
            {
                if (memberTier.Status == 1)
                {
                    memberTier.LoyaltyMember = this.membershipService.GetMembershipById(loyaltyMemberId);
                    return memberTier;
                }
            }
            return null;
        }

        public PagedList<MemberTier> GetMemberTiers(int loyaltyMemberId, PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<MemberTier> memberTierList;
            if (filterString != null)
            {
                memberTierList = databaseContext.MemberTiers.Where(m => m.LoyaltyMemberId == loyaltyMemberId && m.Status == 1 && m.Name.Contains(filterString));
            }
            else
            {
                memberTierList = databaseContext.MemberTiers.Where(m => m.LoyaltyMemberId == loyaltyMemberId && m.Status == 1);
            }

            var sortedMemberTierList = _sortHelper.ApplySort(memberTierList, pagingParameters.OrderBy);
            //foreach (MemberTier memberTier in memberTierList)
            //{
            //    memberTier.LoyaltyMember = this.membershipService.GetMembershipById(loyaltyMemberId);
            //}
            return
                PagedList<MemberTier>.ToPagedList(sortedMemberTierList, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public PagedList<MemberTier> GetMemberTiers(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<MemberTier> memberTiers;
            if (filterString != null)
            {
                memberTiers = databaseContext.MemberTiers.Where(c => c.Name.Contains(filterString));
            }
            else
            {
                memberTiers = databaseContext.MemberTiers;
            }

            var sortedMemberTiers = _sortHelper.ApplySort(memberTiers, pagingParameters.OrderBy);
            if (memberTiers != null)
            {
                return PagedList<MemberTier>.ToPagedList(sortedMemberTiers, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null; ;
        }

        public bool UpdateMemberTier(MemberTier memberTier, int loyaltyMemberId, int loyaltyTierId)
        {
            if (memberTier != null)
            {
                var memberTierDb = databaseContext.MemberTiers.FirstOrDefault(m => (m.LoyaltyMemberId == loyaltyMemberId && m.LoyaltyTierId == loyaltyTierId));
                if (memberTierDb != null)
                {
                    if (memberTierDb.Status == 1)
                    {
                        if (memberTier.Name != null)
                            memberTierDb.Name = memberTier.Name;
                        if (memberTier.EffectiveDate != null)
                            memberTierDb.EffectiveDate = memberTier.EffectiveDate;
                        if (memberTier.ExpirationDate != null)
                            memberTierDb.ExpirationDate = memberTier.ExpirationDate;
                        if (memberTier.UpdateTierDate != null)
                            memberTierDb.UpdateTierDate = memberTierDb.UpdateTierDate;
                        if (memberTier.Status != null)
                            memberTierDb.Status = memberTier.Status;
                        if (memberTier.Description != null)
                            memberTierDb.Description = memberTier.Description;

                        return databaseContext.SaveChanges() > 0;
                    }
                }
            }
            return false;
        }
    }
}
