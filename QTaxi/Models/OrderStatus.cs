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
    
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Order> Order { get; set; }
    }
}
