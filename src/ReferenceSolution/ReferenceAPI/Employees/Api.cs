using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace ReferenceAPI.Employees;

[FeatureGate("Employees")]
public class Api(IValidator<EmployeeCreateRequest> validator, IGenerateSlugsForNewEmployees slugGenerator) : ControllerBase
{
    [HttpPost("/employees")]
    public async Task<ActionResult> AddEmployeeAsync(
        [FromBody] EmployeeCreateRequest request,
        CancellationToken token)
    {
        var validations = validator.Validate(request);
        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }
        var response = new EmployeeResponseItem
        {
            Id = await slugGenerator.GenerateAsync(request.FirstName, request.LastName, token),
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        return StatusCode(201, response);
    }

    //[HttpGet("employees")]
    //public async Task<ActionResult> GetEmployeeAsync()
    //{
    //    return Ok();
    //}
}

public record EmployeeCreateRequest
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
}

public record EmployeeResponseItem
{
    public required string Id { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}
