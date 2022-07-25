using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomCars.Models
{
    public partial class Rental
    {
        public int ReservationNumber { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Vin { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int? Amount { get; set; }

        public virtual Customer1 Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual Car VinNavigation { get; set; }
    }
}
