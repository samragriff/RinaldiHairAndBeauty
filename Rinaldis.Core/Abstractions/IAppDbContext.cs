using Microsoft.EntityFrameworkCore;
using Rinaldis.Core.Entities.Enquiry;

namespace Rinaldis.Core.Abstractions;

public interface IAppDbContext
{
    DbSet<Enquiry> Enquiries { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
