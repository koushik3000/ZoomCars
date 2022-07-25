using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomCars.Models
{
    public partial class Location
    {
        public Location()
        {
            Rentals = new HashSet<Rental>();
        }

        public int LocationId { get; set; }
        public string City { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
