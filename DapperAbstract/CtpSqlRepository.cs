using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperAbstract
{
    public class CtpSqlRepository
    {
        private string ConnectionString = String.Empty;

        public CtpSqlRepository(string ConnectionString_)
        {
            this.ConnectionString = ConnectionString_;
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
        }

        public IEnumerable<CtpParameters> GetCtpParameters()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                return connection.GetList<CtpParameters>();
            }
        }

        public int? InsertCtpParameter(CtpParameters new_)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                return connection.Insert(new_);
            }
        }
    }
}
