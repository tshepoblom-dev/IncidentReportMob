using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class ActivityUpdate
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string? ActivityDetails { get; set; }
        public int? IncidentId { get; set; }

    }
}
