using Model;
using Services.Dtos;

namespace Services
{
    public class CustomerService : ICustomerManager
    {
        public int RegisterCustomer(CustomerDTO customerDTO, AddressDTO addressDTO)
        {
            var address = new Address
            {
                AddressName = addressDTO.AddressName,
                ZipCode = addressDTO.ZipCode,
                District = addressDTO.District
            };

            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                PhoneNumber = customerDTO.PhoneNumber
            };

            CustomerDAO customerDAO = new CustomerDAO();

            int result = customerDAO.RegisterCustomer(customer, address);
            return result;
        }
    }
}
