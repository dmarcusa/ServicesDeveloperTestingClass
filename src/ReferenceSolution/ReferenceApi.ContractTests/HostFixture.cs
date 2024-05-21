
using Alba;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ReferenceAPI.Employees;

namespace ReferenceApi.ContractTests;
public class HostFixture : IAsyncLifetime
{
    public IAlbaHost Host = null!;
    public async Task InitializeAsync()
    {
        Host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureServices(services =>
            {
                var fakeUniqueChecker = Substitute.For<ICheckForUniqueEmployeeStubs>();
                //fakeUniqueChecker.CheckUniqueAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);
                services.AddScoped<ICheckForUniqueEmployeeStubs>(sp => fakeUniqueChecker);
            });
        });
    }
    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
    }
}
