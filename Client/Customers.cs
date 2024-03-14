using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.ServiceReference1;
using Client.ServiceReference2;
using Client.ServiceReference3;
using Client.ServiceReference4;

namespace Client
{
    public partial class Customers : Form
    {


        private CustomerServiceClient _customerServiceClient;
        private ServiceReference1.Customer[] customerArray;
        public Customers()
        {
            InitializeComponent();
            _customerServiceClient = new CustomerServiceClient();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            DisplayCustomers();
        }

        private void DisplayCustomers()
        {

            customerArray = _customerServiceClient.GetAllCustomers();
            List<ServiceReference1.Customer> customers = customerArray.ToList();
            dataGridViewCustomers.AutoGenerateColumns = false;


            dataGridViewCustomers.Columns.Clear();
            // Manually add columns to the DataGridView
            dataGridViewCustomers.Columns.Add("CustId", "Customer ID");
            dataGridViewCustomers.Columns.Add("CustName", "Customer Name");
            dataGridViewCustomers.Columns.Add("CustPhone", "Customer Phone Number");

            foreach (var customer in customers)
            {
                dataGridViewCustomers.Rows.Add(
                customer.CustId,
                customer.CustName,
                customer.CustPhone
                );
            }
        }
        private void dataGridViewCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox3.Text;
                string phone = textBox2.Text;

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone))
                {
                    ServiceReference1.Customer newCustomer = new ServiceReference1.Customer
                    {
                        CustName = name,
                        CustPhone = phone
                    };

                    _customerServiceClient.AddCustomer(newCustomer);
                    DisplayCustomers();
                    MessageBox.Show("Customer added successfully!");
                }
                else
                {
                    MessageBox.Show("Please enter both name and phone number.");
                }
            }

            catch (FormatException ex)
            {
                // Handle the format exception (e.g., display an error message)
                MessageBox.Show("Invalid input format: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationObjectFaultedException ex)
            {
                // Handle the communication fault exception
                MessageBox.Show("CommunicationObjectFaultedException: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Attempt to recreate the service client
                _customerServiceClient = new CustomerServiceClient();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //its giving error.........
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Check if a customer is selected in the DataGridView
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                // Get the selected customer ID from the DataGridView
                var selectedRow = dataGridViewCustomers.SelectedRows[0];
                var selectedCustomerId = (int)selectedRow.Cells["CustId"].Value;

                // Find the selected customer in the customer array
                var selectedCustomer = customerArray.FirstOrDefault(customer => customer.CustId == selectedCustomerId);

                // If the selected customer is found
                if (selectedCustomer != null)
                {
                    // Update the customer object with the new data from the text boxes
                    selectedCustomer.CustName = textBox3.Text;
                    selectedCustomer.CustPhone = textBox2.Text;

                    try
                    {
                        // Update the customer data using the service client
                        _customerServiceClient.UpdateCustomer(selectedCustomer);

                        // Show a success message
                        MessageBox.Show("Customer updated successfully!");

                        // Refresh the display of customers
                        DisplayCustomers();
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur during the update process
                        MessageBox.Show($"An error occurred while updating customer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // If the selected customer is not found in the customer array
                    MessageBox.Show("Selected customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // If no customer is selected in the DataGridView
                MessageBox.Show("Please select a customer to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void delete_click(object sender, EventArgs e)
        {

            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {


                var selectedRow = dataGridViewCustomers.SelectedRows[0];
                var selectedCustomerId = (int)selectedRow.Cells["CustId"].Value;
                var selectedCustomer = customerArray.FirstOrDefault(customer => customer.CustId == selectedCustomerId);

                if (selectedCustomer != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _customerServiceClient.DeleteCustomer(selectedCustomer.CustId);
                        MessageBox.Show("Customer deleted successfully!");
                        DisplayCustomers();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.");
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
        private Customers cu;
        private void dash_customer_Click(object sender, EventArgs e)
        {
            cu.BringToFront();
        }

        private void dash_venue_Click(object sender, EventArgs e)
        {
            Venue venue = new Venue();
            venue.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //event
            Event ev = new Event();
            ev.Show();
        }

        private void dash_feedback_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();

        }
    }


}

