using MediatR;
using Microsoft.EntityFrameworkCore;
using Rinaldis.Core.Abstractions;
using Rinaldis.Shared.Dtos.Enquiries;

namespace Rinaldis.Core.Entities.Enquiry.Queries.GetEnquiries;

public sealed class GetEnquiriesHandler(IAppDbContext db) : IRequestHandler<GetEnquiriesQuery, List<EnquiryResponse>>
{
    public async Task<List<EnquiryResponse>> Handle(GetEnquiriesQuery request, CancellationToken cancellationToken)
    {
        return await db.Enquiries
            .OrderByDescending(e => e.CreatedAtUtc)
            .Take(50)
            .Select(e => new EnquiryResponse(
                e.Id,
                e.Name,
                e.Email,
                e.Phone,
                e.Message,
                e.PreferredDateUtc,
                e.Status,
                e.CreatedAtUtc
            ))
            .ToListAsync(cancellationToken);
    }
}
