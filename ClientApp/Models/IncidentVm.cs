using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class IncidentVm
    {
        public int? Id { get; set; }
        // Navigation properties
        public string? UserId { get; set; }
        public int? IncidentCategoryId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? GPSLocation_Latitude { get; set; }
        public string? GPSLocation_Longitude { get; set; }
        public List<byte[]>? IncidentFilesBytes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public IncidentStatus Status { get; set; } = IncidentStatus.Pending;
    }
}
