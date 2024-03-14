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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Event : Form
    {
        //for dashboard
        
        private ServiceReference2.EventServiceClient _eventServiceClient;
        private ServiceReference2.Event[] eventArray;
        private ServiceReference4.Venue[] venueArray;
        private Event selectedEvent;
        //for updatation
        private Client.ServiceReference2.EventServiceClient proxy;

        public Event()
        {
            InitializeComponent();
            _eventServiceClient = new EventServiceClient();
            DisplayEvents();
            proxy = new Client.ServiceReference2.EventServiceClient("BasicHttpBinding_IProductService");




        }
        private void DisplayEvents()
        {
            eventArray = _eventServiceClient.GetAllEvents();
            List<ServiceReference2.Event> events = eventArray.ToList();
            dataGridViewEvents.AutoGenerateColumns = false; // Disable auto-generation of columns

            dataGridViewEvents.Columns.Clear();
            // Manually add columns to the DataGridView
            dataGridViewEvents.Columns.Add("EvId", "Event ID");
            dataGridViewEvents.Columns.Add("EvName", "Name");
            dataGridViewEvents.Columns.Add("EvDate", "Date");
            dataGridViewEvents.Columns.Add("VenueId", "Venue ID");
            dataGridViewEvents.Columns.Add("EvVenue", "Venue");
            dataGridViewEvents.Columns.Add("EvDuration", "Duration");
            dataGridViewEvents.Columns.Add("EvCusId", "Customer ID");
            dataGridViewEvents.Columns.Add("EvCustName", "Customer Name");
            dataGridViewEvents.Columns.Add("EvStatus", "Status");


            // Set the values for each cell from the list of events
            foreach (var ev in events)
            {
                dataGridViewEvents.Rows.Add(
                    ev.EvId,
                    ev.EvName,
                    ev.EvDate,
                    ev.VenueId,
                    ev.EvVenue,
                    ev.EvDuration,
                    ev.EvCusId,
                    ev.EvCustName,
                    ev.EvStatus
                );
            }
        }

        private void Event_Load(object sender, EventArgs e)
        {
            LoadVenues();
            LoadCustomers();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged_1;


        }
        private void LoadCustomers()
        {
            try
            {
                // Fetch customer data from the service reference
                CustomerServiceClient customerService = new CustomerServiceClient();
                ServiceReference1.Customer[] customerArray = customerService.GetAllCustomers();

                // Populate the combo box or data grid view with customer data
                foreach (var customer in customerArray)
                {
                    comboBox3.Items.Add(customer.CustId);
                  
                    // You can also add customer data to other controls if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVenues()
        {
            try
            {
                // Fetch venue data from the service reference
                VenueServiceClient venueService = new VenueServiceClient();
                ServiceReference4.Venue[] venueArray = venueService.GetAllVenues();

                // Populate the combo box with venue IDs 
                foreach (var venue in venueArray)
                {
                    comboBox1.Items.Add(venue.VId);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new event object with the values from the input controls
                ServiceReference2.Event newEvent = new ServiceReference2.Event
                {
                    EvName = textBox1.Text,
                    EvDate = dateTimePicker1.Value,
                    VenueId = int.Parse(comboBox1.Text),
                    EvVenue = textBox2.Text,
                    EvDuration = int.Parse(textBox4.Text),
                    EvCusId = int.Parse(comboBox3.Text),
                    EvCustName = textBox3.Text,
                    EvStatus = comboBox2.Text
                };

                // Add the new event using the service client
                _eventServiceClient.AddEvent(newEvent);

                // Refresh the DataGridView after adding the event
                DisplayEvents();

                MessageBox.Show("Event added successfully!");
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
                _eventServiceClient = new EventServiceClient();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (dataGridViewEvents.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridViewEvents.SelectedRows[0];
                    var selectedEventId = Convert.ToInt32(selectedRow.Cells["EvId"].Value);

                    // Retrieve the selected event from the eventArray
                    var selectedEvent = eventArray.FirstOrDefault(ev => ev.EvId == selectedEventId);



                    if (selectedEvent != null)
                    {
                        // Update the selected event's properties with the values from the input controls
                        selectedEvent.EvName = textBox1.Text;
                        selectedEvent.EvDate = dateTimePicker1.Value;
                        selectedEvent.VenueId = int.Parse(comboBox1.Text);
                        selectedEvent.EvVenue = textBox2.Text;
                        selectedEvent.EvDuration = int.Parse(textBox4.Text);
                        selectedEvent.EvCusId = int.Parse(comboBox3.Text);
                        selectedEvent.EvCustName = textBox3.Text;
                        selectedEvent.EvStatus = comboBox2.Text;

                        // Call the service method to update the event in the database
                        _eventServiceClient.UpdateEvent(selectedEvent);

                        // Refresh the DataGridView after updating
                        DisplayEvents();
                        MessageBox.Show("Event updated successfully!");
                    }
                }
            }
            catch (FormatException ex)
            {
                // Handle the format exception (e.g., display an error message)
                MessageBox.Show("Invalid input format: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating event: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void Close_Click(object sender, EventArgs e)
            {
                Application.Exit();

            }

            private void EvName_TextChanged(object sender, EventArgs e)
            {

            }

            private void panel1_Paint(object sender, PaintEventArgs e)
            {

            }

        private void dataGridViewEvents_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEvents.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridViewEvents.SelectedRows[0];
                    var selectedEventId = (int)selectedRow.Cells["EvId"].Value;

                    var selectedEvent = eventArray.FirstOrDefault(ev => ev.EvId == selectedEventId);

                    if (selectedEvent != null)
                    {
                        if (MessageBox.Show("Are you sure you want to delete this event?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _eventServiceClient.DeleteEvent(selectedEvent.EvId);
                            MessageBox.Show("Event deleted successfully!");
                            DisplayEvents();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select an event to delete.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select an event to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting event: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected venue ID from the combo box
                int selectedVenueId = int.Parse(comboBox1.Text);

                // Fetch venue data from the service reference
                VenueServiceClient venueService = new VenueServiceClient();
                ServiceReference4.Venue[] venueArray = venueService.GetAllVenues();

                // Find the corresponding venue object in the venue array
                var selectedVenue = venueArray.FirstOrDefault(v => v.VId == selectedVenueId);

                // If a matching venue is found, set the venue name textbox accordingly
                if (selectedVenue != null)
                {
                    textBox2.Text = selectedVenue.VName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving venue details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Get the selected customer ID from the combo box
            int selectedCustomerId = int.Parse(comboBox3.Text);

            // Fetch customer data from the service reference
            CustomerServiceClient customerService = new CustomerServiceClient();
            ServiceReference1.Customer[] customerArray = customerService.GetAllCustomers();

            // Find the corresponding customer object in the customer array
            var selectedCustomer = customerArray.FirstOrDefault(c => c.CustId == selectedCustomerId);

            // If a matching customer is found, set the customer name textbox accordingly
            if (selectedCustomer != null)
            {
                textBox3.Text = selectedCustomer.CustName;
            }
        }

       

      //for footer code

        private void dash_customer_Click(object sender, EventArgs e)
        {
            // Open the Customer form
            Customers c = new Customers();
            c.Show();

        }

        private void dash_venue_Click(object sender, EventArgs e)
        {
            Venue venue = new Venue();
            venue.Show();

        }
        private Event eventForm;
        private void dash_event_Click(object sender, EventArgs e)
        {
            eventForm.BringToFront();

        }

        private void dash_feedback_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();

        }
    }
}

