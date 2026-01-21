using Staff_Management.Application.Features;
using Staff_Management.Domain.Entities;

namespace Staff_Management.Application.Services
{
    public interface IExportService
    {
        byte[] GenerateStaffsExcelReport(List<StaffInfo> data);
        byte[] GenerateStaffsPDFReport(List<StaffInfo> data);
    }
}
