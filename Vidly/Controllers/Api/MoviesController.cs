using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public  IHttpActionResult GetMovies(string query = null)
        {
            // CPRINS: se mapea para que envie la estructura de movieDto con el AutoMapper. No olvidar crear el AutoMapping para Genre o la tabla asociada
            // return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);

            var moviesQuery = _context.Movies.Include(c => c.Genre);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(c => c.Name.Contains(query)).Where(c => c.NumberAvailable >0);
                
            var movieDtos = moviesQuery
            .ToList().Select(Mapper.Map<Movie, MovieDto>);


            return Ok(movieDtos);
        }

        // GET /api/movies/1

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);


            if (movie == null)
                return NotFound();

            // Con el automaper convierte de movie a moviedto
            return Ok(Mapper.Map<Movie, MovieDto>(movie));

        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // CPRINS: Aqui se crea el cliente, por lo tanto se recibe la estructura de MovieDto,
            // y se convierte a la estructura de Movie para quep ueda ser almacenada en la base de datos

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            // CPRINS: Se asigna el Id, ya que cuando se salva el registro se crea un Id nuevo

            movieDto.Id = movie.Id;

            // CPRINS PAra que devuelva el codigo de creado y la ruta donde se encuentra,
            // esto como convencion de REST

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        // PUT /api/movie/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // CPRINS Pasa todo lo que este en MovieDto a MovieInDb, para no tener que asignar los valores manualmente
            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

        }

        // DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
