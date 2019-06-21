using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Vidly.Dtos;

namespace Vidly.Models
{
    // Para validar que el cliente tenga mas de 18 si selecciona un membresia

    public class Min18YearsIfAMember: ValidationAttribute
    {
        private ApplicationDbContext _context;

        public Min18YearsIfAMember()
        {
            _context = new ApplicationDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            // CPRINS: Se utiliza para validar si se esta haciendo la validacion desde el WEB API, o desde la forma
            var customer = new Customer();
            if (validationContext.ObjectType == typeof(Customer))
                customer = (Customer)validationContext.ObjectInstance;
            else
                customer = Mapper.Map((CustomerDto)validationContext.ObjectInstance, customer);
            //

            var membershipType = _context.MembershipType.SingleOrDefault(m => m.Id == customer.MembershipTypeId);

            if (customer.MembershipTypeId == 0 || !membershipType.RequiresAdult)
                return ValidationResult.Success;

            if (customer.Birthdate == null )
                return new ValidationResult("La fecha de cumpleaños es requerida");

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18 ? ValidationResult.Success : 
                     new ValidationResult("El cliente debe tener por lo menos 18 años para optar por una membresia"));
        }
    }
}