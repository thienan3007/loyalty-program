using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class MemberReferrerLevelServiceImpl : MemberReferrerLevelService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<MemberReferrerLevel> _sortHelper;
        public MemberReferrerLevelServiceImpl(DatabaseContext databaseContext, ISortHelper<MemberReferrerLevel> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
        }

        public bool AddMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel)
        {

            _databaseContext.MemberReferrerLevels.Add(memberReferrerLevel);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteMemberReferrerLevel(int id)
        {
            var memberReferrerLevel = _databaseContext.MemberReferrerLevels.FirstOrDefault(b => b.Id == id);
            if (memberReferrerLevel != null)
            {
                if (memberReferrerLevel.Status == 1)
                {
                    memberReferrerLevel.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public MemberReferrerLevel GetMemberReferrerLevel(int id)
        {
            var memberReferrerLevel = _databaseContext.MemberReferrerLevels.FirstOrDefault(b => b.Id == id);
            if (memberReferrerLevel != null)
            {
                if (memberReferrerLevel.Status == 1)
                {
                    return memberReferrerLevel;
                }
            }
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.MemberReferrerLevels.Where(b => b.Status == 1).Count();
        }

        public PagedList<MemberReferrerLevel> GetMemberReferrerLevels(PagingParameters pagingParameters)
        {
            var memberReferrerLevels = _databaseContext.MemberReferrerLevels.Where(b => b.Status == 1);
            var sortedList = _sortHelper.ApplySort(memberReferrerLevels, pagingParameters.OrderBy);
            if (memberReferrerLevels != null)
            {
                return PagedList<MemberReferrerLevel>.ToPagedList(sortedList, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel, int id)
        {
            if (memberReferrerLevel != null)
            {
                var memberReferrerLevelDb = GetMemberReferrerLevel(id);
                if (memberReferrerLevelDb != null)
                {
                    if (memberReferrerLevelDb.Status == 1)
                    {
                        if (memberReferrerLevel.TierSequenceNumber != null)
                            memberReferrerLevelDb.TierSequenceNumber = memberReferrerLevel.TierSequenceNumber;
                        if (memberReferrerLevel.RatioReferrerPoints != null)
                            memberReferrerLevelDb.RatioReferrerPoints = memberReferrerLevel.RatioReferrerPoints;
                        if (memberReferrerLevel.Status != null)
                            memberReferrerLevelDb.Status = memberReferrerLevel.Status;
                        if (memberReferrerLevel.Description != null)
                            memberReferrerLevelDb.Description = memberReferrerLevel.Description;

                        return _databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
