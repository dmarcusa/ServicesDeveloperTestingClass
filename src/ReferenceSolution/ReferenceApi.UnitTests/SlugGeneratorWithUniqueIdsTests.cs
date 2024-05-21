using NSubstitute;
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
    public async Task GeneratingSlugsForPostToEmployees(string firstName, string lastName, string expected)
    {
        var fakeUniqueChecker = Substitute.For<ICheckForUniqueEmployeeStubs>();
        fakeUniqueChecker.CheckUniqueAsync(expected, CancellationToken.None).Returns(true);

        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(fakeUniqueChecker);
        //new EmployeeSlugGeneratorWithUniqueIds(new AlwaysUniqueDummy());

        string slug = await slugGenerator.GenerateAsync(firstName, lastName);

        Assert.Equal(expected, slug);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData(null, null)]
    public async Task InvalidInputs(string? firstName, string? lastName)
    {
        var fakeUniqueChecker = Substitute.For<ICheckForUniqueEmployeeStubs>();
        fakeUniqueChecker.CheckUniqueAsync(Arg.Any<string>(), CancellationToken.None).Returns(true);

        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(fakeUniqueChecker);
        //var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(new AlwaysUniqueDummy());

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await slugGenerator.GenerateAsync(firstName!, lastName));
    }

    [Theory]
    [InlineData("Johnny", "Marr", "marr-johnny-a")]
    [InlineData("Jeff", "Gonzales", "gonzales-jeff-a")]
    [InlineData("Jeff", "Gonzales", "gonzales-jeff-z")]
    public async Task DuplicatesCreateUniqueSlugs(string firstName, string lastName, string expected)
    {
        var fakeUniqueChecker = Substitute.For<ICheckForUniqueEmployeeStubs>();
        fakeUniqueChecker.CheckUniqueAsync(expected, CancellationToken.None).Returns(true);

        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(fakeUniqueChecker);
        //var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(new NeverUniqueDummy());

        var slug = await slugGenerator.GenerateAsync(firstName, lastName);

        Assert.Equal(expected, slug);
    }

    [Fact(Skip = "replace")]
    public async Task RunOutOfAttempts()
    {
        var fakeUniqueChecker = Substitute.For<ICheckForUniqueEmployeeStubs>();

        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(fakeUniqueChecker);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => slugGenerator.GenerateAsync("Dog", "Man"));
    }

    [Fact]
    public async Task AUniqueIdIsAdded()
    {
        var fakeUniqueChecker = Substitute.For<ICheckForUniqueEmployeeStubs>();
        //fakeUniqueChecker.CheckUniqueAsync(Arg.Any<string>(), CancellationToken.None).Returns(false);
        var slugGenerator = new EmployeeSlugGeneratorWithUniqueIds(fakeUniqueChecker);
        var slug = await slugGenerator.GenerateAsync("Dog", "Man");

        Assert.StartsWith("man-dog", slug);
        Assert.True(slug.Length == 7 + 22); // Is the slug with 22 random thingies on the end.

    }
}

//public class AlwaysUniqueDummy : ICheckForUniqueEmployeeStubs
//{
//    public Task<bool> CheckUniqueAsync(string slug, CancellationToken token)
//    {
//        return Task.FromResult(true);
//    }
//}

//public class NeverUniqueDummy : ICheckForUniqueEmployeeStubs
//{
//    public Task<bool> CheckUniqueAsync(string slug, CancellationToken token)
//    {
//        return Task.FromResult(false);
//    }
//}
