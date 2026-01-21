using MediatR;
using Staff_Management.Application.Features.Staffs;
using Staff_Management.Application.Services;

namespace Staff_Management.Application.Features.StaffReports
{
    public record GenerateStaffExcelReport() : IRequest<byte[]>;

    public class GenerateStaffExcelReportHandler : IRequestHandler<GenerateStaffExcelReport, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IExportService _exportService;

        public GenerateStaffExcelReportHandler( IMediator mediator, IExportService exportService)
        {
            _mediator = mediator;
            _exportService = exportService;
        }

        public async Task<byte[]> Handle(GenerateStaffExcelReport request, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new StaffListQuery(), cancellationToken);

            if (!result.Success || result.Data == null || !result.Data.Any())
                return Array.Empty<byte>();

            return _exportService.GenerateStaffsExcelReport(result.Data);
        }
    }
}