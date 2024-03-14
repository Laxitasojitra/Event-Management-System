using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;

namespace EventManagementSystem
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class VenueService : IVenueService
    {
        private string connectionString = "Data Source=LAXITASOJITRA\\SQLEXPRESS;Initial Catalog=Eventmgmt;Integrated Security=True";
        public void AddVenue(Venue venueDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO VenueTbl (VName, VCapacity, VAddress, VManager, VPhone) " +
                               "VALUES (@VName, @VCapacity, @VAddress, @VManager, @VPhone)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VName", venueDetails.VName);
                command.Parameters.AddWithValue("@VCapacity", venueDetails.VCapacity);
                command.Parameters.AddWithValue("@VAddress", venueDetails.VAddress);
                command.Parameters.AddWithValue("@VManager", venueDetails.VManager);
                command.Parameters.AddWithValue("@VPhone", venueDetails.VPhone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateVenue(Venue venueDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE VenueTbl " +
                               "SET VName = @VName, VCapacity = @VCapacity, VAddress = @VAddress, " +
                               "VManager = @VManager, VPhone = @VPhone " +
                               "WHERE VId = @VId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VId", venueDetails.VId);
                command.Parameters.AddWithValue("@VName", venueDetails.VName);
                command.Parameters.AddWithValue("@VCapacity", venueDetails.VCapacity);
                command.Parameters.AddWithValue("@VAddress", venueDetails.VAddress);
                command.Parameters.AddWithValue("@VManager", venueDetails.VManager);
                command.Parameters.AddWithValue("@VPhone", venueDetails.VPhone);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteVenue(int venueId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM VenueTbl WHERE VId = @VId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VId", venueId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Venue> GetAllVenues()
        {
            List<Venue> venues = new List<Venue>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM VenueTbl";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venue venue = new Venue
                    {
                        VId = Convert.ToInt32(reader["VId"]),
                        VName = reader["VName"].ToString(),
                        VCapacity = Convert.ToInt32(reader["VCapacity"]),
                        VAddress = reader["VAddress"].ToString(),
                        VManager = reader["VManager"].ToString(),
                        VPhone = reader["VPhone"].ToString()
                    };

                    venues.Add(venue);
                }

                reader.Close();
            }

            return venues;
        }
    }
}
