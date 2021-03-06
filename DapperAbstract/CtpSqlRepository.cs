﻿using System;
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

            public Address GetCtpAddress(int Id)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.Get<Address>(Id);
                }
            }

            public Address GetCtpAddressByBindingId(string BindingId)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.GetList<Address>(new { BindingId = BindingId }).FirstOrDefault();
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

            public IEnumerable<CtpParameters> GetCtpParameters(string BindingId, DateTime dateFrom, DateTime dateTo)
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.GetListPaged<CtpParameters>(1, 3000, "where BindingId=@BindingId_ and RecvDate between @start and @end", "RecvDate desc", new { BindingId_ = BindingId, start = dateFrom, end = dateTo });
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

        #region agregate
            public IEnumerable<AddressCtpData> GetLastDataByObjects()
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    return connection.Query<AddressCtpData>(@"SELECT addr.Id, addr.ObjectName, addr.Addres, addr.BindingId, p.RecvDate, p.Temp1, p.Temp2, p.Temp3, p.Pressure1, p.Pressure2, p.Pressure3, p.Pressure4, p.PumpStatus, p.ValveStatus, p.Message
  FROM dbo.Address addr left join dbo.lastctpparameters p on addr.BindingId=p.BindingId order by addr.ObjectName");
                }
            }
        #endregion
    }
}
