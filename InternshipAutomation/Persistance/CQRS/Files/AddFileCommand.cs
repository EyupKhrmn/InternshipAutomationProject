using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.Entities.Files;
using MediatR;

namespace InternshipAutomation.Persistance.CQRS.Files;

public class AddFileCommand : IRequest<AddFileResponse>
{
    public IFormFile FileData { get; set; }
    
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand,AddFileResponse>
    {
        private readonly IGeneralRepository _generalRepository;

        public AddFileCommandHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public async Task<AddFileResponse> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            SendingFile sendingFile = new()
            {
                FileData = request.FileData,
                SendingDate = DateTime.UtcNow,
            };
            
            _generalRepository.Add(sendingFile);
            await _generalRepository.SaveChangesAsync(cancellationToken);
            
            return new AddFileResponse
            {
                Success = true
            };
        }
    }
}

public class AddFileResponse
{
    public bool Success { get; set; }
}