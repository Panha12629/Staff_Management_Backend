using MediatR;
using Staff_Management.Application.Common;
using Staff_Management.Application.Common.Interfaces;
using Staff_Management.Domain.Entities;

namespace Staff_Management.Application.Features.Staffs
{
    public record StaffCreateCommand : IRequest<ApiResponse<StaffInfo>>
    {
        public string? StaffId { get; set; }
        public string? FullName { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; } 
    }

    public class StaffCreateCommandHandler : IRequestHandler<StaffCreateCommand, ApiResponse<StaffInfo>>
    {
        private readonly IAppDbContext _context;

        public StaffCreateCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<StaffInfo>> Handle(StaffCreateCommand request, CancellationToken cancellationToken)
        {
            var staff = new Staff
            {
                StaffId = request.StaffId,
                FullName = request.FullName,
                Birthday = request.Birthday.ToUniversalTime(),
                Gender = request.Gender
            };

            _context.Staffs.Add(staff);

           await _context.SaveChangesAsync(cancellationToken);

            var staffInfo = new StaffInfo
            {
                Id = staff.Id,
                StaffId = staff.StaffId,
                FullName = staff.FullName,
                Birthday = staff.Birthday,
                Gender = staff.Gender
            };

            return new ApiResponse<StaffInfo>(true, "Staff created successfully", staffInfo);
        }
    }
}
