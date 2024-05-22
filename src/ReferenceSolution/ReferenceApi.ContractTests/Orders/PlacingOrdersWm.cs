using Alba;
using ReferenceAPI.Oder;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ReferenceApi.ContractTests.Orders;
public class PlacingOrdersWm
{
    //This fails maybe missing something
    [Fact]
    public async Task StubbingHttpCalls()
    {
        var server = WireMockServer.Start();
        server.Given(
            Request.Create().WithPath("/customers/*/purchase-rewards")
            .UsingPost()
            ).RespondWith(Response.Create().WithBodyAsJson(new CustomerLoyaltyTypes.LoyaltyDiscountResponse
            {
                DiscountAmount = 420.69
            }));

        var request = new CreateOrderRequest
        {
            Items = [
                new OrderItemModel{
                    Price = 10.12M,
                    Qty = 12,
                    Sku = "beer"
                }
                ]
        };

        var host = await AlbaHost.For<Program>(config =>
        {
            config.UseSetting("loyaltyApi", server.Url);
        });
        var response = await host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/orders");
            api.StatusCodeShouldBeOk();
        }
        );

        var actualResponse = await response.ReadAsJsonAsync<CreateOrderResponse>();
        Assert.NotNull(actualResponse);
        Assert.Equal(420.69M, actualResponse.Discount);
    }
}
