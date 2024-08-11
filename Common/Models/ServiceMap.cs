using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentBanglalink.Models
{
    public class ServiceMap
    {
        public string? ServiceId { get; set; }


        //remove these code while change the db structure
        public bool IsNotificationOn { get; set; }
        public bool IsNotificationOnForActivation { get; set; }
        public bool IsNotificationOnForDeactivation { get; set; }
        public bool IsNotificationOnForRenewal { get; set; }
        public string? NotificationUrl { get; set; }
        public int? ProviderId { get; set; }


        [StringLength(30)]
        public string? ServiceName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int DurationInDays { get; set; }

        [StringLength(100)]
        public string ServiceDescription { get; set; }

        [StringLength(4)]
        public string? ServiceCode { get; set; }


        //remove OnBehalfOf while change the db structure
        [StringLength(30)]
        public string? OnBehalfOf { get; set; }

        //remove PurchaseCategoryCode while change the db structure

        [StringLength(30)]
        public string? PurchaseCategoryCode { get; set; }

        [Required]
        public int SubscriptionType { get; set; }


        //remove Merchant while change the db structure
        [StringLength(50)]
        public string? Merchant { get; set; }
    }
}
