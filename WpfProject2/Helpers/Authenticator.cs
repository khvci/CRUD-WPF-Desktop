using System.Data.SqlClient;
using System.Windows;

namespace WpfProject2.Helpers
{
    public static class Authenticator
    {
        public static void Authenticate(string name, string employeeId)
        {
            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Employees WHERE FirstName = @name AND EmployeeID = @employeeID", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@employeeID", employeeId);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed.");
                    }
                }
            }
        }
    }
}
