using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
    public partial class Venue : Form
    {
       
        private VenueServiceClient _venueServiceClient;
        private ServiceReference4.Venue[] venueArray;

        public Venue()
        {
            InitializeComponent();
            _venueServiceClient = new VenueServiceClient();
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void Venue_Load(object sender, EventArgs e)
        {
            DisplayVenues();

        }

        private void DisplayVenues()
        {
            venueArray = _venueServiceClient.GetAllVenues();
            List<ServiceReference4.Venue> venues = venueArray.ToList();
            dataGridViewVenue.AutoGenerateColumns = false;

             dataGridViewVenue.Columns.Clear();
            // Manually add columns to the DataGridView
            dataGridViewVenue.Columns.Add("VId", "Venue ID");
            dataGridViewVenue.Columns.Add("VName", "Venue Name");
            dataGridViewVenue.Columns.Add("VCapacity", "Venue Capacity");
            dataGridViewVenue.Columns.Add("VAddress", "Venue Location");
            dataGridViewVenue.Columns.Add("VManager", "Venue Manager");
            dataGridViewVenue.Columns.Add("VPhone", "Venue Phone");

            foreach (var venue in venues)
            {
                dataGridViewVenue.Rows.Add(
                    venue.VId,
                    venue.VName,
                    venue.VCapacity,
                    venue.VAddress,
                    venue.VManager,
                    venue.VPhone
                );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                int capacity;
                if (!int.TryParse(textBox3.Text, out capacity))
                {
                    MessageBox.Show("Capacity must be a valid integer.");
                    return;
                }
                string address = textBox6.Text;
                string phone = textBox4.Text;
                string manager = textBox2.Text;



                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                {
                    ServiceReference4.Venue newVenue = new ServiceReference4.Venue
                    {
                        VName = name,
                        VCapacity = capacity,
                        VAddress = address,
                        VManager = manager,
                        VPhone = phone
                    };

                    _venueServiceClient.AddVenue(newVenue);
                    DisplayVenues();
                    MessageBox.Show("Venue added successfully!");
                }
                else
                {
                    MessageBox.Show("Please enter both name and location.");
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
                _venueServiceClient = new VenueServiceClient();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewVenue.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewVenue.SelectedRows[0];
                var selectedVenueId = (int)selectedRow.Cells["VId"].Value;

                var selectedVenue = venueArray.FirstOrDefault(venue => venue.VId == selectedVenueId);

                if (selectedVenue != null)
                {
                    int capacity; // Added capacity variable
                    if (!int.TryParse(textBox3.Text, out capacity))
                    {
                        MessageBox.Show("Capacity must be a valid integer.");
                        return;
                    }

                    var updatedVenue = new ServiceReference4.Venue
                    {

                     VId = selectedVenue.VId,
                        VName = textBox1.Text,
                        VCapacity = Convert.ToInt32(textBox3.Text),
                        VAddress = textBox6.Text,
                        VManager = textBox2.Text,
                        VPhone = textBox4.Text
                    };

                    _venueServiceClient.UpdateVenue(updatedVenue);

                    MessageBox.Show("Venue data updated successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a venue to edit.");
            }

            DisplayVenues();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewVenue.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewVenue.SelectedRows[0];
                var selectedVenueId = (int)selectedRow.Cells["VId"].Value;

                var selectedVenue = venueArray.FirstOrDefault(venue => venue.VId == selectedVenueId);

                if (selectedVenue != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete this venue?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _venueServiceClient.DeleteVenue(selectedVenue.VId);
                        MessageBox.Show("Venue deleted successfully!");
                        DisplayVenues();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a venue to delete.");
                }
            }
            else
            {
                MessageBox.Show("Please select a venue to delete.");
            }
        }

        private void dataGridViewVenue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dash_customer_Click(object sender, EventArgs e)
        {
            Customers c = new Customers();
            c.Show();
        }
        private Venue venue;

        private void dash_venue_Click(object sender, EventArgs e)
        {
            venue.BringToFront();
        }

        private void dash_event_Click(object sender, EventArgs e)
        {
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
