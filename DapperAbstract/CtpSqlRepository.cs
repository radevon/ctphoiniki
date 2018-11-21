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


        #region Adreses
            public IEnumerable<Address> GetCtpAddresses()
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.GetList<Address>();
                }
            }

            public int? InsertNewAddress(Address new_)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.Insert(new_);
                }
            }

            public int RemoveAddress(int Id)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.Delete<Address>(Id);
                }
            }

        #endregion


        #region Parameters
            public IEnumerable<CtpParameters> GetCtpParameters(string BindingId,int count)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.GetListPaged<CtpParameters>(1, count, "where BindingId=@BindingId_", "RecvDate desc", new { BindingId_ = BindingId });
                }
            }

            public IEnumerable<CtpParameters> GetCtpParameters(string BindingId, DateTime date)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.GetListPaged<CtpParameters>(1, 3000, "where BindingId=@BindingId_ and RecvDate between @start and @end", "RecvDate desc", new { BindingId_ = BindingId,start=date.Date,end=date.Date.AddDays(1)});
                }
            }

            public int? InsertCtpParameter(CtpParameters new_)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.Insert(new_);
                }
            }

        #endregion
    }
}
