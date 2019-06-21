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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /apicustomers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            // CPRINS: se mapea para que envie la estructura de customerDto con el AutoMapper
            return _context.Customers
                .Include(c => c.MembershipType)
                .ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        // GET /api/customers/1

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);


            if (customer == null)
                return NotFound();

            // Con el automaper convierte de customer a customerdto
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));

        }


        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // CPRINS: Aqui se crea el cliente, por lo tanto se recibe la estructura de CustomerDto,
            // y se convierte a la estructura de Customer para quep ueda ser almacenada en la base de datos

            var customer =  Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // CPRINS: Se aseigna el Id, ya que cuando se salva el registro se crea un Id nuevo

            customerDto.Id = customer.Id; 

            // CPRINS PAra que devuelva el codigo de creado y la ruta donde se encuentra,
            // esto como convencion de REST

            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id),customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // CPRINS Pasa todo lo que este en CustomerDto a CustomerInDb, para no tener que asignar los valores manualmente
            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
