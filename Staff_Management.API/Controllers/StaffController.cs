using MediatR;
using Microsoft.AspNetCore.Mvc;
using Staff_Management.Application.Features.Staffs;

namespace Staff_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StaffController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string? staffId, [FromQuery] int? gender,
                                                [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var result = await _mediator.Send(new StaffSearchQuery(staffId, gender, fromDate, toDate));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new StaffQuery { Id = id });
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StaffUpdateCommand command)
        {
            command = command with { Id = id };
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new StaffDeleteCommand(id));
            return Ok(result);
        }
    }
}
