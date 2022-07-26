using EntityFrameworkCore.Triggered;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Utils
{
    public class AfterUpdateMembershipCurreny : IAfterSaveTrigger<MembershipCurrency>
    {
        readonly DatabaseContext _databaseContext;

        public AfterUpdateMembershipCurreny(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AfterSave(ITriggerContext<MembershipCurrency> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Modified)
            {
                //load all tier information (point)
                var tierList = _databaseContext.Tiers.Where(t => t.Status == 1 && t.LoyaltyProgramId == 1);
                var number = 0;

                var currentMemberTier = _databaseContext.MemberTiers.Where(m => m.LoyaltyMemberId == context.Entity.MembershipId).OrderByDescending(m => m.Id).First();

                var memberPoint = context.Entity.TotalPointsAccrued;
                var ordered = tierList.OrderBy("sequenceNumber").ToList();

                for (int i = 0; i < ordered.Count(); i++)
                {
                    var tier = ordered.ElementAt(i);

                    if (memberPoint > tier.MinPoints)
                    {
                        number++;
                    }
                    else
                    {
                        if (number == 0)
                        {
                            number = 1;
                        }

                        break;
                    }
                }

                var currentTier = _databaseContext.Tiers.FirstOrDefault(t => t.Id == currentMemberTier.LoyaltyTierId);

                if (number != currentTier.SequenceNumber)
                {
                    var expectedTier = _databaseContext.Tiers.FirstOrDefault(t =>
                        t.SequenceNumber == number && t.Status == 1 && t.LoyaltyProgramId == 1);

                    var expectedMemberTier = new MemberTier()
                    {
                        LoyaltyMemberId = context.Entity.MembershipId,
                        LoyaltyTierId = expectedTier.Id,
                        Name = expectedTier.Name,
                        ExpirationDate = DateTime.Now.AddMonths(2),
                        EffectiveDate = DateTime.Now,
                        UpdateTierDate = DateTime.Now,
                        Status = 1,
                        Description = expectedTier.Name
                    };

                    _databaseContext.MemberTiers.Add(expectedMemberTier);
                    _databaseContext.Entry(expectedMemberTier).State = EntityState.Added;
                    await _databaseContext.SaveChangesAsync();
                }
            }

            await Task.CompletedTask;
        }
    }
}
