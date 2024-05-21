using FluentValidation;

namespace ReferenceAPI.Employees;

public class EmployeeCreateRequestValidator : AbstractValidator<EmployeeCreateRequest>
{
    public EmployeeCreateRequestValidator()
    {
        RuleFor(o => o.FirstName).NotEmpty();
        RuleFor(o => o.FirstName)
            .MinimumLength(3).WithMessage("We need a longer first name")
            .MaximumLength(256);
        RuleFor(o => o.LastName)
            .MinimumLength(3)
            .MaximumLength(256)
            .When(e => !string.IsNullOrEmpty(e.LastName));
    }
}
