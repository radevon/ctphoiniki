using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperAbstract
{
    public class AddressCtpData
    {
        public int Id { get; set; }

        public string ObjectName { get; set; }

        public string Addres { get; set; }

        public string BindingId { get; set; }

        public long DataId { get; set; }

       
        public DateTime? RecvDate { get; set; }

        public Double? Temp1 { get; set; }

        public Double? Temp2 { get; set; }

        public Double? Temp3 { get; set; }

        public Double? Pressure1 { get; set; }

        public Double? Pressure2 { get; set; }

        public Double? Pressure3 { get; set; }

        public Double? Pressure4 { get; set; }

        public bool? PumpStatus { get; set; }

        public int? ValveStatus { get; set; }

        public string Message { get; set; }
    }
}
