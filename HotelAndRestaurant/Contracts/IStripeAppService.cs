using HotelAndRestaurant.Models;

namespace HotelAndRestaurant.Contracts
{
    public interface IStripeAppService
    {
        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct);
    }
}
