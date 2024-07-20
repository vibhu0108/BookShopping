using Project_Ecomm_Offical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ecomm_Offical.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository:IRepository<OrderDetails>
    {
        void Update (OrderDetails orderDetails);
    }
}
