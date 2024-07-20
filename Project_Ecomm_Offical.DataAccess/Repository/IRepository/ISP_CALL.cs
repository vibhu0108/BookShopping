using Dapper;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ecomm_Offical.DataAccess.Repository.IRepository
{
    public interface ISP_CALL : IDisposable
    {
        void Execute (String ProcedureName,DynamicParameters param =null );

        //Find code
        T Single<T>(string ProcedureName,DynamicParameters param=null); 
        T OneRecord<T>(string ProcedureName, DynamicParameters param = null); 

        IEnumerable<T> List <T>(string ProcedureName, DynamicParameters param=null); //DisPly
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>
            (string ProcedureName, DynamicParameters param = null);

    }
}
