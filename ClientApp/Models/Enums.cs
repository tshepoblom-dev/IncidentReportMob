using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public enum Gender
    {
        Male, Female
    }
    public enum RelationshipToHead
    {
        Head, Spouse, Child, Parent, Sibling, Other
    }
    public enum EmploymentStatus
    {
        Employed, Unemployed, SelfEmployed, Student, Retired, Homemaker, Other
    }
    public enum EducationLevel
    {
        None, Primary, Secondary, Tertiary, Vocational, Other
    }
    public enum FileType
    {
        IDCopy, ProofOfResidence, IncomeProof, Other
    }
    public enum SyncStatus
    {
        Pending, InProgress, Completed, Failed
    }

    public enum HousingType
    {
        RDP, Informal, Owned, Rented, Other
    }

    public enum BuildingMaterial
    {
        Brick, Wood, Metal, Thatch, Other
    }
}
