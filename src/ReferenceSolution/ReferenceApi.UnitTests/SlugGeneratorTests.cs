using ReferenceAPI.Employees;

namespace ReferenceApi.UnitTests;
public class SlugGeneratorTests
{
    [Theory]
    [InlineData("Boba", "Fett", "fett-boba")]
    [InlineData("Luke", "Skywalker", "skywalker-luke")]
    [InlineData("Joe", "", "joe")]
    [InlineData("Cher", "", "cher")]
    [InlineData(" Joe", " Schmidt  ", "schmidt-joe", Skip = "Waiting")]
    [InlineData("Johnny", "Marr", "marr-johnny")]
    public async Task Avocado(string firstName, string lastName, string expected)
    {
        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGenerator();

        string slug = await slugGenerator.GenerateAsync(firstName, lastName);

        Assert.Equal(expected, slug);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData(null, null)]
    public async Task InvalidInputs(string? firstName, string? lastName)
    {
        var slugGenerator = new EmployeeSlugGenerator();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await slugGenerator.GenerateAsync(firstName!, lastName));
    }
}
