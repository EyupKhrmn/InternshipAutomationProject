namespace InternshipAutomation.Domain.Entities.Files;

public class StateContributionFile
{
    #region Student

    public string StudentNameSurname { get; set; }
    public string StudentNumber { get; set; }
    public string StudentTCKN { get; set; }
    public string StudentPhoneNumber { get; set; }
    public string StudentProgram { get; set; }
    public DateTime StudentBirthDate { get; set; }

    #endregion

    #region Company

    public string CompanyName { get; set; }
    public string CompanyTCKN { get; set; }
    public string CompanyVKN { get; set; }
    public int EmployeeCount { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountHolder { get; set; }
    public string CompnayIBAN { get; set; }

    #endregion
}