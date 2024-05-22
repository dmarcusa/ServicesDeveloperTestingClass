namespace ReferenceAPI.Oder;

public class CustomerLoyaltyHttpClient(HttpClient client)
{
    public async Task<decimal> GetBonusForPurchaseAsync(Guid customerId, decimal orderTotal, CancellationToken token = default)
    {
        var resource = $"customers/{customerId}/purcharse-rewards";
        //var request = new CustomerLoyaltyHttpClient(client);
        //request.GetBonusForPurchase(customerId, orderTotal);

        var request = new CustomerLoyaltyTypes.LoyaltyDiscountRequest()
        {
            OrderTotal = (double)orderTotal,
            PurchaseDate = DateTimeOffset.Now,
        };

        var response = await client.PostAsJsonAsync(resource, request, token);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<CustomerLoyaltyTypes.LoyaltyDiscountResponse>();
        if (body is not null)
            return (decimal)body.DiscountAmount;

        return 0;
    }
}
