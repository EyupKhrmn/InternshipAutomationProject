using InternshipAutomation.Domain.Entities.Internship;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace InternshipAutomation.Domain.User;

public class User : IdentityUser<Guid>
{
    #region Student
    
    public string StudentNumber { get; set; }
    public int Class { get; set; }
    
    #endregion

    #region TeachingStaff

    public string TeacherNameSurname { get; set; }

    #endregion

    public string Token { get; set; }

    public List<Internship> Internships { get; set; }
}