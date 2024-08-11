using System.ComponentModel.DataAnnotations;

namespace Common.Responses
{
    public class SubscriptionStatusResponse
    {
        public string MSISDN { get; set; }
        public string ServiceId { get; set; }
        public long NextRenewalDate { get; set; }
    }
}
