using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ProductsAddViewModel
    { //there porperties is duplicate in ServiceMap because of current db sturcture
        [StringLength(50)]
        public string Merchant { get; set; }
        [StringLength(30)]
        public string PurchaseCategoryCode { get; set; }
        [StringLength(30)]
        public string OnBehalfOf { get; set; }
        public int? ProviderId { get; set; }
        public bool IsNotificationOn { get; set; }
        public bool IsNotificationOnForActivation { get; set; }
        public bool IsNotificationOnForDeactivation { get; set; }
        public bool IsNotificationOnForRenewal { get; set; }
        public string? NotificationUrl { get; set; }
        public string? CPId { get; set; }
        public string? PlanId { get; set; }
        public string? ShortCode { get; set; }
        public List<ServiceMapViewModel> Services { get; set; }

    }
}
