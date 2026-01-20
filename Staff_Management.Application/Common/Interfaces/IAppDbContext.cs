using Microsoft.EntityFrameworkCore;
using Staff_Management.Domain.Entities;

namespace Staff_Management.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Staff> Staffs { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
