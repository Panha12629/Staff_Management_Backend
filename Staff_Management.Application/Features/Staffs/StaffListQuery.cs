using MediatR;
using Microsoft.EntityFrameworkCore;
using Staff_Management.Application.Common;
using Staff_Management.Application.Common.Interfaces;

namespace Staff_Management.Application.Features.Staffs
{
    public record StaffListQuery() : IRequest<ApiResponse<List<StaffInfo>>>;

    public class StaffListQueryHandler : IRequestHandler<StaffListQuery, ApiResponse<List<StaffInfo>>>
    {
        private readonly IAppDbContext _context;

        public StaffListQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<StaffInfo>>> Handle(StaffListQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _context.Staffs
                .Select(s => new StaffInfo
                {
                    Id = s.Id,
                    StaffId = s.StaffId,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Gender = s.Gender
                }).ToListAsync(cancellationToken);

            return new ApiResponse<List<StaffInfo>>(true, "Staff list retrieved Succesfully", staffs);
        }
    }
}
