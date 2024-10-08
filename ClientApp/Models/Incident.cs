﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Incident
    {
        public int Id { get; set; }
        // Navigation properties
        public object? User { get; set; }
        public IncidentCategory? IncidentCategory { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? GPSLocation_Latitude { get; set; }
        public string? GPSLocation_Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public IncidentStatus Status { get; set; } = IncidentStatus.Pending;
    }
    
}
