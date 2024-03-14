using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;

namespace EventManagementSystem
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CustomerService : ICustomerService
    {
        private string connectionString = "Data Source=LAXITASOJITRA\\SQLEXPRESS;Initial Catalog=Eventmgmt;Integrated Security=True";


        public Customer GetCustomer(int customerId)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CustomerTbl WHERE CustId = @CustId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustId", customerId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer
                    {
                        CustId = Convert.ToInt32(reader["CustId"]),
                        CustName = reader["CustName"].ToString(),
                        CustPhone = reader["CustPhone"].ToString()
                    };
                }

                reader.Close();
            }

            return customer;
        }

        public void AddCustomer(Customer customerDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO CustomerTbl (CustName, CustPhone) " +
                               "VALUES (@CustName, @CustPhone)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustName", customerDetails.CustName);
                command.Parameters.AddWithValue("@CustPhone", customerDetails.CustPhone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customerDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE CustomerTbl " +
                               "SET CustName = @CustName, CustPhone = @CustPhone " +
                               "WHERE CustId = @CustId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustId", customerDetails.CustId);
                command.Parameters.AddWithValue("@CustName", customerDetails.CustName);
                command.Parameters.AddWithValue("@CustPhone", customerDetails.CustPhone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CustomerTbl WHERE CustId = @CustId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustId", customerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CustomerTbl WHERE CustId = @CustId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustId", customerId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer
                    {
                        CustId = Convert.ToInt32(reader["CustId"]),
                        CustName = reader["CustName"].ToString(),
                        CustPhone = reader["CustPhone"].ToString()
                    };
                }

                reader.Close();
            }

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CustomerTbl";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        CustId = Convert.ToInt32(reader["CustId"]),
                        CustName = reader["CustName"].ToString(),
                        CustPhone = reader["CustPhone"].ToString()
                    };

                    customers.Add(customer);
                }

                reader.Close();
            }

            return customers;
        }
    }
}
