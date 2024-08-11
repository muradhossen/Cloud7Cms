using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RenewRetryHistory
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsNotified { get; set; }
    }
}
