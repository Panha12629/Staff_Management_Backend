using MediatR;
using Microsoft.EntityFrameworkCore;
using Staff_Management.Application.Common;
using Staff_Management.Application.Common.Interfaces;

namespace Staff_Management.Application.Features.Staffs
{
    public record StaffSearchQuery(string? StaffId, int? Gender, DateTime? FromDate, DateTime? ToDate)
        : IRequest<ApiResponse<List<StaffInfo>>>;

    public class StaffSearchQueryHandler : IRequestHandler<StaffSearchQuery, ApiResponse<List<StaffInfo>>>
    {
        private readonly IAppDbContext _context;
        public StaffSearchQueryHandler(IAppDbContext context) => _context = context;

        public async Task<ApiResponse<List<StaffInfo>>> Handle(StaffSearchQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Staffs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.StaffId))
                query = query.Where(s => s.StaffId.Contains(request.StaffId));

            if (request.Gender.HasValue)
                query = query.Where(s => s.Gender == request.Gender.Value);

            if (request.FromDate.HasValue)
                query = query.Where(s => s.Birthday >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(s => s.Birthday <= request.ToDate.Value);

            var staffs = await query
                .Select(s => new StaffInfo
                {
                    Id = s.Id,
                    StaffId = s.StaffId,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Gender = s.Gender
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<StaffInfo>>(true, "Staffs retrieved successfully", staffs);
        }
    }
}
