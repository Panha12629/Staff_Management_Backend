using MediatR;
using Staff_Management.Application.Features.Staffs;
using Staff_Management.Application.Services;

namespace Staff_Management.Application.Features.StaffReports
{
    public record GenerateStaffPdfReport() : IRequest<byte[]>;

    public class GenerateStaffPdfReportHandler : IRequestHandler<GenerateStaffPdfReport, byte[]>
    {
        private readonly IMediator _mediator;
        private readonly IExportService _exportService;

        public GenerateStaffPdfReportHandler( IMediator mediator, IExportService exportService)
        {
            _mediator = mediator;
            _exportService = exportService;
        }

        public async Task<byte[]> Handle(GenerateStaffPdfReport request, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new StaffListQuery(), cancellationToken);

            if (!result.Success || result.Data == null || !result.Data.Any())
                return Array.Empty<byte>();

            return _exportService.GenerateStaffsPDFReport(result.Data);
        }
    }
}