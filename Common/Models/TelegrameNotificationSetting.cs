namespace Common.Models
{
    public class TelegrameNotificationSetting
    {
        public string BotId { get; set; }
        public string ChatId { get; set; }
        public bool IsNotifySuccess { get; set; }
        public bool IsNotifyIfNoSuccess { get; set; }
        public bool IsNotifyIfAdsNotRunning { get; set; }
        public bool IsNotifyIfNoSuccessIn1H { get; set; }
        public int WaitTimeInMin { get; set; }
    }
}
