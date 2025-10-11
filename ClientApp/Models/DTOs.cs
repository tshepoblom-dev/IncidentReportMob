using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class HouseholdDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
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
        public string? Notes { get; set; }
        // Navigation Properties
        public int? WardId { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        public ICollection<HouseholdMediaDTO>? HouseholdMedias { get; set; }
        public ICollection<HouseholdMemberDTO>? HouseholdMembers { get; set; }
        public byte[]? RowVersion { get; set; }
    }

    public class HouseholdMediaDTO
    {
        public int Id { get; set; }
        public int? HouseholdId { get; set; }
        public int? UploadedById { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public FileType? FileType { get; set; } // e.g., ID Copy, Proof of Residence
    }
    public class HouseholdMemberDTO
    {
        public int Id { get; set; }
        public int? HouseholdId { get; set; }
        public string? FullName { get; set; }
        public string? NationalId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public RelationshipToHead? RelationshipToHead { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        public string? Notes { get; set; }
    }
}
