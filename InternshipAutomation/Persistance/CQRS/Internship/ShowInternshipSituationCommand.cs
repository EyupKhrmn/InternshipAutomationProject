using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class ShowInternshipSituationCommand : IRequest<ShowInternshipSituationResponse>
{
    public class ShowInternshipSituationCommandHandler : IRequestHandler<ShowInternshipSituationCommand, ShowInternshipSituationResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;

        public ShowInternshipSituationCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
        }

        public async Task<ShowInternshipSituationResponse> Handle(ShowInternshipSituationCommand request, CancellationToken cancellationToken)
        {
            var CurrentUser = await _decodeTokenService.GetUsernameFromToken();

            var internship = _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefault(_ => _.StudentUser == CurrentUser.Id);
            
            var teacherUser = await _generalRepository.Query<Domain.User.User>()
                .Where(_ => _.Id == internship.TeacherUser)
                .Select(_ => _.TeacherNameSurname)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var companyUser = await _generalRepository.Query<Domain.User.User>()
                .Where(_ => _.Id == internship.CompanyUser)
                .Select(_=>_.CompanyUserNameSurname)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            return new ShowInternshipSituationResponse
            {
                InternshipDto = new InternshipDto
                {
                    StudentUser = CurrentUser.StudentNameSurname,
                    TeacherUser = teacherUser,
                    CompanyUser = companyUser,
                    InternshipAverage = internship.InternshipAverage,
                    Note = internship.Note,
                    Status = internship.Status.GetDisplayName()
                }
            };
        }
    }
}

public class ShowInternshipSituationResponse
{
    public InternshipDto InternshipDto { get; set; }
}