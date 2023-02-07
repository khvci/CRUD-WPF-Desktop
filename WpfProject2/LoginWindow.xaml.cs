
using System.Data.SqlClient;
using System.Windows;

namespace WpfProject2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            string name = txtName.Text;
            string employeeId = txtEmployeeId.Text;

            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Employees WHERE FirstName = @name AND EmployeeID = @employeeID", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@employeeID", employeeId);

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
