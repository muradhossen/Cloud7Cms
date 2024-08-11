using Cloud7Cms.Configuration;
using Common.Configuration;
using Microsoft.Extensions.Options;

namespace Cloud7Cms.DapperConfig
{
    public class PaymentGrameenphoneDapperContext : BaseDapperContext
    {
        public PaymentGrameenphoneDapperContext(IOptions<DbConnections> options) : base(options.Value.PaymentGrameenphoneConnection)
        {

        }
    }
}
