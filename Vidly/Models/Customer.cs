using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        // Para modificar la convencion en el campo nombre
        [Required]
        [StringLength(255)]
        //
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        // para cargar todo el objeto MembershipType
        public MembershipType MembershipType { get; set; }

        [Display(Name="Tipo de Membresía")]
        public byte MembershipTypeId { get; set; }

        // El signo de interrogacion para que el valor pueda ser nulo
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

    }
}