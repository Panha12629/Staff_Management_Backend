using MediatR;
using Microsoft.AspNetCore.Mvc;
using Staff_Management.Application.Features.StaffReports;

namespace Staff_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExportController(IMediator mediator) => _mediator = mediator;

        [HttpGet("pdf")]
        public async Task<IActionResult> ExportStaffPdf()
        {
            var pdfBytes = await _mediator.Send(new GenerateStaffPdfReport());

            if (pdfBytes.Length == 0)
                return BadRequest("No data to export");

            return File(pdfBytes, "application/pdf", "staffs.pdf");
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var excelBytes = await _mediator.Send(new GenerateStaffExcelReport());

            if (excelBytes.Length == 0)
                return BadRequest("No data to export");

            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "staffs.xlsx"
            );
        }
    }
}
