using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        // Para poder consultar en la base de datos
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // el ToList es para que se ejecute el query inmediatamente.
            // El metodo Include, sirve para que el query tome los datos de la tabla asociada MembershipType
            //ya no se realiza la consulta desde aqui porque se realiza desde el api
          //  var customers = _context.Customers.Include(c=> c.MembershipType).ToList();

            return View(/*customers*/);
        }

        public ActionResult Detail(int id)
        {
            // El singleOrDefault metodo es para ejecutar el query y taiga un solo registro
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {

            var membershipTypes = _context.MembershipType.ToList();
            var viewControler = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                // CPRINS Se instancia la clase customer para que inicialice las propiedades, especificamente el Id para que no de error al salvar
                // por encontrarce vacio
                Customer = new Customer()
                
            };

            // CPRINS: Se le cambia el nombre a CustomerForm para poder usarlo para La Forma de Crear y de Editar
            return View("CustomerForm",viewControler);
        }

        [HttpPost]
        // CPRINS Para evitar falsificacion de solicitud
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // CPRINS: Se valida si el modelo es valido o si tiene algun error

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
               
            };

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            };

            return View("CustomerForm",viewModel);
        }




    }
}