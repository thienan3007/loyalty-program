using System.Globalization;
using System.Net.Http.Headers;
using EntityFrameworkCore.Triggered;
using LoyaltyProgram.Areas.Admin.Controllers;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
namespace LoyaltyProgram.Utils
{
    public class AfterAddMemberTier : IAfterSaveTrigger<MemberTier>
    {
        readonly DatabaseContext _databaseContext;
        public AfterAddMemberTier(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AfterSave(ITriggerContext<MemberTier> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {

                
                //get the member tier that just added 
                var tier = _databaseContext.Tiers.First(t => t.Id == context.Entity.LoyaltyTierId);

                var sequenceNumber = tier.SequenceNumber;

                var ok = AddVoucherWalletTier(context.Entity.LoyaltyMemberId, sequenceNumber);

                if (ok)
                {
                    var notification = new NotificationModel()
                    {

                        IsAndroidDevice = true,
                        Notification = new Noti()
                        {
                            AccountId = context.Entity.LoyaltyMemberId,
                            Body = "Congratulation! You have just reached Tier " + sequenceNumber + ", and you have just got an up tier voucher. Let's check it out!",
                            Title = "Up Tier",
                            IsRead = false
                        }
                    };
                    bool result = false;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://13.232.213.53/");
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsJsonAsync("api/v1/notification/send", notification).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            result = true;
                            Console.Write("Success");
                        }
                        else
                        {
                            Console.Write("error");
                        }
                    }

                    if (result)
                    {
                        notification.Notification.Date = DateTime.Now;


                        _databaseContext.Notis.Add(notification.Notification);
                        await _databaseContext.SaveChangesAsync();
                    }
                }
            }

            await Task.CompletedTask;
        }

        private bool AddVoucherWalletTier(int memberId, int? tierSequenceNumber)
        {
            bool result = false;
            int voucherId = 0;
            switch (tierSequenceNumber)
            {
                case 1:
                    voucherId = 22;
                    break;
                case 2:
                    voucherId = 23;
                    break;
                case 3:
                    voucherId = 24;
                    break;
                case 4:
                    voucherId = 25;
                    break;
                case 5:
                    voucherId = 26;
                    break;

            }

            var voucher = _databaseContext.VoucherDefinitions.FirstOrDefault(v => v.Id == voucherId);

            var voucherWallet = new VoucherWallet()
            {
                MembershipId = memberId,
                VoucherDefinitionId = voucherId
            };


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://13.232.213.53/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJlbWFpbCI6ImxldHJhbmR1eWFuaDEwNDIwMDBAZ21haWwuY29tIiwianRpIjoiNzlkMGNmOWQtNjU1NS00NmFjLWJjMjMtOGNjZjdjZGFiMWJkIiwicm9sZSI6ImFkbWluIiwiZXhwIjoxNjU3MjcyMTI4LCJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vbG95YWx0eS1wbGF0Zm9ybS1kYmIwNSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDk2MjIvaW5kZXguaHRtbCJ9.wSyDew5olENMVyDV7ajJlpir4NrG_zoRdo65o61bfl8");
                var response = client.PostAsync($"api/v1/voucher-wallets/present/{memberId}/{voucherId}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("error");
                }
            }

            return result;
        }
    }


}
