using Rinaldis.Shared.Enums;

namespace Rinaldis.Shared.Dtos.Enquiries;

public record EnquiryResponse(
    int Id,
    string Name,
    string Email,
    string? Phone,
    string Message,
    DateTime? PreferredDateUtc,
    EnquiryStatus Status,
    DateTime CreatedAtUtc
);
