using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class MemberReferrerLevelServiceImpl : MemberReferrerLevelService
    {
        private readonly DatabaseContext _databaseContext;
        public MemberReferrerLevelServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        public List<MemberReferrerLevel> GetMemberReferrerLevels()
        {
            return _databaseContext.MemberReferrerLevels.Where(b => b.Status == 1).ToList();
        }

        public bool UpdateMemberReferrerLevel(MemberReferrerLevel memberReferrerLevel, int id)
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

            return false;
        }
    }
}
