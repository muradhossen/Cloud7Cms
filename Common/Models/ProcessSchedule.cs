using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ProcessSchedule
    {
        public int Id { get; set; }
        public int PriorityOrder { get; set; }
        public string Type { get; set; }
        public TimeSpan Schedule { get; set; }
    }
}
