using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BadHttpRequestException = Microsoft.AspNetCore.Server.IIS.BadHttpRequestException;

namespace LoyaltyProgram.Areas.Admin.Controllers
{

    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DatabaseContext context;
        public OrderController(DatabaseContext databaseContext)
        
        {
            context = databaseContext;
        }
        [Route("send")]
        [HttpPost]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> SendOrder(String orderJson)
        {
            try
            {
                Order order = JsonConvert.DeserializeObject<Order>(orderJson);
                Debug.WriteLine("applycode: " + order.ApplyCode);
                var memberTier = context.MemberTiers.Where(m => m.LoyaltyMemberId == order.AccountId).OrderByDescending(m => m.Id).First();
                var tier = context.Tiers.FirstOrDefault(t => t.Id == memberTier.LoyaltyTierId);
                var membership = context.Memberships.FirstOrDefault(m => m.AccountId == order.AccountId);
                Membership referrer = null;
                if (membership.ReferrerMemberId != null)
                {
                    referrer = context.Memberships.FirstOrDefault(m => m.AccountId == membership.ReferrerMemberId);
                }
                // if applycode = 1 => apply amount condition 
                if (order.ApplyCode == 1)
                {
                    //get all order amount condition
                    var amountConditionList = context.OrderAmountConditions.ToList();
                    int? point = 0;
                    //check if has discount 
                    if (order.Discount == 0)
                    {
                        //check which point that user can gain
                        for (int i = 0; i < amountConditionList.Count; i++)
                        {
                            var condition = amountConditionList[i];
                            if (order.TotalAmount >= condition.MinOrderAmount &&
                                order.TotalAmount <= condition.NextOrderTotalAmount)
                            {
                                point = Convert.ToInt32(condition.OrderTotalAmountGainPoint * tier.RatioPoints);
                                break;
                            }

                            
                        }

                        if (referrer != null)
                        {
                            var allReferrerLevel = context.MemberReferrerLevels.Where(m => m.Status == 1).ToList();
                            double? ratio = 0;
                            for (int i = 0; i < allReferrerLevel.Count; i++)
                            {
                                var level = allReferrerLevel.ElementAt(i);
                                if (level.TierSequenceNumber == tier.SequenceNumber)
                                {
                                    ratio = level.RatioReferrerPoints;
                                    break;
                                }
                            }

                            var oldReferrerCurrency = context.MembershipCurrencies.FirstOrDefault(m =>
                                m.MembershipId == referrer.AccountId && m.CurrencyId == 2);

                            var referrerPoint = point * ratio;
                            if (oldReferrerCurrency != null)
                            {
                                var referrerCurrency = new MembershipCurrency()
                                {
                                    TotalPointsAccrued = oldReferrerCurrency.TotalPointsAccrued +
                                                         Convert.ToInt32(referrerPoint),
                                    PointsBalance = oldReferrerCurrency.PointsBalance + Convert.ToInt32(referrerPoint),
                                    MembershipId = referrer.AccountId,
                                    CurrencyId = 2
                                };

                                CreateRequest(referrerCurrency);
                            }
                        }

                        var oldMemberCurrency = context.MembershipCurrencies.FirstOrDefault(c =>
                            c.MembershipId == order.AccountId && c.CurrencyId == 2);

                        var memberCurrency = new MembershipCurrency()
                        {
                            TotalPointsAccrued = oldMemberCurrency.TotalPointsAccrued + point,
                            PointsBalance = oldMemberCurrency.PointsBalance + point,
                            MembershipId = order.AccountId,
                            CurrencyId = 2
                        };


                        //create a request to membercurreny controller to update record 
                        var result = CreateRequest(memberCurrency);

                        if (result)
                        {
                            return Ok("Successful");
                        }
                    }
                    else
                    {
                        //has discount 
                        //get total amount after discount 
                        var totalAmountAfterDiscount = order.TotalAmountAfterDiscount;
                        //check which point that user can gain
                        for (int i = 0; i < amountConditionList.Count; i++)
                        {
                            var condition = amountConditionList[i];
                            if (totalAmountAfterDiscount >= condition.OrderTotalAmountAfterDiscount &&
                                totalAmountAfterDiscount <= condition.NextOrderTotalAmountAfterDiscont)
                            {
                                point = Convert.ToInt32(condition.OrderTotalAmountGainPoint * tier.RatioPoints);
                                break;
                            }


                        }

                        if (referrer != null)
                        {
                            var allReferrerLevel = context.MemberReferrerLevels.Where(m => m.Status == 1).ToList();
                            double? ratio = 0;
                            for (int i = 0; i < allReferrerLevel.Count; i++)
                            {
                                var level = allReferrerLevel.ElementAt(i);
                                if (level.TierSequenceNumber == tier.SequenceNumber)
                                {
                                    ratio = level.RatioReferrerPoints;
                                    break;
                                }
                            }

                            var oldReferrerCurrency = context.MembershipCurrencies.FirstOrDefault(m =>
                                m.MembershipId == referrer.AccountId && m.CurrencyId == 2);

                            var referrerPoint = point * ratio;
                            if (oldReferrerCurrency != null)
                            {
                                var referrerCurrency = new MembershipCurrency()
                                {
                                    TotalPointsAccrued = oldReferrerCurrency.TotalPointsAccrued +
                                                         Convert.ToInt32(referrerPoint),
                                    PointsBalance = oldReferrerCurrency.PointsBalance + Convert.ToInt32(referrerPoint),
                                    MembershipId = referrer.AccountId,
                                    CurrencyId = 2
                                };

                                CreateRequest(referrerCurrency);
                            }
                        }

                        var oldMemberCurrency = context.MembershipCurrencies.FirstOrDefault(c =>
                            c.MembershipId == order.AccountId && c.CurrencyId == 2);

                        var memberCurrency = new MembershipCurrency()
                        {
                            TotalPointsAccrued = oldMemberCurrency.TotalPointsAccrued + point,
                            PointsBalance = oldMemberCurrency.PointsBalance + point,
                            MembershipId = order.AccountId,
                            CurrencyId = 2
                        };


                        //create a request to membercurreny controller to update record 
                        var result = CreateRequest(memberCurrency);

                        if (result)
                        {
                            return Ok("Successful");
                        }
                    }

                } else if (order.ApplyCode == 2)
                {
                    // if applycode = 2 => apply item condition
                    //get all order item condition
                    var orderItemCondition = context.OrderItemConditions
                        .Where(o => o.TierSequenceNumber == tier.SequenceNumber).ToList();
                    int? point = 0;
                    for (int i = 0; i < order.Details.Count; i++)
                    {
                        var detail = order.Details[i];
                        for (int j = 0; j < orderItemCondition.Count; j++)
                        {
                            var itemCondition = orderItemCondition[j];
                            if (itemCondition.ProductId == detail.ProductId)
                            {
                                if (detail.Quantity >= itemCondition.Quantity &&
                                    detail.Quantity <= itemCondition.NextQuantity)
                                {
                                    var gainPoint = itemCondition.QuantityGainPoint;
                                    if (gainPoint != null)
                                    {
                                        var ratioTier = tier.RatioPoints;
                                        gainPoint = Convert.ToInt32(gainPoint * ratioTier);
                                        point += gainPoint;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    if (referrer != null)
                    {
                        var allReferrerLevel = context.MemberReferrerLevels.Where(m => m.Status == 1).ToList();
                        double? ratio = 0;
                        for (int i = 0; i < allReferrerLevel.Count; i++)
                        {
                            var level = allReferrerLevel.ElementAt(i);
                            if (level.TierSequenceNumber == tier.SequenceNumber)
                            {
                                ratio = level.RatioReferrerPoints;
                                break;
                            }
                        }

                        var oldReferrerCurrency = context.MembershipCurrencies.FirstOrDefault(m =>
                            m.MembershipId == referrer.AccountId && m.CurrencyId == 2);

                        var referrerPoint = point * ratio;
                        if (oldReferrerCurrency != null)
                        {
                            var referrerCurrency = new MembershipCurrency()
                            {
                                TotalPointsAccrued = oldReferrerCurrency.TotalPointsAccrued +
                                                     Convert.ToInt32(referrerPoint),
                                PointsBalance = oldReferrerCurrency.PointsBalance + Convert.ToInt32(referrerPoint),
                                MembershipId = referrer.AccountId,
                                CurrencyId = 2
                            };

                            CreateRequest(referrerCurrency);
                        }
                    }


                    var oldMemberCurrency = context.MembershipCurrencies.FirstOrDefault(c =>
                        c.MembershipId == order.AccountId && c.CurrencyId == 2);

                    var memberCurrency = new MembershipCurrency()
                    {
                        TotalPointsAccrued = oldMemberCurrency.TotalPointsAccrued + point,
                        PointsBalance = oldMemberCurrency.PointsBalance + point,
                        MembershipId = order.AccountId,
                        CurrencyId = 2
                    };


                    //create a request to membercurreny controller to update record 
                    var result = CreateRequest(memberCurrency);

                    if (result)
                    {
                        return Ok("Successful");
                    }
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        private bool CreateRequest(MembershipCurrency membershipCurrency)
        {
            var result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://13.232.213.53/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJlbWFpbCI6ImxldHJhbmR1eWFuaDEwNDIwMDBAZ21haWwuY29tIiwianRpIjoiNzlkMGNmOWQtNjU1NS00NmFjLWJjMjMtOGNjZjdjZGFiMWJkIiwicm9sZSI6ImFkbWluIiwiZXhwIjoxNjU3MjcyMTI4LCJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vbG95YWx0eS1wbGF0Zm9ybS1kYmIwNSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDk2MjIvaW5kZXguaHRtbCJ9.wSyDew5olENMVyDV7ajJlpir4NrG_zoRdo65o61bfl8");
                var response = client.PutAsJsonAsync($"api/v1/member-currencies/{membershipCurrency.MembershipId}/2", membershipCurrency).Result;
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
