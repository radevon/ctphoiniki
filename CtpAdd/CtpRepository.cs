using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace CtpAdd
{
    /// <summary>
    /// Репозиторий для вставки данных по ЦТП Хойники
    /// </summary>
    public class CtpRepository
    {
         private string ConnectionString = String.Empty;

        public CtpRepository(string ConnectionString_)
        {
            this.ConnectionString = ConnectionString_;
            
        }
        /// <summary>
        /// Метод добавляет строку с данными по одному объекту с заданным BindingId
        /// </summary>
        /// <param name="BindingId">Идентификатор объекта</param>
        /// <param name="RecvDate">Дата время получения инф</param>
        /// <param name="Temp1">Температура подачи</param>
        /// <param name="Temp2">Температура обратки</param>
        /// <param name="Temp3">Температура окр среды</param>
        /// <param name="Pressure1">Давление 1</param>
        /// <param name="Pressure2">Давление 2</param>
        /// <param name="Pressure3">Давление 3</param>
        /// <param name="Pressure4">Давление 4</param>
        /// <param name="PumpStatus">Состояние насоса true - вкл, false - выкл</param>
        /// <param name="ValveStatus">Состояние клапана: -1 - закр, 0 - покой, 1 - откр</param>
        /// <param name="Message">какое то там сообщение</param>
        /// <returns>кол-во вставленных строк - обычно 1 или исключение Exception (при неполадках)</returns>
        public int InsertCtpData(string BindingId, DateTime RecvDate, Double? Temp1, Double? Temp2, Double? Temp3, Double? Pressure1, Double? Pressure2, Double? Pressure3, Double? Pressure4, bool? PumpStatus, int? ValveStatus, string Message)
        {
            int res = 0;
            using (IDbConnection con = new SqlConnection(this.ConnectionString))
            {
                res = con.Execute(@"insert into ctpparameters (bibdingid,recvdate,temp1,temp2,temp3,pressure1,pressure2,pressure3,pressure4,pumpstatus,valvestatus,message) values(@bibdingid_,@recvdate_,@temp1_,@temp2_,@temp3_,@pressure1_,@pressure2_,@pressure3_,@pressure4_,@pumpstatus_,@valvestatus_,@message_);", new { bibdingid_=BindingId, recvdate_=RecvDate, temp1_=Temp1, temp2_=Temp2, temp3_=Temp3, pressure1_=Pressure1, pressure2_=Pressure2, pressure3_=Pressure3, pressure4_=Pressure4, pumpstatus_=PumpStatus, valvestatus_=ValveStatus, message_=Message });
            }
            return res;
        }
    }
}
