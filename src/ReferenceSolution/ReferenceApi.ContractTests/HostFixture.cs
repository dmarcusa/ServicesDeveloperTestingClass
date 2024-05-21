
using Alba;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.PostgreSql;

namespace ReferenceApi.ContractTests;
public class HostFixture : IAsyncLifetime
{
    public IAlbaHost Host = null!;

    private PostgreSqlContainer _container =
        new PostgreSqlBuilder()
        .WithImage("postgres:16.2-bullseye")
        .WithUsername("testuser")
        .WithPassword("tacosalad")
        .WithDatabase("reference")
        .Build();

    public async Task InitializeAsync()
    {
        Host = await AlbaHost.For<Program>(config =>
        {
            config.UseSetting("ConnectionString:data", _container.GetConnectionString());
            config.ConfigureTestServices(services =>
            {
                //var testFakeButtress = Substitute.For<ICheckForUniqueEmployeeStubs>();
                //testFakeButtress.CheckUniqueAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);
                //services.AddScoped<ICheckForUniqueEmployeeStubs>(sp => testFakeButtress);
            });
        });
    }
    public async Task DisposeAsync()
    {
        await _container.StopAsync();
        await Host.DisposeAsync();
    }

}
