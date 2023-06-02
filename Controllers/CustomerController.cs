using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Models;
using MyFirstProject.Repositories;

namespace MyFirstProject.Controllers
{
    public class CustomerController : Controller
    {
        //public static List<Customer> staticList = new List<Customer>();
        CustomerRepository customerRepository = new CustomerRepository();

        public IActionResult CustomerList()
        {
            IQueryable<Customer> data = customerRepository.GetAllCustomers();

            return View(data);

            #region withList
            /*
            Listmodel listmodel = new Listmodel();
            listmodel.list = staticList;
            return View(listmodel);
            */
            #endregion
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.AddCustomer(customer);
                return RedirectToAction("CustomerList");
                
                #region withList
                /*
                staticList.Add(customer);
                Listmodel listmodel = new Listmodel();
                listmodel.list = staticList;
                */
                #endregion
            }
            return View();

        }

        public IActionResult AddCustomer()
        {
            return View();
        }

    }
}
