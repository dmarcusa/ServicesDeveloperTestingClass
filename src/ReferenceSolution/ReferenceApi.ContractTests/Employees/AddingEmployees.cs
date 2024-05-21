
using Alba;
using ReferenceAPI.Employees;

namespace ReferenceApi.ContractTests.Employees;

//System Tests
public class AddingEmployees : IClassFixture<HostFixture>
{
    private readonly IAlbaHost Host;

    public AddingEmployees(HostFixture fixture)
    { Host = fixture.Host; }

    //when running it create an instance for each test. even for inline data!!!!

    //[Fact]
    //public async Task Banana()
    //{
    //    // A Host per Test (Host Per class, Collection)
    //    var request = new EmployeeCreateRequest { FirstName = "Boba", LastName = "Feet" };

    //    var expected = new EmployeeResponseItem
    //    { Id = "feet-boba", FirstName = "Boba", LastName = "Feet" };
    //    var host = await AlbaHost.For<Program>();

    //    var response = await host.Scenario(api =>
    //    {
    //        //api.Post.Url("/Employees");
    //        api.Post.Json(request).ToUrl("/Employees");
    //        api.StatusCodeShouldBe(201);
    //    }
    //    );

    //    var responseMessage = await response.ReadAsJsonAsync<EmployeeResponseItem>();
    //    Assert.NotNull(responseMessage);
    //    Assert.Equal(expected, responseMessage);
    //}

    [Theory]
    //[InlineData("Boba", "Fett", "fett-boba")]
    //[InlineData("Luke", "Skywalker", "skywalker-luke")]
    //[InlineData("Joe", null, "joe")]
    [ClassData(typeof(EmployeesSampleData))]
    public async Task CanHireNewEmployee(EmployeeCreateRequest request, string expectedId)
    {
        // Given
        // A Host Per Test (Host Per Class, Collections)
        var expected = new EmployeeResponseItem
        {
            Id = expectedId,
            FirstName = request.FirstName,
            LastName = request.LastName!
        };
        //var host = await AlbaHost.For<Program>();

        var response = await Host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/employees");
            api.StatusCodeShouldBe(201);
        });

        var responseMessage = await response.ReadAsJsonAsync<EmployeeResponseItem>();
        Assert.NotNull(responseMessage);

        Assert.Equal(expected, responseMessage);

        var response2 = await Host.Scenario(api =>
        {
            api.Get.Url($"/employees/{responseMessage.Id}");
            api.StatusCodeShouldBeOk();
        });

        var responseMessage2 = await response2.ReadAsJsonAsync<EmployeeResponseItem>();
        Assert.NotNull(responseMessage2);

        Assert.Equal(responseMessage, responseMessage2);
    }

    [Fact]
    public async Task ValidationsAreChecked()
    {
        var request = new EmployeeCreateRequest { FirstName = "", LastName = "" }; // BAD Employee

        var response = await Host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/employees");
            api.StatusCodeShouldBe(400);
        });
    }
}
