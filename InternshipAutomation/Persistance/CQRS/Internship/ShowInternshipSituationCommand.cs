using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class ShowInternshipSituationCommand : IRequest<Result<InternshipDto>>
{
    public class ShowInternshipSituationCommandHandler : IRequestHandler<ShowInternshipSituationCommand, Result<InternshipDto>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly ILogService _logger;

        public ShowInternshipSituationCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, ILogService logger)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _logger = logger;
        }

        public async Task<Result<InternshipDto>> Handle(ShowInternshipSituationCommand request, CancellationToken cancellationToken)
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
            
            _logger.Information($"{CurrentUser.UserName} kullanıcısı staj durumunu görüntüledi. Staj durumu: {internship.Status.GetDisplayName()}");
            return new Result<InternshipDto>
            {
                Data = new InternshipDto
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