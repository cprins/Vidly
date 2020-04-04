using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Fecha de Lanzamiento")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        // Cantidad de peliculas en el stock
        [Required]
        [Display(Name = "Cantidad en Stock")]
        [Range(1,20)]
        public int CantStock { get; set; }

        // para cargar todo el objeto Genre
        public Genre Genre { get; set; }
        // CPRINS:Para asociar la tabla de generos con la de peliculas
        [Display(Name = "Genero")]
        public byte GenreId { get; set; }

        public byte NumberAvailable { get; set; }

    }
}
