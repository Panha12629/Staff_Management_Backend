using Moq;
using Staff_Management.Application.Common.Interfaces;
using Staff_Management.Application.Features.Staffs;
using Xunit;

namespace StaffManagement.Tests.UnitTests
{
    public class StaffCreateCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateStaff()
        {
            var mockContext = new Mock<IAppDbContext>();
            var handler = new StaffCreateCommandHandler(mockContext.Object);

            var command = new StaffCreateCommand
            {
                StaffId = "A001",
                FullName = "Staff Test",
                Birthday = new DateTime(2005, 1, 1),
                Gender = 1
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
