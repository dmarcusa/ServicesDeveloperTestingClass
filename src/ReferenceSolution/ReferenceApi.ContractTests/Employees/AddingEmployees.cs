
using Alba;
using ReferenceAPI.Employees;

namespace ReferenceApi.ContractTests.Employees;
public class AddingEmployees
{
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
    [InlineData("Boba", "Fett", "fett-boba")]
    [InlineData("Luke", "Skywalker", "skywalker-luke")]
    [InlineData("Joe", null, "joe")]
    public async Task Bannana(string firstName, string? lastName, string expectedId)
    {
        // Given
        // A Host Per Test (Host Per Class, Collections)
        var request = new EmployeeCreateRequest
        {
            FirstName = firstName,
            LastName = lastName
        };

        var expected = new EmployeeResponseItem
        {

            Id = expectedId,
            FirstName = firstName,
            LastName = lastName
        };
        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/employees");
            api.StatusCodeShouldBe(201);
        });

        var responseMessage = await response.ReadAsJsonAsync<EmployeeResponseItem>();
        Assert.NotNull(responseMessage);

        Assert.Equal(expected, responseMessage);

    }
}
