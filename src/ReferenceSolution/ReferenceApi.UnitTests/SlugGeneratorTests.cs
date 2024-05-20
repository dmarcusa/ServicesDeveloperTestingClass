﻿using ReferenceAPI.Employees;

namespace ReferenceApi.UnitTests;
public class SlugGeneratorTests
{
    [Theory]
    [InlineData("Boba", "Fett", "fett-boba")]
    [InlineData("Luke", "Skywalker", "skywalker-luke")]
    [InlineData("Joe", "", "joe")]
    [InlineData("Cher", "", "cher")]
    [InlineData(" Joe", " Schmidt  ", "schmidt-joe")]
    public void Avocado(string firstName, string lastName, string expected)
    {
        //Write the code you wish you had
        var slugGenerator = new EmployeeSlugGenerator();

        string slug = slugGenerator.Generate(firstName, lastName);

        Assert.Equal(expected, slug);
    }
}
