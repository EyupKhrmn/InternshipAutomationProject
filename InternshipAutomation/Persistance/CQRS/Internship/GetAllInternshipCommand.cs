using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Domain.Entities.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GetAllInternshipCommand : IRequest<GetAllInternshipResponse>
{
    public int PeriodYear { get; set; }
    
    public class GetAllInternshipCommandHandler : IRequestHandler<GetAllInternshipCommand,GetAllInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly UserManager<Domain.User.User> _userManager;

        public GetAllInternshipCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, UserManager<Domain.User.User> userManager)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _userManager = userManager;
        }

        public async Task<GetAllInternshipResponse> Handle(GetAllInternshipCommand request, CancellationToken cancellationToken)
        {
            List<InternshipPreviewDto> internshipPreviewDto = new();
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var internships = await _generalRepository.Query<Domain.Entities.Internship.Internship>()
                .Where(_=>_.TeacherUser == currentUser.Id && _.CreatedDate.Year == request.PeriodYear)
                .ToListAsync(cancellationToken: cancellationToken);

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

            return new GetAllInternshipResponse
            {
                InternshipPreviewDtos =  internshipPreviewDto
            };
        }
    }
}

public class GetAllInternshipResponse
{
    public List<InternshipPreviewDto> InternshipPreviewDtos { get; set; }
}