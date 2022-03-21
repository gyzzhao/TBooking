using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TBooking.Models;
using System.Text.Json;
using System.Net.Http;


namespace TBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TBookingItemsController : ControllerBase
    {
        private readonly TBookingContext _context;
        private readonly IHttpClientFactory _clientFactory;
        //private readonly ILogger<TBookingController> _logger;

        public TBookingItemsController(TBookingContext context, IHttpClientFactory clientFactory
            //,ILogger<TBookingController> logger
            )
        {
            _context = context;
            _clientFactory = clientFactory;
            //_logger = logger;
        }

        public TBookingItemsController(TBookingContext context)
        {
            _context = context;
        }gi

        // GET: api/TBookingItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TBookingItem>>> GetTBookingItems()
        {
            return await _context.TBookingItems.ToListAsync();
        }

        // GET: api/TBookingItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TBookingItem>> GetTBookingItem(long id)
        {
            var tBookingItem = await _context.TBookingItems.FindAsync(id);

            if (tBookingItem == null)
            {
                return NotFound();
            }

            return tBookingItem;
        }

        // PUT: api/TBookingItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTBookingItem(long id, TBookingItem tBookingItem)
        {
            if (id != tBookingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(tBookingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TBookingItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TBookingItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TBookingItem>> PostTBookingItem(TBookingItem tBookingItem)
        {
            _context.TBookingItems.Add(tBookingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTBookingItem", new { id = tBookingItem.Id }, tBookingItem);
        }

        // DELETE: api/TBookingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTBookingItem(long id)
        {
            var tBookingItem = await _context.TBookingItems.FindAsync(id);
            if (tBookingItem == null)
            {
                return NotFound();
            }

            _context.TBookingItems.Remove(tBookingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TBookingItemExists(long id)
        {
            return _context.TBookingItems.Any(e => e.Id == id);
        }

        //This is a POST request to collect info from Postman (front end)
        [HttpPost("GetCurrentLocation")]
        public async Task<Location> GetCurrentLocation()   //Location is the name of the public class name in the Currecy.cs file
        {
            HttpClient client = _clientFactory.CreateClient();
            Location location = new Location();
            string uri = "https://freegeoip.app/json/";

            //make a HttpGet to call the above public API uri
            var response = client.GetAsync(uri).Result;

            var content = await response.Content.ReadAsStringAsync();

            //to convert json to object
            Location locat = JsonSerializer.Deserialize<Location>(content);
            location.latitude = locat.latitude;
            location.longitude = locat.longitude;
            //to check if you get the correct exchange rate
            //_logger.LogInformation("Exchange rate is: " + rate.sgd.ToString());

            //to return to Postman the total sgd amount after exchange rate
            return location;

        }

    }
}
