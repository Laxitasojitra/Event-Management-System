using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;

namespace EventManagementSystem
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class EventService : IEventService
    {
        private string connectionString = "Data Source=LAXITASOJITRA\\SQLEXPRESS;Initial Catalog=Eventmgmt;Integrated Security=True";
        public void AddEvent(Event eventDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO EventTbl (EvName, EvDate, VenueId, EvVenue, EvDuration, EvCusId, EvCustName, EvStatus) " +
                               "VALUES (@EvName, @EvDate, @VenueId, @EvVenue, @EvDuration, @EvCusId, @EvCustName, @EvStatus)";

                SqlCommand command = new SqlCommand(query, connection);
             
                command.Parameters.AddWithValue("@EvName", eventDetails.EvName);
                command.Parameters.AddWithValue("@EvDate", eventDetails.EvDate);
                command.Parameters.AddWithValue("@VenueId", eventDetails.VenueId);
                command.Parameters.AddWithValue("@EvVenue", eventDetails.EvVenue);
                command.Parameters.AddWithValue("@EvDuration", eventDetails.EvDuration);
                command.Parameters.AddWithValue("@EvCusId", eventDetails.EvCusId);
                command.Parameters.AddWithValue("@EvCustName", eventDetails.EvCustName);
                command.Parameters.AddWithValue("@EvStatus", eventDetails.EvStatus);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEvent(Event eventDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE EventTbl " +
                               "SET EvName = @EvName, EvDate = @EvDate, VenueId = @VenueId, " +
                               "EvVenue = @EvVenue, EvDuration = @EvDuration, EvCusId = @EvCusId, " +
                               "EvCustName = @EvCustName, EvStatus = @EvStatus " +
                               "WHERE EvId = @EvId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EvId", eventDetails.EvId);
                command.Parameters.AddWithValue("@EvName", eventDetails.EvName);
                command.Parameters.AddWithValue("@EvDate", eventDetails.EvDate);
                command.Parameters.AddWithValue("@VenueId", eventDetails.VenueId);
                command.Parameters.AddWithValue("@EvVenue", eventDetails.EvVenue);
                command.Parameters.AddWithValue("@EvDuration", eventDetails.EvDuration);
                command.Parameters.AddWithValue("@EvCusId", eventDetails.EvCusId);
                command.Parameters.AddWithValue("@EvCustName", eventDetails.EvCustName);
                command.Parameters.AddWithValue("@EvStatus", eventDetails.EvStatus);

                
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM EventTbl WHERE EvId = @EvId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EvId", eventId);

                
                command.ExecuteNonQuery();
            }
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM EventTbl";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Event ev = new Event
                    {
                        EvId = Convert.ToInt32(reader["EvId"]),
                        EvName = reader["EvName"].ToString(),
                        EvDate = Convert.ToDateTime(reader["EvDate"]),
                        VenueId = Convert.ToInt32(reader["VenueId"]),
                        EvVenue = reader["EvVenue"].ToString(),
                        EvDuration = Convert.ToInt32(reader["EvDuration"]),
                        EvCusId = Convert.ToInt32(reader["EvCusId"]),
                        EvCustName = reader["EvCustName"].ToString(),
                        EvStatus = reader["EvStatus"].ToString()
                    };

                    events.Add(ev);
                }

                reader.Close();
            }

            return events;
        }
    }
}
