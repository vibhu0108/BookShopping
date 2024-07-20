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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
          _context= context;
            Category = new CategoryRepository(_context);
            CoverType =new CoverTypeRepository(_context);
            SP_CALL = new SP_CALL(_context);
            Product= new ProductRepository(_context);
            Company=new CompanyRepository(_context);
            ApplicationUser=new ApplicationUserRepository(_context);
            ShoppingCart=new ShoppingCartRepository(_context);
            OrderDetail=new OrderDetailRepository(_context);
            OrderHeader=new OrderHeaderRepository(_context);

        }
        public ICategoryRepository Category { get;private set; }

        public ICoverTypeRepository CoverType { get;private set; }

        public ISP_CALL SP_CALL { get;private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public void Save()
        {
          _context.SaveChanges();
        }
    }
}
