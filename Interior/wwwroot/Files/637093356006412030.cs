using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace EmailService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                Execute();
                System.Threading.Thread.Sleep(120000);
            }

        }

        static void Execute()
        {
            try
            {
                Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
                string connStr = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
                string minutes = ConfigurationManager.AppSettings["minutes"];
                string tabletApi = ConfigurationManager.AppSettings["tabletApi"];

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    string sql = "SELECT distinct Notifications.ID, Users.[NotificationEmail] FROM Users" +
                        " LEFT JOIN Notifications ON Notifications.UserID = Users.ID" +
                        " WHERE Notifications.[Read] = 0 AND Users.[NotificationEmail] IS NOT NULL AND " +
                        " Notifications.[Emailed] = 0 AND" +
                        " CURRENT_TIMESTAMP > DATEADD(MINUTE, 5, Notifications.CreatedDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string NewSql = "";
                                while (reader.Read())
                                {
                                    if (!reader.IsDBNull(1))
                                    {
                                        string mail = "";
                                        mail = reader.GetString(1);
                                        Int64 id = reader.GetInt64(0);

                                        EmailSender.Send(new List<string> { reader.GetString(1) },
                                            "You have a new notification", "New Notification");
                                        NewSql += "UPDATE Notifications SET Notifications.Emailed = 1 WHERE ID=" + id.ToString() + "; ";

                                    }



                                }
                                if (!string.IsNullOrEmpty(NewSql))
                                {
                                    using (SqlCommand new_command = new SqlCommand(NewSql, connection))
                                    {
                                        new_command.ExecuteNonQuery();
                                    }
                                }
                            }

                        }
                    }

                    sql = "SELECT Applications.ID" +
                         " FROM Applications INNER JOIN Users ON Applications.CaUserID = Users.ID" +
                         " WHERE Applications.MeetDate < DATEADD(MINUTE, 2, CURRENT_TIMESTAMP) AND Applications.EmailedMeet = 0;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            string NewSql =string.Empty;

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    Int64 id = reader.GetInt64(0);

                                    string html = string.Empty;
                                    string url = tabletApi + "api/Application/bo-send-email/" + id.ToString();

                                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                                    request.AutomaticDecompression = DecompressionMethods.GZip;

                                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                    using (Stream stream = response.GetResponseStream())
                                    using (StreamReader r = new StreamReader(stream))
                                    {
                                        html = r.ReadToEnd();
                                    }

                                    Console.WriteLine(html);

                                   // NewSql += "UPDATE Applications SET Applications.EmailedMeet = 1 WHERE ID=" + id.ToString() + "; ";

                                }

                                //if (!string.IsNullOrEmpty(NewSql))
                                //{
                                //    using (SqlCommand new_command = new SqlCommand(NewSql, connection))
                                //    {
                                //        new_command.ExecuteNonQuery();
                                //    }
                                //}
                            }
                            
                        }
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
