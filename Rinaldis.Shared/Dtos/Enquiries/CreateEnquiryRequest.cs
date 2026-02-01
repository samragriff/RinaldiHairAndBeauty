namespace Rinaldis.Shared.Dtos.Enquiries;

public record CreateEnquiryRequest(
    string Name,
    string Email,
    string? Phone,
    string Message,
    DateTime? PreferredDateUtc
);
