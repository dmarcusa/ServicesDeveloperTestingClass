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

    public static async Task<Ok<CreateOrderResponse>> AddOrdersAsync(CancellationToken token)
    {
        var response = new CreateOrderResponse
        {
            Id = Guid.NewGuid(),
            Discount = 0,
            SubTotal = 0,
            Total = 0,
        };
        return TypedResults.Ok(response);
    }
}

public record CreateOrderRequest
{
    IList<OrderItemModel> Items { get; set; } = [];
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