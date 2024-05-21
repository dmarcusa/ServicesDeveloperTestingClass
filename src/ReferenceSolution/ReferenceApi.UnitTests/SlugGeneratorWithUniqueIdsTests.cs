using ReferenceAPI.Employees;

namespace ReferenceApi.UnitTests;
public class SlugGeneratorWithUniqueIdsTests
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
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(new AlwaysUniqueDummy());

        string slug = await slugGenerator.GenerateAsync(firstName, lastName);

        Assert.Equal(expected, slug);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData(null, null)]
    public async Task InvalidInputs(string? firstName, string? lastName)
    {
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(new AlwaysUniqueDummy());

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await slugGenerator.GenerateAsync(firstName!, lastName));
    }

    [Theory]
    [InlineData("Johnny", "Marr", "marr-johnny-a")]
    [InlineData("Jeff", "Gonzales", "gonzaless-jeff-a")]
    public async Task DuplicatesCreateUniqueSlugs(string firstName, string lastName, string expected)
    {
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(new NeverUniqueDummy());

        var slug = await slugGenerator.GenerateAsync(firstName, lastName);

        Assert.Equal(expected, slug);
    }
}

public class AlwaysUniqueDummy : ICheckForUniqueEmployeeStubs
{
    public Task<bool> CheckUniqueAsync(string slug, CancellationToken token)
    {
        return Task.FromResult(true);
    }
}

public class NeverUniqueDummy : ICheckForUniqueEmployeeStubs
{
    public Task<bool> CheckUniqueAsync(string slug, CancellationToken token)
    {
        return Task.FromResult(false);
    }
}
