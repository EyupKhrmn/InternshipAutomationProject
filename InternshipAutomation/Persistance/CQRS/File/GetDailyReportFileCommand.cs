using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using InternshipAutomation.Persistance.CQRS.Response;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetDailyReportFileCommand : IRequest<Result<InternshipDailyReportFile>>
{
    public Guid StudentId { get; set; }
    public DateTime ReportDate { get; set; }
    
    
    public class GetDailyReportFileCommandHandler : IRequestHandler<GetDailyReportFileCommand,Result<InternshipDailyReportFile>>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetDailyReportFileCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<Result<InternshipDailyReportFile>> Handle(GetDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var studentUser = await _generalRepository.Query<Domain.User.User>()
                .FirstOrDefaultAsync(_ => _.Id == request.StudentId, cancellationToken: cancellationToken);

            if (studentUser is null)
            {
                throw new Exception("Öğrenci bulunamadı.");
            }

            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ =>
                    _.StudentNameSurname == studentUser.StudentNameSurname && _.WorkingDate.Date == request.ReportDate.Date, cancellationToken: cancellationToken);

            return new Result<InternshipDailyReportFile>
            {
                Data = file,
                Success = true
            };
        }
    }
}