using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GetInternshipCommand : IRequest<Result<InternshipDto>>
{
    public Guid InternshipId { get; set; }
 
    public class GetInternshipCommandHandler : IRequestHandler<GetInternshipCommand, Result<InternshipDto>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public GetInternshipCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result<InternshipDto>> Handle(GetInternshipCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);

            var studentUser = await _generalRepository.Query<Domain.User.User>()
                .Where(_ => _.Id == internship.StudentUser)
                .Select(_ => _.StudentNameSurname)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            var teacherUser = await _generalRepository.Query<Domain.User.User>()
                .Where(_ => _.Id == internship.TeacherUser)
                .Select(_ => _.TeacherNameSurname)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var companyUser = await _generalRepository.Query<Domain.User.User>()
                .Where(_ => _.Id == internship.CompanyUser)
                .Select(_=>_.CompanyUserNameSurname)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            #region Null Control

            if (internship is null || studentUser is null || teacherUser is null || companyUser is null)
            {
                if (internship is null)
                    _logService.Error($"{request.InternshipId} ID'li staj bulunamadı.");
                if (studentUser is null)
                    _logService.Error($"{request.InternshipId} ID'li staj için öürenci bulunamadı.");
                if (teacherUser is null)
                    _logService.Error($"{request.InternshipId} ID'li staj için öğretmen bulunamadı.");
                if (companyUser is null)
                    _logService.Error($"{request.InternshipId} ID'li staj için şirket bulunamadı.");
                
                return new Result<InternshipDto>
                {
                    Message = "Staj ile ilgili gerekli alanlar bulunamadığı için görüntülenemiyor.",
                    Success = false
                };
            }

            #endregion
            
            return new Result<InternshipDto>
            {
                Data = new InternshipDto
                {
                    CompanyUser = companyUser,
                    StudentUser = studentUser,
                    TeacherUser = teacherUser,
                    InternshipAverage = internship.InternshipAverage,
                    Note = internship.Note,
                    Status = internship.Status.GetDisplayName()
                }
            };
        }
    }
}