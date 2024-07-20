using Project_Ecomm_Offfical.Data;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using Project_Ecomm_Offical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ecomm_Offical.DataAccess.Repository
{
    public class ShoppingCartRepository: Repository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public void Update(ShoppingCart shoppingCart)
        {
            _context.shoppingCarts.Update(shoppingCart);
        }
    }
}
