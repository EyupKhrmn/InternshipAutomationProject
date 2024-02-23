﻿namespace InternshipAutomation.Security.Token;

public class IdentityData
{
    public const string UserRankClaimsName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    
    public const string AdminUserRankName = "Admin";
    public const string AdminUserPolicyName = "admin";

    public const string TeacherUserRankName = "Öğretmen";
    public const string TeacherUserPolicyName = "teacher";

    public const string StudentUserRankName = "Öğrenci";
    public const string StudentUserPolicyName = "student";
    
    public const string CompanyUserRankName = "Şirket";
    public const string CompanyUserPolicyName = "company";
    
    public const string AdminAndTeacherUserPolicyName = "admin,teacher";
    public const string AdminAndTeacherUserRankName = "Admin,Öğretmen";
    
    public const string AdminAndStudentUserPolicyName = "admin,student";
    public const string AdminAndStudentUserRankName = "Admin,Öğrenci";
    
    public const string AdminAndCompanyUserPolicyName = "admin,company";
    public const string AdminAndCompanyUserRankName = "Admin,Şirket";
}