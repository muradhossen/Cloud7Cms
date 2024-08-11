using Common.Enums;
using PaymentBanglalink.Models;

namespace Common.Models
{
    public class ServiceMapViewModel : ServiceMap
    {
        public string? ServiceId { get; set; }
        public string Duration { get; set; }
        public string? SubscriptionTypeString
        {
            get
            {
                return Enum.GetName(typeof(SubscriptionType), SubscriptionType);
            }
        } 
        public string? ChargingPurposeID { get; set; }
    }
}
