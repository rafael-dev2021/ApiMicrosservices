using ApiMicrosservicesShoppingCart.DTOs;

namespace ApiMicrosservicesShoppingCart.Repositories;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserIdAsync(string userId);
    Task<CartDTO> UpdateCartAsync(CartDTO cart);
    Task<bool> CleanCartAsync(string userId);
    Task<bool> DeleteItemCartAsync(int cartItemId);

    Task<bool> ApplyCouponAsync(string userId, string couponCode);
    Task<bool> DeleteCouponAsync(string userId);
}
