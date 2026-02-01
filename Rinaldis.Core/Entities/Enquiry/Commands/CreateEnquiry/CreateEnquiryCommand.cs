using MediatR;
using Rinaldis.Shared.Dtos.Enquiries;

namespace Rinaldis.Core.Entities.Enquiry.Commands.CreateEnquiry;

public record CreateEnquiryCommand(
    string Name,
    string Email,
    string? Phone,
    string Message,
    DateTime? PreferredDateUtc
) : IRequest<EnquiryResponse>;
