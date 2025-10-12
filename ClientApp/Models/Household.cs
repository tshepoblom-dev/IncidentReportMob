using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Household
    {
        public int Id { get; set; }
        public Object? User { get; set; }

        public string? ClientRecordId { get; set; }
        public string? HeadOfHouseholdName { get; set; }

        public string? HeadOfHouseholdIdNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int? NumberOfMembers { get; set; }
        public HousingType? HousingType { get; set; }
        public BuildingMaterial? BuildingMaterial { get; set; }
        public int? NumberOfRooms { get; set; }
        public bool? HasElectricity { get; set; }
        public bool? HasWater { get; set; }
        public bool? HasSanitation { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime DateCaptured { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }
        // Navigation Properties
        public Ward? Ward { get; set; }

        //public EmploymentInfo? EmploymentInfo { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        public ICollection<HouseholdMedia>? HouseholdMedias { get; set; }
        public ICollection<HouseholdMember>? HouseholdMembers { get; set; }
        public byte[]? RowVersion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? SyncedAt { get; set; }

    }

    public class HouseholdMedia
    {
        public int Id { get; set; }

       // [JsonIgnore]
        public Household? Household { get; set; }
      //  public AppUser? UploadedBy { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        public FileType FileType { get; set; } // e.g., ID Copy, Proof of Residence

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class HouseholdMember
    {
        public int Id { get; set; }
        public Household? Household { get; set; }

        public string? FullName { get; set; }

        public string? NationalId { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public RelationshipToHead? RelationshipToHead { get; set; }

        public EducationLevel? EducationLevel { get; set; }

        public EmploymentStatus? EmploymentStatus { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
    public class Ward
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Municipality? Municipality { get; set; }
        //public BoundaryGeo? BoundaryGeo { get; set; }   
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }

    }
    public class Municipality
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
