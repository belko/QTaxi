using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaxi.Models
{
    public class OrderModel
    {
        public int id { get; set; }
        public string address_from { get; set; }
        public string address_to { get; set; }
        public Nullable<int> taxi_id { get; set; }
        public Nullable<int> car_id { get; set; }
        public Nullable<int> client_id { get; set; }
        public string client_phone { get; set; }
        public string client_name { get; set; }
        public Nullable<int> status_id { get; set; }

        public string taxi_name { get;set;}
        public string car_name { get; set; }

        public string status { get; set; }

        
    }
}
