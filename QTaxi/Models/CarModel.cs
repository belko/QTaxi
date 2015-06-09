using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTaxi.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string car_mark { get; set; }
        public string car_model { get; set; }
        public string number { get; set; }

        public Nullable<int> driver_id { get; set; }
        public string driver_name { get; set; }
        public Nullable<int> status_id { get; set; }
        public string status { get; set; }

        public string title
        {
            get
            {
                return string.Format("{0} {1} {2}", car_mark, car_model, driver_name);
            }
        }
    }
}
