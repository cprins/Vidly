using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        // Cantidad de peliculas en el stock
        [Required]
        [Range(1, 20)]
        public int CantStock { get; set; }

        // CPRINS:Para asociar la tabla de generos con la de peliculas
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }

    }
}