//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QTaxi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Driver
    {
        public Driver()
        {
            this.Car = new HashSet<Car>();
        }
    
        public int Id { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string user_id { get; set; }
    
        public virtual ICollection<Car> Car { get; set; }
        public virtual User AspNetUsers { get; set; }
    }
}