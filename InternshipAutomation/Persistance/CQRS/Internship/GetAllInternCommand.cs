using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GetAllInternCommand : IRequest<GetAllInternResponse>
{
    public class GetAllInternCommandHandler : IRequestHandler<GetAllInternCommand, GetAllInternResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;

        public GetAllInternCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<GetAllInternResponse> Handle(GetAllInternCommand request, CancellationToken cancellationToken)
        {
            List<InternDto> internsDto = new();
            
            var currentUser = await _decodeTokenService.GetUsernameFromToken();

            var interns = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .Join(
                    _generalRepository.Query<Domain.User.User>(),
                    internship => 1,
                    user => 1,
                    (internship, user) => new
                    {
                        Internship = internship,
                        User = user
                    })
                .Join(
                    _generalRepository.Query<InternshipApplicationFile>(),
                    joinedData => 1,
                    internshipApplicationFile => 1,
                    (joinedData, internshipApplicationFile) => new
                    {
                        JoinedData = joinedData,
                        InternshipApplicationFile = internshipApplicationFile
                    })
                .Where(joinedData => 
                    joinedData.JoinedData.Internship.CompanyUser == currentUser.Id
                    && joinedData.JoinedData.User.StudentNameSurname != null
                    && !string.IsNullOrWhiteSpace(joinedData.JoinedData.User.StudentNameSurname))
                .Select(result => new
                {
                    result.JoinedData.Internship.StudentUser,
                    result.JoinedData.User.StudentNameSurname,
                    result.JoinedData.Internship.InternshipApplicationFile!.StudentPhoneNumber,
                    result.JoinedData.User.Email,
                    result.JoinedData.Internship.InternshipApplicationFile.StudentProgram,
                    result.JoinedData.Internship.InternshipApplicationFile.StudentAGNO,
                    result.JoinedData.Internship.InternshipApplicationFile.StudentTCKN
                })
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var intern in interns)
            {
                var internDto = new InternDto
                {
                    InternId = intern.StudentUser,
                    NameSurname = intern.StudentNameSurname,
                    StundetPhoneNumber = intern.StudentPhoneNumber,
                    StudentProgram = intern.StudentProgram,
                    StudentAGNO = intern.StudentAGNO,
                    StudentTCKN = intern.StudentTCKN,
                    StundentEmail = intern.Email
                };
                
                internsDto.Add(internDto);
            }

            return new GetAllInternResponse
            {
                InternDtos = internsDto
            };

        }
    }
}

public class GetAllInternResponse
{
    public List<InternDto> InternDtos { get; set; }
}