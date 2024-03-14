using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManagementSystem;

namespace Host1
{
    public partial class Form1 : Form
    {
        private ServiceHost customerServiceHost;
        private ServiceHost eventServiceHost;
        private ServiceHost feedbackServiceHost;
        private ServiceHost venueServiceHost;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Customer Service
                customerServiceHost = new ServiceHost(typeof(EventManagementSystem.CustomerService));
                customerServiceHost.Open();

                // Event Service
                eventServiceHost = new ServiceHost(typeof(EventManagementSystem.EventService));
                eventServiceHost.Open();

                // Feedback Service
                feedbackServiceHost = new ServiceHost(typeof(EventManagementSystem.FeedbackService));
                feedbackServiceHost.Open();

                // Venue Service
                venueServiceHost = new ServiceHost(typeof(EventManagementSystem.VenueService));
                venueServiceHost.Open();

                label1.Text = "All services are running...";
            }
            catch (Exception ex)
            {
                label1.Text = "Error: " + ex.Message;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Close service hosts when the form is closing
            customerServiceHost?.Close();
            eventServiceHost?.Close();
            feedbackServiceHost?.Close();
            venueServiceHost?.Close();
        }
    }
}
