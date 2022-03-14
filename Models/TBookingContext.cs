using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TBooking.Models
{
    public class TBookingContext : DbContext
    {
        public TBookingContext(DbContextOptions<TBookingContext> options)
        : base(options)
        {
        }
        public DbSet<TBookingItem> TBookingItems { get; set; }
    }
}
