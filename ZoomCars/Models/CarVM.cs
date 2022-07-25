using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomCars.Models
{
    public partial class CarVM
    {
       

        public int Vin { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int? CarSeats { get; set; }
        public string CarType { get; set; }
        public string CarAvailable { get; set; }
        public int? CarPrice { get; set; }

        public int? FPrice { get; set; }


    }
}
