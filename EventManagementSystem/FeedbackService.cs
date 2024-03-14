using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;

namespace EventManagementSystem
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class FeedbackService : IFeedbackService
    {
        private string connectionString = "Data Source=LAXITASOJITRA\\SQLEXPRESS;Initial Catalog=Eventmgmt;Integrated Security=True";
        public void AddFeedback(Feedback feedbackDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO FeedBacktbl (EvId, EvName, Venue, Punctuality, Hospitality, Overall) " +
                               "VALUES (@EvId, @EvName, @Venue, @Punctuality, @Hospitality, @Overall)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EvId", feedbackDetails.EvId);
                command.Parameters.AddWithValue("@EvName", feedbackDetails.EvName);
                command.Parameters.AddWithValue("@Venue", feedbackDetails.Venue);
                command.Parameters.AddWithValue("@Punctuality", feedbackDetails.Punctuality);
                command.Parameters.AddWithValue("@Hospitality", feedbackDetails.Hospitality);
                command.Parameters.AddWithValue("@Overall", feedbackDetails.Overall);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteFeedback(int feedbackNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM FeedBacktbl WHERE FbNum = @FbNum";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FbNum", feedbackNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateFeedback(Feedback updatedFeedback)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE FeedBacktbl " +
                               "SET EvId = @EvId, EvName = @EvName, Venue = @Venue, " +
                               "Punctuality = @Punctuality, Hospitality = @Hospitality, Overall = @Overall " +
                               "WHERE FbNum = @FbNum";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EvId", updatedFeedback.EvId);
                command.Parameters.AddWithValue("@EvName", updatedFeedback.EvName);
                command.Parameters.AddWithValue("@Venue", updatedFeedback.Venue);
                command.Parameters.AddWithValue("@Punctuality", updatedFeedback.Punctuality);
                command.Parameters.AddWithValue("@Hospitality", updatedFeedback.Hospitality);
                command.Parameters.AddWithValue("@Overall", updatedFeedback.Overall);
                command.Parameters.AddWithValue("@FbNum", updatedFeedback.FbNum);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public List<Feedback> GetAllFeedbacks()
        {
            List<Feedback> feedbacks = new List<Feedback>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM FeedBacktbl";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Feedback feedback = new Feedback
                    {
                        FbNum = Convert.ToInt32(reader["FbNum"]),
                        EvId = Convert.ToInt32(reader["EvId"]),
                        EvName = reader["EvName"].ToString(),
                        Venue = Convert.ToInt32(reader["Venue"]),
                        Punctuality = Convert.ToInt32(reader["Punctuality"]),
                        Hospitality = Convert.ToInt32(reader["Hospitality"]),
                        Overall = Convert.ToInt32(reader["Overall"])
                    };

                    feedbacks.Add(feedback);
                }

                reader.Close();
            }

            return feedbacks;
        }
    }
}
