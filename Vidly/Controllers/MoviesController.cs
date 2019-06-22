using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
// CPRINS Para usar el include se debe agregar esta libreria
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        // Para poder consultar en la base de datos
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers

            };

            return View(viewModel);
        }

        public ActionResult Index()
        {

            // el ToList es para que se ejecute el query inmediatamente. Se llama desde al API
           // var movies = _context.Movies.Include(c => c.Genre).ToList();

            return View(/*movies*/);
            
        }

        public ActionResult Detail(int id)
        {
            // El singleOrDefault metodo es para ejecutar el query y traiga un solo registro
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        /* CPRINS: Para que solo la persona con ese rol pueda ingresar a ese controlador*/
        /*[Authorize(Roles = "members, admin")]*/
        /*[Authorize(Roles = CustomRoles.Administrator +","+ CustomRoles.User)]*/
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {

            var genres = _context.Genres.ToList();
            var viewControler = new MovieFormViewModel
            {
                Genres = genres
            };

            // CPRINS: Se le cambia el nombre de la vista a MovieForm para poder usarlo para La Forma de Crear y de Editar
            return View("MovieForm", viewControler);
        }

        [HttpPost]
        // CPRINS Para evitar falsificacion de solicitud
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);

            };

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Today;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.CantStock = movie.CantStock;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }


    }
}
 