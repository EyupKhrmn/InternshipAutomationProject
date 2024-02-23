using InternshipAutomation.Application.Caching;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GetAllInternCommand : IRequest<Result<List<InternDto>>>
{
    public class GetAllInternCommandHandler : IRequestHandler<GetAllInternCommand, Result<List<InternDto>>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly CacheService _cache;
        private readonly CacheObject _cacheObject;

        public GetAllInternCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, CacheService cache, CacheObject cacheObject)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _cache = cache;
            _cacheObject = cacheObject;
        }

        public async Task<Result<List<InternDto>>> Handle(GetAllInternCommand request, CancellationToken cancellationToken)
        {
            List<InternDto> internsDto = new();
            
            var currentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var cacheInterns = await _cache.GetCache("interns");

            if (cacheInterns is not null)
            {
                return new Result<List<InternDto>>
                {
                    Data = await _cacheObject.DeserializeObject<List<InternDto>>(cacheInterns),
                    Success = true
                };
            }

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
            
            if (interns is null)
            {
                return new Result<List<InternDto>>
                {
                    Message = "Stajyer bulunamadı.",
                    Success = false
                };
            }

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
            
            await _cache.SetCache("interns", await _cacheObject.SerializeObject(internsDto));

            return new Result<List<InternDto>>
            {
                Data = internsDto
            };

        }
    }
}