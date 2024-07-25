using Microsoft.EntityFrameworkCore;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart, MovieContext>, ICartRepository
    {
        public void DeleteFromCart(int cartId, int movieId)
        {
            using (var context = new MovieContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0 and MovieId=@p1";
                context.Database.ExecuteSqlRaw(cmd,cartId,movieId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using(var context = new MovieContext())
            {
                return context.Carts
                              .Include(s => s.CartItems)
                              .ThenInclude(s => s.Movie)
                              .FirstOrDefault(l => l.UserId == userId);
                       
            }
        }
        public override void Update(Cart entity)
        {
            using (var context = new MovieContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
