using FluentValidation.TestHelper;
using ReferenceAPI.Employees;

namespace ReferenceApi.UnitTests;
public class ValidingEmployeeCreateRequestsTests
{
    [Fact]
    public void RulesSetup()
    {
        var validator = new EmployeeCreateRequestValidator();

        var model = new EmployeeCreateRequest
        {
            FirstName = "xx",
            LastName = null
        };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("We need a longer first name");
    }

    [Theory]
    [MemberData(nameof(GetSampleModels))]
    public void FirstNameHasToExistAndBeValidLength(EmployeeCreateRequest model)
    {
        var validator = new EmployeeCreateRequestValidator();

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    public static IEnumerable<object[]> GetSampleModels()
    {
        yield return new object[] {
         new EmployeeCreateRequest {FirstName = "x"}
         //,second arg if it takes 2 the Theory method
        };
        yield return new object[] {
         new EmployeeCreateRequest {FirstName = new string('X', 3)}
        };
    }


}
