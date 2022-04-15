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
    public class FlightsmastersController : Controller
    {
        private readonly AirlineReservationDatabaseContext _context;

        public FlightsmastersController(AirlineReservationDatabaseContext context)
        {
            _context = context;
        }

        // GET: Flightsmasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flightsmasters.ToListAsync());
        }

        // GET: Flightsmasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightsmaster = await _context.Flightsmasters
                .FirstOrDefaultAsync(m => m.FlightregistrationId == id);
            if (flightsmaster == null)
            {
                return NotFound();
            }

            return View(flightsmaster);
        }

        // GET: Flightsmasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flightsmasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightregistrationId,Flightname,Economyseats,Businessseats")] Flightsmaster flightsmaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightsmaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightsmaster);
        }

        // GET: Flightsmasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightsmaster = await _context.Flightsmasters.FindAsync(id);
            if (flightsmaster == null)
            {
                return NotFound();
            }
            return View(flightsmaster);
        }

        // POST: Flightsmasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightregistrationId,Flightname,Economyseats,Businessseats")] Flightsmaster flightsmaster)
        {
            if (id != flightsmaster.FlightregistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightsmaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightsmasterExists(flightsmaster.FlightregistrationId))
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
            return View(flightsmaster);
        }

        // GET: Flightsmasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightsmaster = await _context.Flightsmasters
                .FirstOrDefaultAsync(m => m.FlightregistrationId == id);
            if (flightsmaster == null)
            {
                return NotFound();
            }

            return View(flightsmaster);
        }

        // POST: Flightsmasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightsmaster = await _context.Flightsmasters.FindAsync(id);
            _context.Flightsmasters.Remove(flightsmaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightsmasterExists(int id)
        {
            return _context.Flightsmasters.Any(e => e.FlightregistrationId == id);
        }
    }
}
