using InternshipAutomation.Application.Caching;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using InternshipAutomation.Persistance.CQRS.Response;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class ShowInternshipSituationCommand : IRequest<Result<InternshipDto>>
{
    public class ShowInternshipSituationCommandHandler : IRequestHandler<ShowInternshipSituationCommand, Result<InternshipDto>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IDecodeTokenService _decodeTokenService;
        private readonly ILogService _logger;
        private readonly CacheService _cache;
        private readonly CacheObject _cacheObject;

        public ShowInternshipSituationCommandHandler(IGeneralRepository generalRepository, IDecodeTokenService decodeTokenService, ILogService logger, CacheService cache, CacheObject cacheObject)
        {
            _generalRepository = generalRepository;
            _decodeTokenService = decodeTokenService;
            _logger = logger;
            _cache = cache;
            _cacheObject = cacheObject;
        }

        public async Task<Result<InternshipDto>> Handle(ShowInternshipSituationCommand request, CancellationToken cancellationToken)
        {
            var CurrentUser = await _decodeTokenService.GetUsernameFromToken();
            
            var cacheInternship = await _cache.GetCache("internship");

            if (cacheInternship is not null)
            {
                return new Result<InternshipDto>
                {
                    Data = await _cacheObject.DeserializeObject<InternshipDto>(cacheInternship),
                    Success = true
                };
            }

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

            #region Null Control

            if (internship is null)
                _logger.Error($"Staj Bulunamadı.");
            
            if (teacherUser is null)
                _logger.Error($"Öğretmen bulunamadı.");
            
            if (companyUser is null)
                _logger.Error($"Şirket bulunamadı.");

            #endregion

            var result = new InternshipDto
            {
                StudentUser = CurrentUser.StudentNameSurname,
                TeacherUser = teacherUser,
                CompanyUser = companyUser,
                InternshipAverage = Math.Round(internship.InternshipAverage, 2),
                Note = internship.Note,
                Status = internship.Status.GetDisplayName()
            };
            
            await _cache.SetCache("internship", await _cacheObject.SerializeObject(result));
            
            _logger.Information($"{CurrentUser.UserName} kullanıcısı staj durumunu görüntüledi. Staj durumu: {internship.Status.GetDisplayName()}");
            
            return new Result<InternshipDto>
            {
                Data = result,
                Success = true
            };
        }
    }
}