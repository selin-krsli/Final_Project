using MovieApp.BUSINESS.Abstract;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BUSINESS.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddToCart(string userId, int movieId, int quantity)
        {
            var cartInfo = GetCartByUserId(userId);
            if (cartInfo!=null)
            {
                var index = cartInfo.CartItems.FindIndex(s => s.MovieId == movieId);
                if(index<0)
                {
                    cartInfo.CartItems.Add(new CartItem
                    {
                        MovieId = movieId,
                        Quantity = quantity,
                        CartId = cartInfo.Id
                    });
                }
                else
                {
                    cartInfo.CartItems[index].Quantity += quantity;
                }
                _cartRepository.Update(cartInfo);
            }
        }

        public void DeleteFromCart(string? userId, int movieId)
        {
            var cartInfo = GetCartByUserId(userId);
            if(cartInfo!=null)
            {
                _cartRepository.DeleteFromCart(cartInfo.Id, movieId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
           _cartRepository.Create(new Cart { UserId = userId });
        }
    }
}
