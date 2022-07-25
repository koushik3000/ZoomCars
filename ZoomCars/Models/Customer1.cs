using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomCars.Models
{
    public partial class Customer1
    {
        public Customer1()
        {
            Rentals = new HashSet<Rental>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
