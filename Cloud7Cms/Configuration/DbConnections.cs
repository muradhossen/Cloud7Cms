using Common.Configuration;

namespace Cloud7Cms.Configuration
{
    public class DbConnections : BaseDbConnections
    {
        public string? PaymentGrameenphoneConnection { get; set; } = string.Empty;
        public string? PaymentBanglalinkConnection { get; set; } = string.Empty;
        public string? PaymentRobiConnection { get; set; } = string.Empty;
    }
}
