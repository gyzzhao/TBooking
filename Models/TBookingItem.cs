using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBooking.Models
{
    public class TBookingItem
    {
        public long Id { get; set; }
        public DateTime BookingDate { get; set; }
        private DateTime BookingTime { get; set; }

        public string PickUpPoint { get; set; }
        public string Destination { get; set; }
        public decimal Current_Location_Latitude { get; set; }
        public decimal Current_Location_Longitude { get; set; }
    }
}
