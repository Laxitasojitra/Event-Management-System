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
using System.Xml.Linq;
using Client.ServiceReference1;
using Client.ServiceReference2;
using Client.ServiceReference3;
using Client.ServiceReference4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Feedback : Form
    {
        
        private FeedbackServiceClient _feedbackServiceClient;
        private ServiceReference3.Feedback[] FeedbackArray;


        public Feedback()
        {
            InitializeComponent();
            _feedbackServiceClient = new FeedbackServiceClient();
            DisplayFeedbacks();
        }
        private void Feedback_Load(object sender, EventArgs e)
        {
            //load for event and venue
            LoadVenues();
            LoadEvents();
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
           
        }

        private void LoadEvents()
        {
            try
            {
                // Fetch event data from the service reference
                EventServiceClient eventService = new EventServiceClient(); // Assuming the service reference is named EventServiceClient
                ServiceReference2.Event[] eventArray = eventService.GetAllEvents(); // Assuming the service method for fetching all events is named GetAllEvents()

                // Populate the combo box or data grid view with event data
                foreach (var evt in eventArray)
                {
                    comboBox4.Items.Add(evt.EvId); // Assuming comboBox4 is for Event ID

                    // You can also add event data to other controls if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading events: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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




        private void DisplayFeedbacks()
        {
            FeedbackArray = _feedbackServiceClient.GetAllFeedbacks();
            List<ServiceReference3.Feedback> feedbacks = FeedbackArray.ToList();
            dataGridViewFeedbacks.AutoGenerateColumns = false;

            // Clear existing rows in the DataGridView
            dataGridViewFeedbacks.Rows.Clear();

            // Manually add columns to the DataGridView
            dataGridViewFeedbacks.Columns.Clear();
            dataGridViewFeedbacks.Columns.Add("FbNum", "Feedback Number");
            dataGridViewFeedbacks.Columns.Add("EvId", "Event ID");
            dataGridViewFeedbacks.Columns.Add("EvName", "Event Name");
            dataGridViewFeedbacks.Columns.Add("Venue", "Venue");
            dataGridViewFeedbacks.Columns.Add("Punctuality", "Punctuality");
            dataGridViewFeedbacks.Columns.Add("Hospitality", "Hospitality");
            
            // Add new rows to the DataGridView
            foreach (var feedback in feedbacks)
            {
                dataGridViewFeedbacks.Rows.Add(
                    feedback.FbNum,
                    feedback.EvId,
                    feedback.EvName,
                    feedback.Venue,
                    feedback.Punctuality,
                    feedback.Hospitality         
                );
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

      

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //save
            try
            {
                // Create a new Feedback object
                ServiceReference3.Feedback newFeedback = new ServiceReference3.Feedback
                {
                    // Fill in the properties of the Feedback object based on user input
                    EvId = comboBox4.SelectedIndex,
                    EvName = textBox1.Text,
                    Venue = comboBox4.SelectedIndex,
                    Punctuality = comboBox2.SelectedIndex,
                    Hospitality = comboBox3.SelectedIndex,
                    // Add more properties as needed
                };

                // Add the new feedback using the service client
                _feedbackServiceClient.AddFeedback(newFeedback);

                // Refresh the display of feedbacks
                DisplayFeedbacks();

                // Show a success message
                MessageBox.Show("Feedback added successfully!");
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
                _feedbackServiceClient = new FeedbackServiceClient();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //reset button
          

        }
        


        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //delete button
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //customer redirection
            Customers c = new Customers();
            c.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // venue redirection
            Venue venue = new Venue();
            venue.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //event redirection
            Event ev = new Event();
            ev.Show();
        }

        private Feedback feedback;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //feedback redirection
            feedback.BringToFront();
        }

        private void dataGridViewFeedbacks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected event ID from the combo box
            int selectedEventId = int.Parse(comboBox4.Text);

            // Fetch event data from the service reference
            EventServiceClient eventService = new EventServiceClient();
            ServiceReference2.Event[] eventArray = eventService.GetAllEvents();

            // Find the corresponding event object in the event array
            var selectedEvent = eventArray.FirstOrDefault(ev => ev.EvId == selectedEventId);

            // If a matching event is found, set the event name textbox accordingly
            if (selectedEvent != null)
            {
                textBox1.Text = selectedEvent.EvName;
            }


        }


        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new Feedback object
                ServiceReference3.Feedback newFeedback = new ServiceReference3.Feedback
                {
                    // Fill in the properties of the Feedback object based on user input
                   
                    EvId = int.Parse(comboBox4.Text),
                    EvName = textBox1.Text,
                    Venue = comboBox4.SelectedIndex,
                    Punctuality = comboBox2.SelectedIndex,
                    Hospitality = comboBox3.SelectedIndex,
                    // Add more properties as needed
                };

                // Add the new feedback using the service client
                _feedbackServiceClient.AddFeedback(newFeedback);

                // Refresh the display of feedbacks
                DisplayFeedbacks();

                // Show a success message
                MessageBox.Show("Feedback added successfully!");
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
                _feedbackServiceClient = new FeedbackServiceClient();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void delete_click(object sender, EventArgs e)
        {
            if (dataGridViewFeedbacks.SelectedRows.Count > 0)
            {


                var selectedRow = dataGridViewFeedbacks.SelectedRows[0];
                var selectedFeedbackId = (int)selectedRow.Cells["FbNum"].Value;
                var selectedFeedback = FeedbackArray.FirstOrDefault(feedback => feedback.FbNum == selectedFeedbackId);

                if (selectedFeedback != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete this feedback?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _feedbackServiceClient.DeleteFeedback(selectedFeedback.FbNum);
                        MessageBox.Show("feedback deleted successfully!");
                        DisplayFeedbacks();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a feedback to delete.");
                }
            }
            else
            {
                MessageBox.Show("Please select a feedback to delete.");
            }

        }

        
    }
}
