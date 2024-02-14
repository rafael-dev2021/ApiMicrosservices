using ApiMicrosservicesShoppingCart.DTOs;
using ApiMicrosservicesShoppingCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiMicrosservicesShoppingCart.Endpoints;

public static class CartServiceEndPoint
{
    public static void MapCartServiceEndpoints(this WebApplication app)
    {
        app.MapPost("/api/checkout", async ([FromBody] CheckoutHeaderDTO checkoutDto, [FromServices] ICartRepository repository) =>
        {
            var cart = await repository.GetCartByUserIdAsync(checkoutDto.UserId);

            if (cart is null)
            {
                return Results.NotFound($"Cart Not found for {checkoutDto.UserId}");
            }

            checkoutDto.CartItems = cart.CartItems;
            checkoutDto.DateTime = DateTime.Now;

            return Results.Ok(checkoutDto);
        });

        app.MapPost("/api/applycoupon", async ([FromBody] CartDTO cartDto, [FromServices] ICartRepository repository) =>
        {
            var result = await repository.ApplyCouponAsync(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);

            if (!result)
            {
                return Results.NotFound($"CartHeader not found for userId = {cartDto.CartHeader.UserId}");
            }

            return Results.Ok(result);
        });

        app.MapDelete("/api/deletecoupon/{userId}", async (string userId, [FromServices] ICartRepository repository) =>
        {
            var result = await repository.DeleteCouponAsync(userId);

            if (!result)
            {
                return Results.NotFound($"Discount Coupon not found for userId = {userId}");
            }

            return Results.Ok(result);
        });

        app.MapGet("/api/getcart/{userid}", async (string userid, [FromServices] ICartRepository repository) =>
        {
            var cartDto = await repository.GetCartByUserIdAsync(userid);

            if (cartDto is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(cartDto);
        });

        app.MapPost("/api/addcart/", async ([FromBody] CartDTO cartDto, [FromServices] ICartRepository repository) =>
        {
            var cart = await repository.UpdateCartAsync(cartDto);

            if (cart is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(cart);
        });

        app.MapPut("/api/updatecart/", async ([FromBody] CartDTO cartDto, [FromServices] ICartRepository repository) =>
        {
            var cart = await repository.UpdateCartAsync(cartDto);

            if (cart is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(cart);
        });

        app.MapDelete("/api/deletecart/{id}", async (int id, [FromServices] ICartRepository repository) =>
        {
            var status = await repository.DeleteItemCartAsync(id);

            if (!status)
            {
                return Results.BadRequest();
            }

            return Results.Ok(status);
        });

    }
}
