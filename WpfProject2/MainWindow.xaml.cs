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
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RemoveCustomer();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("This customer has an order, so cannot be deleted.");
            }
        }

        
        private void GetCustomers()
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

        private void RemoveCustomer()
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to remove.");
            }

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
