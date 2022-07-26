using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class MembershipServiceImpl : MembershipService
    {
        private DatabaseContext databaseContext;
        private ISortHelper<Membership> _sortHelper;
        public MembershipServiceImpl(DatabaseContext databaseContext, ISortHelper<Membership> sortHelper)
        {
            this.databaseContext = databaseContext;
            _sortHelper = sortHelper;
        }

        public int AddMembership(Membership membership)
        {
            if (membership != null)
            {
                databaseContext.Memberships.Add(membership);
                databaseContext.SaveChanges();
                return membership.AccountId;
            }
            return 0;
        }

        public bool DeleteMembership(int id)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.AccountId == id);
            if (membership != null)
            {
                if (membership.Status == 1)
                {
                    membership.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public int GetCount()
        {
            return databaseContext.Memberships.Where(m => m.Status == 1).Count();
        }

        public Membership GetMembershipById(int id)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.AccountId == id);
            if (membership != null)
            {
                return membership;
            }
            return null;
        }

        public Membership GetMembership(string email)
        {
            var membership = databaseContext.Memberships.FirstOrDefault(m => m.Email == email);
            if (membership != null)
            {
                return membership;
            }
            return null;
        }

        public PagedList<Membership> GetMemberships(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Membership> memberships;
            if (filterString != null)
            {
                memberships =databaseContext.Memberships.Where(m => m.Status == 1 && m.Email.Contains(filterString));
            }
            else
            {
                memberships = databaseContext.Memberships.Where(m => m.Status == 1);
            }

            var sortedMemberships = _sortHelper.ApplySort(memberships, pagingParameters.OrderBy);
            if (memberships != null)
            {
                return PagedList<Membership>.ToPagedList(sortedMemberships, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateMembership(Membership membership, int id)
        {
            var membershipDb = databaseContext.Memberships.FirstOrDefault(m => m.AccountId == id);
            if (membership != null)
            {
                if (membershipDb != null)
                {
                    if (membershipDb.Status == 1)
                    {
                        if (membership.MembershipEndDate != null)
                            membershipDb.MembershipEndDate = membership.MembershipEndDate;
                        if (membership.CanReceivePromotions != null)
                            membershipDb.CanReceivePromotions = membership.CanReceivePromotions;
                        if (membership.Status != null)
                            membershipDb.Status = membership.Status;
                        if (membership.LastTransactionDate != null)
                            membershipDb.LastTransactionDate = membership.LastTransactionDate;
                        if (membership.Description != null)
                            membershipDb.Description = membership.Description;
                        if (membership.EnrollmentDate != null)
                            membershipDb.EnrollmentDate = membership.EnrollmentDate;
                        if (membership.LoyaltyProgramId != null)
                            membershipDb.LoyaltyProgramId = membership.LoyaltyProgramId;
                        if (membership.MembershipCode != null)
                            membershipDb.MembershipCode = membership.MembershipCode;
                        if (membership.ReferrerMemberId != null)
                            membershipDb.ReferrerMemberId = membership.ReferrerMemberId;
                        if (membership.ReferrerMemberDate != null)
                            membershipDb.ReferrerMemberDate = membership.ReferrerMemberDate;

                        return databaseContext.SaveChanges() > 0;
                    }
                }
            }
            return false;
        }
    }
}
