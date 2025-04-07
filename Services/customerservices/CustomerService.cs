using Model;
using Model.DAO;
using Services.Dtos;

namespace Services.CustomerServices
{
    public class CustomerService
    {
        public int RegisterCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                PhoneNumber = customerDTO.PhoneNumber
            };

            CustomerDAO customerDAO = new CustomerDAO();

            int result = customerDAO.RegisterCustomer(customer);
            return result;
        }
    }
}
