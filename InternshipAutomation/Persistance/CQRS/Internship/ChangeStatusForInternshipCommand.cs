using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Enums;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class ChangeStatusForInternshipCommand : IRequest<Result>
{
    public Guid InternshipId { get; set; }
    public InternshipStatus Status { get; set; }
    
    public class ChangeStatusForInternshipCommandHandler : IRequestHandler<ChangeStatusForInternshipCommand, Result>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILogService _logService;

        public ChangeStatusForInternshipCommandHandler(IGeneralRepository generalRepository, ILogService logService)
        {
            _generalRepository = generalRepository;
            _logService = logService;
        }

        public async Task<Result> Handle(ChangeStatusForInternshipCommand request, CancellationToken cancellationToken)
        {
            var internship = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .FirstOrDefaultAsync(_=>_.Id == request.InternshipId, cancellationToken: cancellationToken);

            if (internship is null)
            {
                _logService.Error($"{request.InternshipId} ID'li staj bulunamadı.");
                return new Result
                {
                    Message = "Staj bulunamadı.",
                    Success = false
                };
            }
            
            internship.Status = request.Status;
            
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            _logService.Information($"{internship.StudentUser} kullanıcısının staj durumu güncellendi. Güncellenen durum: {internship.Status.GetDisplayName()}. Staj: {internship.Id}. Güncelleyen: {internship.TeacherUser}");
            
            return new Result
            {
                Message = "Staj durumu başarıyla değiştirildi.",
                Success = true
            };
        }
    }
}