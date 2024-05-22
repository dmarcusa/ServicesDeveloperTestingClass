
namespace ReferenceAPI.Oder;

public interface IGetBonusesForOrders
{
    Task<decimal> GetBonusForPurchaseAsync(Guid customerId, decimal orderTotal, CancellationToken token = default);
}