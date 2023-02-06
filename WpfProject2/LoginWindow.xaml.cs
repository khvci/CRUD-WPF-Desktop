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

            string name = txtName.Text;
            string employeeId = txtCustomerId.Text;

            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=Northwind;Integrated Security=True"))
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
                        mainWindow.Show();
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
