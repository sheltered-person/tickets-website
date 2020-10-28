using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tickets.Website.Areas.Identity.Data;
using Tickets.Website.Data;
using Tickets.Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tickets.Website.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(ApplicationContext context,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            db = context;

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        private List<Ticket> LoadTicketsInfo()
        {
            var tickets = (from t in db.Tickets
                           join fr in (from f in db.Flights
                                       join r in db.Routes
                                       on f.RouteId equals r.Id
                                       select new Flight()
                                       {
                                           Id = f.Id,
                                           DepartureDate = f.DepartureDate,
                                           ArrivalDate = f.ArrivalDate,
                                           AviaCompany = f.AviaCompany,
                                           PlaneInformation = f.PlaneInformation,
                                           RouteId = f.RouteId,
                                           Route = r
                                       }) on t.FlightId equals fr.Id
                           select new Ticket()
                           {
                               Id = t.Id,
                               Place = t.Place,
                               Price = t.Price,
                               Class = t.Class,
                               IsHandLuggageIncluded = t.IsHandLuggageIncluded,
                               IsLuggageIncluded = t.IsLuggageIncluded,
                               AvailabilityStatus = t.AvailabilityStatus,
                               FlightId = t.FlightId,
                               Flight = fr
                           }
                );

            return tickets.ToList();
        }

        public IActionResult SearchTickets(SearchQuery query)
        {
            var tickets = LoadTicketsInfo();
            var queryTickets = (from t in tickets 
                                where t.AvailabilityStatus == true
                                && t.Flight.Route.DepartureAirport == query.DepartureAirport
                                && t.Flight.Route.ArrivalAirport == query.ArrivalAirport
                                && t.Flight.DepartureDate.Date == query.DepartureDate.Date
                                && t.Class == query.Class
                                select t);

            return View("Flights", queryTickets.ToList());
        }

        public IActionResult Passengers()
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = userManager.FindByNameAsync(User.Identity.Name);
                var persons = (from person in db.Persons
                               where person.UserId == user.Result.Id
                               select person).ToList();

                return View(persons);
            }

            return View("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Person person = await db.Persons.FirstOrDefaultAsync(p => p.Id == id);

                if (person != null)
                    return View(person);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            var user = userManager.FindByNameAsync(User.Identity.Name);
            person.UserId = user.Result.Id;

            db.Persons.Update(person);
            await db.SaveChangesAsync();
            return RedirectToAction("Passengers");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Person person = await db.Persons.FirstOrDefaultAsync(p => p.Id == id);
                db.Persons.Remove(person);
                db.SaveChanges();

                return RedirectToAction("Passengers");
            }

            return NotFound();
        }

        public IActionResult AddPassenger()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPassenger(Person person)
        {
            if (person.Lastname != null && 
                person.Firstname != null && 
                person.Patronymic != null &&
                person.Birthday != null &&
                person.PassportNum != null)
            {
                var user = userManager.FindByNameAsync(User.Identity.Name);
                person.UserId = user.Result.Id;

                db.Persons.Add(person);
                db.SaveChanges();

                return RedirectToAction("Passengers");
            }

            return NotFound();
        }

        private List<TicketPerson> LoadUserTickets()
        {
            var tickets = LoadTicketsInfo();
            var user = userManager.FindByNameAsync(User.Identity.Name);

            var ticketPerson = (from tp in db.TicketPersons
                                join p in db.Persons
                                on tp.PersonId equals p.Id
                                where p.UserId == user.Result.Id
                                select new TicketPerson()
                                {
                                    Id = tp.Id,
                                    TicketId = tp.TicketId,
                                    PersonId = p.Id,
                                    Person = p
                                }).ToList();

            var userTickets = (from tp in ticketPerson
                               join t in tickets
                               on tp.TicketId equals t.Id
                               select new TicketPerson()
                               {
                                   Id = tp.Id,
                                   TicketId = tp.TicketId,
                                   Ticket = t,
                                   PersonId = tp.PersonId,
                                   Person = tp.Person
                               });

            return userTickets.ToList();
        }

        public IActionResult CurrentTickets()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userTickets = LoadUserTickets();
                var currentTickets = (from tp in userTickets
                                      where tp.Ticket.Flight.DepartureDate >= DateTime.Now
                                      select tp).ToList();

                ViewData["ActivePage"] = Views.Shared.ManageTicketsPages.CurrentTickets;
                return View(currentTickets);
            }

            return NotFound();
        }


        public IActionResult TicketsArchive()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userTickets = LoadUserTickets();
                var archiveTickets = (from tp in userTickets
                                      where tp.Ticket.Flight.DepartureDate < DateTime.Now
                                      select tp).ToList();

                ViewData["ActivePage"] = Views.Shared.ManageTicketsPages.TicketsArchive;
                return View("CurrentTickets", archiveTickets);
            }

            return NotFound();
        }
    }
}
