using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public record GetAllInternshipCommand : IRequest<Result<List<InternshipPreviewDto>>>
{
    public int PeriodYear { get; set; }
    
    public class GetAllInternshipCommandHandler : IRequestHandler<GetAllInternshipCommand,Result<List<InternshipPreviewDto>>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly ILogService _logService;

        public GetAllInternshipCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, UserManager<Domain.User.User> userManager, ILogService logService)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _userManager = userManager;
            _logService = logService;
        }

        public async Task<Result<List<InternshipPreviewDto>>> Handle(GetAllInternshipCommand request, CancellationToken cancellationToken)
        {
            List<InternshipPreviewDto> internshipPreviewDto = new();
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var internships = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .Where(_=>_.TeacherUser == currentUser.Id && _.CreatedDate.Year == request.PeriodYear)
                .ToListAsync(cancellationToken: cancellationToken);

            if (internships is null)
            {
                _logService.Error($"{request.PeriodYear} yılına ait staj bulunamadı.");
                return new Result<List<InternshipPreviewDto>>
                {
                    Data = null,
                    Message = $"{request.PeriodYear} yılına ait staj bulunamadı.",
                    Success = false
                };
            }

            string? studentUserNameSurname;
            Guid? internshipId;

            foreach (var internship in internships)
            {
                InternshipPreviewDto previewDto = new();
                
                studentUserNameSurname = await _generalRepository.Query<Domain.User.User>()
                    .Where(_ => _.Id == internship.StudentUser)
                    .Select(_ => _.StudentNameSurname)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                internshipId = internship.Id;

                previewDto.StudentNameSurname = studentUserNameSurname;
                previewDto.InternshipId = (Guid)internshipId;

                internshipPreviewDto.Add(previewDto);
            }

            return new Result<List<InternshipPreviewDto>>
            {
                Data =  internshipPreviewDto,
                Message = "Stajlar başarıyla getirildi.",
                Success = true
            };
        }
    }
}