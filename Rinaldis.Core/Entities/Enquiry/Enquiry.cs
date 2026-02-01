using Rinaldis.Shared.Enums;

namespace Rinaldis.Core.Entities.Enquiry;

public class Enquiry : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string Message { get; set; } = null!;
    public DateTime? PreferredDateUtc { get; set; }
    public EnquiryStatus Status { get; set; }
}
