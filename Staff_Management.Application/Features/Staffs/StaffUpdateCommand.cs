using MediatR;
using Staff_Management.Application.Common;
using Staff_Management.Application.Common.Interfaces;

namespace Staff_Management.Application.Features.Staffs
{
    public record StaffUpdateCommand(int Id) : IRequest<ApiResponse<StaffInfo>>
    {
        public string? StaffId { get; init; }
        public string? FullName { get; init; }
        public DateTime? Birthday { get; init; }
        public int? Gender { get; init; }
    }

    public class StaffUpdateCommandHandler : IRequestHandler<StaffUpdateCommand, ApiResponse<StaffInfo>>
    {
        private readonly IAppDbContext _context;
        public StaffUpdateCommandHandler(IAppDbContext context) => _context = context;

        public async Task<ApiResponse<StaffInfo>> Handle(StaffUpdateCommand request, CancellationToken cancellationToken)
        {
            var staff = await _context.Staffs.FindAsync(new object[] { request.Id }, cancellationToken);
            if (staff == null)
                return new ApiResponse<StaffInfo>(false, "Staff not found", null);

            staff.StaffId = request.StaffId ?? staff.StaffId;
            staff.FullName = request.FullName ?? staff.FullName;
            staff.Birthday = request.Birthday.HasValue ? request.Birthday.Value.ToUniversalTime() : staff.Birthday;
            staff.Gender = request.Gender ?? staff.Gender;

            await _context.SaveChangesAsync(cancellationToken);
           
            return new ApiResponse<StaffInfo>(true, "Staff updated successfully", new StaffInfo
            {
                Id = staff.Id,
                StaffId = staff.StaffId,
                FullName = staff.FullName,
                Birthday = staff.Birthday,
                Gender = staff.Gender
            });
        }
    }
}
