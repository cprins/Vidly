using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {


        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {

            if (newRental.MovieIds.Count == 0)
                return BadRequest("No se han asignado ningun Id de películas");

            /* Se busca el cliente que se selecciona */
            var customer = _context.Customers.SingleOrDefault(
                c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("Id del cliente inválido.");

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("Uno o mas Ids de películas son invalidos");

            /* Para guardar los alquileres */
            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                    return BadRequest("La película no se encuentra disponible");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
                _context.SaveChanges();
            }

            return Created(new Uri(Request.RequestUri + "/" + newRental.CustomerId), newRental);

            /*  throw new NotImplementedException();*/
        }
        

    }
}
