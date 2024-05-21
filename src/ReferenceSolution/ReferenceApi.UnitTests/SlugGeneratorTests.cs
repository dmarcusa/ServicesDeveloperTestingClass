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
    public void Avocado(string firstName, string lastName, string expected)
    {
        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGenerator();

        string slug = slugGenerator.Generate(firstName, lastName);

        Assert.Equal(expected, slug);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData(null, null)]
    public void InvalidInputs(string? firstName, string? lastName)
    {
        var slugGenerator = new EmployeeSlugGenerator();

        Assert.Throws<InvalidOperationException>(() => slugGenerator.Generate(firstName!, lastName));
    }

    [Theory]
    [InlineData("Johnny", "Marr", "marr-johnny-a")]
    public void DuplicatesCreateUniqueSlugs(string firstName, string lastName, string expected)
    {
        var slugGenerator = new EmployeeSlugGenerator();

        var slug = slugGenerator.Generate(firstName, lastName);

        Assert.Equal(expected, slug);
    }
}
