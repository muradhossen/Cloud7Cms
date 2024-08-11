namespace Common.Models
{
    public class NotifiedHistory
    {
        public string VendorName { get; set; }
        public DateTime? LastNotified { get; set; }
        public DateTime? LastClickIdInserted { get; set; }
        public int LastHourClicked { get; set; }
    }
}
