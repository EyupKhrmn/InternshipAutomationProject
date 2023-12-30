using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class GetInternshipCommand : IRequest<GetInternshipresponse>
{
    public Guid InternshipId { get; set; }
 
    public class GetInternshipCommandHandler : IRequestHandler<GetInternshipCommand, GetInternshipresponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public GetInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<GetInternshipresponse> Handle(GetInternshipCommand request, CancellationToken cancellationToken)
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
            
            var internshipPeriod = await _generalRepository.Query<Domain.Entities.Internship.InternshipPeriod>()
                .Where(_ => _.Id == internship.InternshipPeriod.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            
            return new GetInternshipresponse
            {
                InternshipDto = new InternshipDto
                {
                    CompanyUser = companyUser,
                    StudentUser = studentUser,
                    TeacherUser = teacherUser,
                    InternshipAverage = internship.InternshipAverage,
                    InternshipPeriod = internshipPeriod.StartedDate,
                    Note = internship.Note,
                    Status = internship.Status
                }
            };
        }
    }
}

public class GetInternshipresponse
{
    public InternshipDto InternshipDto { get; set; }
}