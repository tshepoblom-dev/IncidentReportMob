using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class IncidentMedia
    {
        public int Id { get; set; }
        public byte[]? Content { get; set; }
        public int? IncidentId { get; set; }

    }
}
