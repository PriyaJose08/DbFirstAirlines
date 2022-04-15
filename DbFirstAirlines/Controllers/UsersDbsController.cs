#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbFirstAirlines.Models;

namespace DbFirstAirlines.Controllers
{
    public class UsersDbsController : Controller
    {
        private readonly AirlineReservationDatabaseContext _context;

        public UsersDbsController(AirlineReservationDatabaseContext context)
        {
            _context = context;
        }

        // GET: UsersDbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsersDbs.ToListAsync());
        }

        // GET: UsersDbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersDb = await _context.UsersDbs
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (usersDb == null)
            {
                return NotFound();
            }

            return View(usersDb);
        }
        // GET: UsersDbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersDb = await _context.UsersDbs.FindAsync(id);
            if (usersDb == null)
            {
                return NotFound();
            }
            return View(usersDb);
        }

        // POST: UsersDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Title,FirstName,LastName,Email,Password,DateOfBirth,PhoneNumber")] UsersDb usersDb)
        {
            if (id != usersDb.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersDbExists(usersDb.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usersDb);
        }

        // GET: UsersDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersDb = await _context.UsersDbs
              .FirstOrDefaultAsync(m => m.Userid == id);
            if (usersDb == null)
            {
                return NotFound();
            }
            return View(usersDb);
        }

        // POST: UsersDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersDb = await _context.UsersDbs.FindAsync(id);
            _context.UsersDbs.Remove(usersDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersDbExists(int id)
        {
            return _context.UsersDbs.Any(e => e.Userid == id);
        }

        // GET: UsersDbs/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Title,FirstName,LastName,Email,Password,DateOfBirth,PhoneNumber")] UsersDb usersDb)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = (from c in _context.UsersDbs
                                  where c.Email == usersDb.Email
                                  select c).FirstOrDefault();
                    if (result == null)
                    {
                        _context.Add(usersDb);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Email Id already exists.. Enter different one";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }

       

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = (from c in _context.UsersDbs
                                  where c.Email == login.Emailid &&
                                  c.Password == login.Password
                                  select c).FirstOrDefault();
                    if (result != null)
                    {
                        TempData["username"] = login.Emailid;
                        TempData.Keep();
                        return RedirectToAction("UserLogin", "UsersDbs");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Login id or Password..Pls try again";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult SearchFlight()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SearchFlight(Flight flight)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var dbdate = (from c in _context.Flights select c.DateofJourney);
                    var result = (from c in _context.Flights
                                  where c.SourceArea == flight.SourceArea &&
                                  c.DestinationArea == flight.DestinationArea
                                  select new
                                  {
                                      c.JourneyId,
                                      c.Flightregistrationid,
                                      c.SourceArea,
                                      c.DestinationArea,
                                      c.DateofJourney,
                                      c.Departuretime,
                                      c.Arrivaltime,
                                      c.Businessclassprice,
                                      c.Economyclassprice,
                                      c.Duration,
                                      c.Availablebusinessseats,
                                      c.Availableeconomyseats
                                  }).ToList();
                    List<Flight> flightslist = new List<Flight>();
                    foreach (var item in result)
                    {
                        Flight f = new Flight();
                        f.JourneyId = item.JourneyId;
                        f.Flightregistrationid = item.Flightregistrationid;
                        f.SourceArea = item.SourceArea;
                        f.DestinationArea = item.DestinationArea;
                        f.DateofJourney = item.DateofJourney;
                        f.Departuretime = item.Departuretime;
                        f.Arrivaltime = item.Arrivaltime;
                        f.Businessclassprice = item.Businessclassprice;
                        f.Economyclassprice = item.Economyclassprice;
                        f.Duration = item.Duration;
                        f.Availablebusinessseats = item.Availablebusinessseats;
                        f.Availableeconomyseats = item.Availableeconomyseats;
                        flightslist.Add(f);
                    }
                    return View(flightslist);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }


        }

        public IActionResult BookTicket(int id, int id1)
        {
            ViewBag.JourneyId = id;
            ViewBag.Flightregistrationid = id1;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookTicket(IFormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Booking booking = new Booking();
                    booking.JourneyId = int.Parse(frm["Journeyid"]);
                    booking.FlightRegistrationId = int.Parse(frm["Flightregistrationid"]);
                    booking.NoOfPassengers = int.Parse(frm["NoOfPassengers"]);
                    var newbooking = _context.Bookings.Add(booking);
                    _context.SaveChanges();
                    return RedirectToAction("BookingDetails", newbooking.Entity);

                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public IActionResult BookingDetails(Booking newbooking)
        {
            return View(newbooking);
        }
        [HttpPost]
        [ActionName("BookingDetails")]
        public async Task<IActionResult> BookingDetailsPost(Booking newbooking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = (from c in _context.Bookings
                                  where c.BookingId == newbooking.BookingId
                                  select new
                                  {
                                      c.BookingId,
                                      c.JourneyId,
                                      c.FlightRegistrationId,
                                      c.NoOfPassengers
                                  }).ToList();
                    List<Booking> bookingslist = new List<Booking>();
                    foreach (var item in result)
                    {
                        Booking f = new Booking();
                        f.BookingId = item.BookingId;
                        f.JourneyId = item.JourneyId;
                        f.FlightRegistrationId = item.FlightRegistrationId;
                        f.NoOfPassengers = item.NoOfPassengers;

                        bookingslist.Add(f);
                    }
                    return View(bookingslist);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }

        public IActionResult SeatSelection(int id, int id1)
        {

            ViewBag.BookingId = id;
            ViewBag.FlightregistrationId = id1;
            TempData["bid"] = ViewBag.BookingId;
            TempData.Keep("bid");
            TempData["fid"] = ViewBag.FlightregistrationId;
            TempData.Keep("fid");
            return View();
        }

        [HttpPost]
        public IActionResult SeatSelection(IFormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Seat seat = new Seat();
                        seat.BookingId = int.Parse(frm["BookingId"]);
                        TempData["bid"] = int.Parse(frm["BookingId"]);
                        TempData["sid"] = int.Parse(frm["SeatId"]);
                        TempData.Keep("bid");

                        seat.SeatId = int.Parse(frm["SeatId"]);
                        seat.FlightregistrationId = int.Parse(frm["FlightregistrationId"]);
                        TempData["fid"] = int.Parse(frm["FlightregistrationId"]);
                        TempData.Keep("fid");
                        _context.Seats.Add(seat);
                        _context.SaveChanges();
                        return RedirectToAction("Passengers");

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = ex.Message;
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }



        public IActionResult Passengers()
        {
            int bid = (int)TempData["bid"];
            int sid = (int)TempData["sid"];
            ViewBag.BookingId = bid;
            ViewBag.SeatId = sid;
            return View();
        }
        [HttpPost]
        public IActionResult Passengers(IFormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Passenger passenger = new Passenger();
                    passenger.BookingId = int.Parse(frm["BookingId"]);
                    passenger.FirstName = frm["FirstName"];
                    passenger.LastName = frm["LastName"];
                    passenger.SeatId = int.Parse(frm["SeatId"]);
                    passenger.ClassType = frm["ClassType"];
                    var newpassenger = _context.Passengers.Add(passenger);
                    _context.SaveChanges();
                    return RedirectToAction("PassengerDetails", newpassenger.Entity);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public IActionResult PassengerDetails(Passenger newpassenger)
        {
            return View(newpassenger);
        }
        [HttpPost]
        [ActionName("PassengerDetails")]
        public async Task<IActionResult> PassengerDetailsPost(Passenger newpassenger)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = (from c in _context.Passengers
                                  where c.PassengerId == newpassenger.PassengerId
                                  select new
                                  {
                                      c.BookingId,
                                      c.PassengerId,
                                      c.FirstName,
                                      c.LastName,
                                      c.SeatId,
                                      c.ClassType
                                  }).ToList();
                    List<Passenger> passengerlist = new List<Passenger>();
                    foreach (var item in result)
                    {
                        Passenger f = new Passenger();
                        f.BookingId = item.BookingId;
                        f.PassengerId = item.PassengerId;
                        f.FirstName = item.FirstName;
                        f.LastName = item.LastName;
                        f.SeatId = item.SeatId;
                        f.ClassType = item.ClassType;

                        passengerlist.Add(f);
                    }
                    return View(passengerlist);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }
        public IActionResult Ticket(int id,int id1,int id2,string id3,string id4)
        {
            ViewBag.PassengerId = id;
            ViewBag.BookingId=id1;
            ViewBag.SeatId=id2;
            ViewBag.FirstName=id3;
            ViewBag.LastName=id4;
            return View();
        }
        [HttpPost]
        [ActionName("Ticket")]
        public IActionResult TicketPost(IFormCollection frm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Ticket ticket = new Ticket();
                    ticket.BookingId = int.Parse(frm["BookingId"]);
                    ticket.PassengerId = int.Parse(frm["PassengerId"]);
                    ticket.FirstName = frm["FirstName"];
                    ticket.LastName = frm["LastName"];
                    ticket.SeatId = int.Parse(frm["SeatId"]);
                    _context.Tickets.Add(ticket);
                    _context.SaveChanges();
                    return RedirectToAction("TicketGenerated");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }


    public IActionResult TicketGenerated(int id)
        {
           
            return View();

        }

    }
}
