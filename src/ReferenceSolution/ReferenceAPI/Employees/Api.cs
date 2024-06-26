﻿using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace ReferenceAPI.Employees;

[FeatureGate("Employees")]
public class Api(
    IValidator<EmployeeCreateRequest> validator,
    IGenerateSlugsForNewEmployees slugGenerator,
    IDocumentSession session,
    INotifyOfPossibleSithLords notifier) : ControllerBase
{
    [HttpPost("employees")]
    public async Task<ActionResult> AddEmployeeAsync(
        [FromBody] EmployeeCreateRequest request,
        CancellationToken token)
    {
        var validations = validator.Validate(request);
        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }

        var slug = await slugGenerator.GenerateAsync(request.FirstName, request.LastName, token);

        if (request?.LastName?.ToLowerInvariant() == "vader")
        {
            notifier.Notify(request.FirstName, request.LastName);
        }

        var entity = new EmployeeEntity
        {
            Id = Guid.NewGuid(),
            Slug = slug,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        session.Insert(entity);
        await session.SaveChangesAsync();
        var response = new EmployeeResponseItem
        {
            Id = slug,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        return StatusCode(201, response);
    }

    [HttpGet("/employees/{slug}")]
    public async Task<ActionResult> GetEmployeeBySlug(string slug)
    {
        var entity = await session.Query<EmployeeEntity>()
            .Where(e => e.Slug == slug)
            .SingleOrDefaultAsync();
        if (entity is null)
        {
            return NotFound();
        }

        var response = new EmployeeResponseItem
        {
            Id = entity.Slug,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
        };

        return Ok(response);

        //return Ok(new EmployeeResponseItem
        //{
        //    Id = "tacos",
        //    FirstName = "tacos",
        //    LastName = "burrito"
        //});
    }
}

public record EmployeeCreateRequest
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
}

public class EmployeeEntity
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public record EmployeeResponseItem
{
    public required string Id { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}
