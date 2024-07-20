using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ecomm_Offical.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        void Add(T entity);
        void Remove(T entity);
        void Remove(int id);
        void Update(T entity);
        void RemoveRange(IEnumerable<T> entities);
        T Get(int id);
        IEnumerable<T> GetAll(
             Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,//shoting
            string includeProperties = null //Category,Covertype
            );
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
    }
}
