using System.Data.SqlClient;
using System.Windows;

namespace WpfProject2
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerToDB();
        }

        private void AddCustomerToDB()
        {
            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) " +
                                                    "VALUES (@id, @companyName, @contactName, @contactTitle, @address, @city, @region, @postalCode, @country, @phone, @fax)", connection);
                    cmd.Parameters.AddWithValue("@id", txtCustomerID.Text);
                    cmd.Parameters.AddWithValue("@companyName", txtCompanyName.Text);
                    cmd.Parameters.AddWithValue("@contactName", txtContactName.Text);
                    cmd.Parameters.AddWithValue("@contactTitle", txtContactTitle.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@city", txtCity.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegion.Text);
                    cmd.Parameters.AddWithValue("@postalCode", txtPostalCode.Text);
                    cmd.Parameters.AddWithValue("@country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@fax", txtFax.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer added successfully.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add customer.");
                    }
                }
                catch (System.Exception)
                {
                    MessageBox.Show("This ID has already been used. Please select another ID.");
                }
            }
        }
    }
}
