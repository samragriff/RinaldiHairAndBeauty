using MediatR;
using Rinaldis.Core.Abstractions;
using Rinaldis.Shared.Dtos.Enquiries;
using Rinaldis.Shared.Enums;

namespace Rinaldis.Core.Entities.Enquiry.Commands.CreateEnquiry;

public sealed class CreateEnquiryHandler(IAppDbContext db) : IRequestHandler<CreateEnquiryCommand, EnquiryResponse>
{
    public async Task<EnquiryResponse> Handle(CreateEnquiryCommand request, CancellationToken cancellationToken)
    {
        var enquiry = new Enquiry
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Message = request.Message,
            PreferredDateUtc = request.PreferredDateUtc,
            Status = EnquiryStatus.New
        };
        db.Enquiries.Add(enquiry);
        await db.SaveChangesAsync(cancellationToken);
        return new EnquiryResponse(
            enquiry.Id,
            enquiry.Name,
            enquiry.Email,
            enquiry.Phone,
            enquiry.Message,
            enquiry.PreferredDateUtc,
            enquiry.Status,
            enquiry.CreatedAtUtc
        );
    }
}
