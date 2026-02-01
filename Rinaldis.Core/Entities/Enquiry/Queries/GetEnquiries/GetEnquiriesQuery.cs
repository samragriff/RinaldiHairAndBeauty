using MediatR;
using Rinaldis.Shared.Dtos.Enquiries;

namespace Rinaldis.Core.Entities.Enquiry.Queries.GetEnquiries;

public record GetEnquiriesQuery : IRequest<List<EnquiryResponse>>;
