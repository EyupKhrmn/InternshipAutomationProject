using InternshipAutomation.Domain.Entities.Internship;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace InternshipAutomation.Domain.User;

public class User : IdentityUser<Guid>
{
    #region Student

    public string? StudentNameSurname { get; set; }
    
    #endregion

    #region TeachingStaff

    public string? TeacherNameSurname { get; set; }

    #endregion

    #region Company

    public string? CompanyUserNameSurname { get; set; }

    #endregion

    #region Admin

    public string? AdminUserNameSurname { get; set; }

    #endregion

    public string? Token { get; set; }
}