using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    // CPRINS: DTO (Data Object Transfer) Se utiliza como contrato para el envio de los APIS,
      // para solo enviar las propiedades que se utilizan en el modelo
    public class CustomerDto
    {
        public int Id { get; set; }
        // Para modificar la convencion en el campo nombre
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }

        // Para que solo tengan los atributos que queremos y no todos
        public MembershipTypeDto MembershipType { get; set; }

        // El signo de interrogacion para que el valor pueda ser nulo
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}