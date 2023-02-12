using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Customer selectedCustomer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            GetCustomers();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            UserDetails userDetails = new UserDetails();
            userDetails.ShowDialog();
            GetCustomers();
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {

            if (DataGrid.SelectedItem != null)
            {
                UpdateCustomer();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RemoveCustomer();
            }
            catch (System.Exception)
            {
                MessageBox.Show("This customer has an order, so cannot be deleted.");
            }
        }


        public void GetCustomers()
        {
            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Customers", connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    DataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
        }

        private void UpdateCustomer()
        {
            DataRowView row = (DataRowView)DataGrid.SelectedItem;
            string customerID = row["CustomerID"].ToString();

            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Customers SET CompanyName=@companyName, " +
                    "ContactName=@contactName, ContactTitle=@contactTitle, " +
                    "Address=@address, City=@city, Region=@region, PostalCode=@postalCode, Country=@country, Phone=@phone, " +
                    "Fax=@fax WHERE CustomerID=@id", connection))
                {
                    command.Parameters.AddWithValue("@id", customerID);
                    command.Parameters.AddWithValue("@companyName", row["CompanyName"]);
                    command.Parameters.AddWithValue("@contactName", row["ContactName"]);
                    command.Parameters.AddWithValue("@contactTitle", row["ContactTitle"]);
                    command.Parameters.AddWithValue("@address", row["Address"]);
                    command.Parameters.AddWithValue("@city", row["City"]);
                    command.Parameters.AddWithValue("@region", row["Region"]);
                    command.Parameters.AddWithValue("@postalCode", row["PostalCode"]);
                    command.Parameters.AddWithValue("@country", row["Country"]);
                    command.Parameters.AddWithValue("@phone", row["Phone"]);
                    command.Parameters.AddWithValue("@fax", row["Fax"]);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        GetCustomers();
                        MessageBox.Show("Customer updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer.");
                    }
                }
            }
        }

        private void RemoveCustomer()
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to remove.");
            }
            else
            {
                DataRowView row = (DataRowView)DataGrid.SelectedItem;
                string customerID = row["CustomerID"].ToString();

                using (SqlConnection connection = new SqlConnection(Connection.connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @id", connection);
                    cmd.Parameters.AddWithValue("@id", customerID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        GetCustomers();
                        MessageBox.Show("Customer removed successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove customer.");
                    }
                }
            }
        }
    }
}
