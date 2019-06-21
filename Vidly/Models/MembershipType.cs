using System;
using System.Collections.Generic;
// Para que se tome en cuenta en la BD
using System.ComponentModel.DataAnnotations;
//
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public byte DiscountRate { get; set; }

        // Para modificar la convencion en el campo nombre
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //CPRINS: Se coloca esta propiedad para saber si la membresia requiere ser mayor de edad 
        public bool RequiresAdult { get; set; }

    }
}
