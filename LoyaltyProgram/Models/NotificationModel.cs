using Newtonsoft.Json;

namespace LoyaltyProgram.Models
{
    public class NotificationModel
    {
        [JsonProperty("isAndroidDevice")]
        public bool IsAndroidDevice { get; set; }
        [JsonProperty("title")]
        public Noti Notification { get; set; }
    }

    public class GoogleNotification
    {
        public class DataPayload
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }
        }
        [JsonProperty("priority")]
        public string Priority { get; set; }
        [JsonProperty("data")]
        public DataPayload Data { get; set; }
        [JsonProperty("notification")]
        public DataPayload GooglePayload { get; set; }
    }

}
