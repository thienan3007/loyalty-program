using System.Net.Http.Headers;
using CorePush.Google;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Services
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
        public List<Noti> GetAllNotifications(int accountId);
        public bool UpdateNotification(int notificationId);
    }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        private readonly DeviceService deviceService;
        private readonly DatabaseContext _databaseContext;

        public NotificationService(IOptions<FcmNotificationSetting> settings, DeviceService deviceService, DatabaseContext databaseContext)
        {
            _fcmNotificationSetting = settings.Value;
            this.deviceService = deviceService;
            _databaseContext = databaseContext;
        }
        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroidDevice)
                {
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };

                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);

                    //get all device of this user
                    var device = new Device();

                    var devices = this.deviceService.GetDevices(notificationModel.Notification.AccountId);

                    List<string> list = new List<string>();
                    foreach (var item in devices)
                    {
                        list.Add(item.DeviceId);
                    }

                    string[] deviceToken = list.ToArray();

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    GoogleNotification.DataPayload dataPayload = new GoogleNotification.DataPayload();
                    dataPayload.Title = notificationModel.Notification.Title;
                    dataPayload.Body = notificationModel.Notification.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.GooglePayload = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);

                    var result = true;

                    foreach (var token in deviceToken)
                    {
                        var fcmSendResponse = await fcm.SendAsync(token, notification);

                        if (!fcmSendResponse.IsSuccess())
                        {
                            result = false;

                            response.IsSuccess = false;
                            response.Message = fcmSendResponse.Results[0].Error;
                            return response;

                        }
                    }
                    if (result)
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                }
                else
                {

                }

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        public List<Noti> GetAllNotifications(int accountId)
        {
            return _databaseContext.Notis.Where(n => n.AccountId == accountId).ToList();
        }

        public bool UpdateNotification(int notificationId)
        {
            var notification = _databaseContext.Notis.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null)
            {
                notification.IsRead = true;

                return _databaseContext.SaveChanges() > 0;
            }

            return false;

        }
    }
}
