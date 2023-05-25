using Microsoft.Data.SqlClient;
using PhotographyBusiness.Models;
using System.Data;

namespace PhotographyBusiness.Services.ADOService
{
    public class SQL_Booking
    {
        static string connectionString = "Data Source=saunders.database.windows.net;Initial Catalog=SaundersDB;User" +
                " ID=adminjack;Password=Vgroupftw!;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application" +
                " Intent=ReadWrite;Multi Subnet Failover=False";
        public static void CreateBooking(Booking booking)
        {
            string query = "If exists(Select * from Bookings where Bookings.Date = @date)\r\n" +
                "Begin RaisError('This date is already booked. Choose another date',16,1);\r\nReturn End\r\n" +
                "Else Begin Insert Into Bookings " +
                "Values(@category,@price,@customerNote,@adminNote,@date,@dateCreate,@isAccepted,@address,@userId); End;";
            DateTime currentDataTime = DateTime.Now;
            DateTime BookingDataTime = booking.Date;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    if(BookingDataTime >= currentDataTime)
                    {
                        command.Parameters.AddWithValue("@category", booking.Category);
                        if (booking.Price == null)
                        {
                            command.Parameters.Add("@price", SqlDbType.Float).Value = DBNull.Value;
                        }
                        else
                        {
                            command.Parameters.Add("@price", SqlDbType.Float).Value = booking.Price;
                        }
                        command.Parameters.AddWithValue("@customerNote", booking.CustomerNote);
                        if (booking.Price == null)
                        {
                            command.Parameters.Add("@adminNote", SqlDbType.NVarChar, 4000).Value = DBNull.Value;
                        }
                        else
                        {
                            command.Parameters.Add("@adminNote", SqlDbType.NVarChar, 4000).Value = booking.AdminNote;
                        }
                        command.Parameters.AddWithValue("@date", booking.Date);
                        command.Parameters.AddWithValue("@dateCreate", booking.DateCreated);
                        command.Parameters.AddWithValue("@isAccepted", booking.IsAccepted);
                        command.Parameters.AddWithValue("@address", booking.Address);
                        command.Parameters.AddWithValue("@userId", booking.UserId);
                        int affectedRow = command.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("The date must be from today onwards.");
                    }
                    

                }
            }
        }
    }
}
