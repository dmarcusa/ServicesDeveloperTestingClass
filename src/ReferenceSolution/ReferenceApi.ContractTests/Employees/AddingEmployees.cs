﻿
using Alba;
using ReferenceAPI.Employees;

namespace ReferenceApi.ContractTests.Employees;

//System Tests
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
    //[InlineData("Boba", "Fett", "fett-boba")]
    //[InlineData("Luke", "Skywalker", "skywalker-luke")]
    //[InlineData("Joe", null, "joe")]
    [ClassData(typeof(EmployeesSampleData))]
    public async Task CanHireNewEmployee(EmployeeCreateRequest req, string expectedId)
    {
        // Given
        // A Host Per Test (Host Per Class, Collections)
        var request = new EmployeeCreateRequest
        {
            FirstName = req.FirstName,
            LastName = req.LastName
        };

        var expected = new EmployeeResponseItem
        {
            Id = expectedId,
            FirstName = req.FirstName,
            LastName = req.LastName
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
