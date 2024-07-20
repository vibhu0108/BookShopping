using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project_Ecomm_Offfical.Data;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ecomm_Offical.DataAccess.Repository
{
    public class SP_CALL : ISP_CALL
    {
        private readonly ApplicationDbContext _context;
        private static string connectionString = "";
        public SP_CALL(ApplicationDbContext context)
        {
            _context= context;
            connectionString = _context.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Execute(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                SqlCon.Execute(ProcedureName, param,commandType:CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon=new SqlConnection(connectionString))
            {
                SqlCon.Open();
                return SqlCon.Query<T>(ProcedureName,param,commandType:CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon=new SqlConnection(connectionString))
            {
                var result = SqlCon.QueryMultiple(ProcedureName,param,commandType:CommandType.StoredProcedure);
                var item1 = result.Read<T1>();
                var item2 = result.Read<T2>();
                if (item1!= null && item2 != null)
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
                return new Tuple<IEnumerable<T1>,IEnumerable<T2>>(new List<T1>(),new List<T2>());
            }
        }

        public T OneRecord<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon= new SqlConnection(connectionString))
            {
                SqlCon.Open();
                var Value = SqlCon.Query<T>(ProcedureName, param, commandType: CommandType.StoredProcedure);
                return Value.FirstOrDefault();
            }
        }

        public T Single<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon= new SqlConnection(connectionString))
            {
                SqlCon.Open();
                return SqlCon.ExecuteScalar<T>(ProcedureName, param,commandType:CommandType.StoredProcedure);
            }
        }
    }
}
