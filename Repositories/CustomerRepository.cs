using MyFirstProject.Models;

namespace MyFirstProject.Repositories
{
    public class CustomerRepository
    {
        public IQueryable<Customer> GetAllCustomers()
        {
            AdminContext context = new AdminContext();
            IQueryable<Customer> customer = context.Customers;

            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            AdminContext context = new AdminContext();
            context.Customers.Add(customer);
            context.SaveChanges();
        }
    }
}
