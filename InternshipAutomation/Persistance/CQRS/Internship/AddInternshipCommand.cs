using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Internship;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Persistance.CQRS.Internship;

public class AddInternshipCommand : IRequest<AddInternshipResponse>
{
    public Guid UserId { get; set; }
    public Domain.Entities.Internship.Internship Internship { get; set; }
    
    public class AddInternshipCommandHandler : IRequestHandler<AddInternshipCommand,AddInternshipResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public AddInternshipCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<AddInternshipResponse> Handle(AddInternshipCommand request, CancellationToken cancellationToken)
        {
            var user = await _generalRepository
                .Query<Domain.User.User>()
                .SingleOrDefaultAsync(_ => _.Id == request.UserId, cancellationToken: cancellationToken);
            
            user.Internships.Add(request.Internship);

            return new AddInternshipResponse
            {
                Success = true
            };
        }
    }
}

public class AddInternshipResponse
{
    public bool Success { get; set; }
}