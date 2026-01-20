using MediatR;
using Staff_Management.Application.Common;
using Staff_Management.Application.Common.Interfaces;

namespace Staff_Management.Application.Features.Staffs
{
    public record StaffDeleteCommand(int Id) : IRequest<ApiResponse<string>>;

    public class StaffDeleteCommandHandler : IRequestHandler<StaffDeleteCommand, ApiResponse<string>>
    {
        private readonly IAppDbContext _context;
        public StaffDeleteCommandHandler(IAppDbContext context) => _context = context;

        public async Task<ApiResponse<string>> Handle(StaffDeleteCommand request, CancellationToken cancellationToken)
        {
            var staff = await _context.Staffs.FindAsync(new object[] { request.Id }, cancellationToken);
            if (staff == null) return new ApiResponse<string>(false, "Staff not found", null);

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<string>(true, "Staff deleted successfully", staff.StaffId);
        }
    }
}
