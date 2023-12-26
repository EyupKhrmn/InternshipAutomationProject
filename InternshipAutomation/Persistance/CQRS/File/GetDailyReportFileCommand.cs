using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.File;

public class GetDailyReportFileCommand : IRequest<GetDailyReportFileResponse>
{
    public Guid StudentId { get; set; }
    public DateTime ReportDate { get; set; }
    
    
    public class GetDailyReportFileCommandHandler : IRequestHandler<GetDailyReportFileCommand,GetDailyReportFileResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetDailyReportFileCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<GetDailyReportFileResponse> Handle(GetDailyReportFileCommand request, CancellationToken cancellationToken)
        {
            var studentUser = await _generalRepository.Query<Domain.User.User>()
                .FirstOrDefaultAsync(_ => _.Id == request.StudentId, cancellationToken: cancellationToken);

            var file = await _generalRepository.Query<InternshipDailyReportFile>()
                .FirstOrDefaultAsync(_ =>
                    _.StudentNameSurname == studentUser.StudentNameSurname && _.WorkingDate.Date == request.ReportDate.Date);

            return new GetDailyReportFileResponse
            {
                InternshipDailyReportFile = file,
                Success = true
            };
        }
    }
}

public class GetDailyReportFileResponse
{
    public InternshipDailyReportFile InternshipDailyReportFile { get; set; }
    public bool Success { get; set; }
}