using Model;
using Services.Dtos;

namespace Services
{
    public class CustomerService : ICustomerManager
    {
        public int AddCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                EmailAddress = customerDTO.EmailAddress,
                PhoneNumber = customerDTO.PhoneNumber,
            };

            var address = new Address
            {
                AddressName = customerDTO.Address.AddressName,
                ZipCode = customerDTO.Address.ZipCode,
                City = customerDTO.Address.City
            };

            CustomerDAO customerDAO = new CustomerDAO();
            int result = customerDAO.AddCustomer(customer, address);
            return result;
        }

        public bool IsCustomerEmailAvailable(string email)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            return customerDAO.IsCustomerEmailAvailable(email);
        }
    }
}
