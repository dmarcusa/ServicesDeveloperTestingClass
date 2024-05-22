using Microsoft.AspNetCore.Http.HttpResults;

namespace ReferenceAPI.Oder;

public static class Api
{
    public static IEndpointRouteBuilder MapOrdersApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("orders");
        //group.MapGet("/", () => "Orders Here");
        group.MapGet("/", GetOrdersAsync);
        group.MapPost("/", AddOrdersAsync);
        return app;
    }

    public static async Task<Ok> GetOrdersAsync(CancellationToken token)
    {
        return TypedResults.Ok();
    }

    public static async Task<Ok<CreateOrderResponse>> AddOrdersAsync(
        CreateOrderRequest request,
        IGetBonusesForOrders client,
        CancellationToken token)
    {
        // OBVIOUSLY NEVER TRUST ANYTHING FROM THE CLIENT - Look up these items and verify the price, etc.
        var subTotal = request.Items.Select(i => i.Qty * i.Price).Sum();
        decimal discount = await client.GetBonusForPurchaseAsync(Guid.NewGuid(), subTotal);

        var response = new CreateOrderResponse
        {
            Id = Guid.NewGuid(),
            Discount = discount,
            SubTotal = subTotal,
            Total = subTotal - discount,
        };
        return TypedResults.Ok(response);
    }
}

public record CreateOrderRequest
{
    public IList<OrderItemModel> Items { get; set; } = [];
}

public record OrderItemModel
{
    public string Sku { get; set; } = string.Empty;
    public int Qty { get; set; }
    public decimal Price { get; set; }
}

public record CreateOrderResponse
{
    public Guid Id { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}