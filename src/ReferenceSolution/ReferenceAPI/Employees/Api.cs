using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace ReferenceAPI.Employees;

[FeatureGate("Employees")]
public class Api(IValidator<EmployeeCreateRequest> validator) : ControllerBase
{
    [HttpPost("employees")]
    public async Task<ActionResult> AddEmployeeAsync([FromBody] EmployeeCreateRequest request)
    {
        var validations = validator.Validate(request);
        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }
        var response = new EmployeeResponseItem
        {
            Id = $"{request.LastName.ToLower()}-{request.FirstName.ToLower()}",
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        return StatusCode(201, response);
    }
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

public class EmployeeCreateRequestValidator : AbstractValidator<EmployeeCreateRequest>
{
    public EmployeeCreateRequestValidator()
    {
        RuleFor(o => o.FirstName).NotEmpty();
        RuleFor(o => o.FirstName).MinimumLength(3).MaximumLength(256);
        RuleFor(o => o.LastName).MinimumLength(3).MaximumLength(256);
    }
}
