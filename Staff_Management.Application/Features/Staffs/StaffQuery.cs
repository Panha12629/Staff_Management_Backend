using MediatR;
using Staff_Management.Application.Common;
using Staff_Management.Application.Common.Interfaces;

namespace Staff_Management.Application.Features.Staffs
{
    public record StaffQuery : IRequest<ApiResponse<StaffInfo>>
    {
        public int Id { get; set; }
    }

    public class StaffQueryHandler : IRequestHandler<StaffQuery, ApiResponse<StaffInfo>>
    {
        private readonly IAppDbContext _context;

        public StaffQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<StaffInfo>> Handle(StaffQuery request, CancellationToken cancellationToken)
        {
            var staff = await _context.Staffs.FindAsync(new object[] { request.Id }, cancellationToken);

            if (staff == null)
                return new ApiResponse<StaffInfo>(false, "Staff not found", null);

            // Map  to Staffinfo
            var staffInfo = new StaffInfo
            {
                Id = request.Id,
                StaffId = staff.StaffId,
                FullName = staff.FullName,
                Birthday = staff.Birthday,
                Gender = staff.Gender
            };

            return new ApiResponse<StaffInfo>(true, "Staff retrieved successfully", staffInfo);
        }
    }
}
