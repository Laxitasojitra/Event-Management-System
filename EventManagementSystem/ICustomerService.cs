using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        void AddCustomer(Customer customerDetails);

        [OperationContract]
        void UpdateCustomer(Customer customerDetails);

        [OperationContract]
        void DeleteCustomer(int customerId);

        [OperationContract]
        List<Customer> GetAllCustomers();
    }
}
