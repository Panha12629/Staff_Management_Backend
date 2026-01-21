using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Staff_Management.API; 

namespace StaffManagement.Tests.IntegrationTests
{
    public class StaffApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public StaffApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetStaffs_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/staff");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }
    }
}
