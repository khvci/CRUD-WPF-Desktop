using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject2.DAL
{
    public static class CustomerManager
    {
        public static void GetCustomers(DataGrid grid)
        {
            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Customers", connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    grid.ItemsSource = dataTable.DefaultView;
                }
            }
        }

        public static void UpdateCustomer(DataRowView row)
        {
            string customerID = row["CustomerID"].ToString();

            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
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

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer.");
                    }
                }
            }
        }

        public static void RemoveCustomer(DataRowView row)
        {
            string customerID = row["CustomerID"].ToString();

            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @id", connection);
                cmd.Parameters.AddWithValue("@id", customerID);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Customer removed successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to remove customer.");
                }
            }
        }

        public static void AddCustomer(DataGrid grid)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.ShowDialog();
            DAL.CustomerManager.GetCustomers(grid);
        }

        public static void AddCustomerToDB(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Connection.connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) " +
                                                    "VALUES (@id, @companyName, @contactName, @contactTitle, @address, @city, @region, @postalCode, @country, @phone, @fax)", connection);
                    cmd.Parameters.AddWithValue("@id", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@companyName", customer.CompanyName);
                    cmd.Parameters.AddWithValue("@contactName", customer.ContactName);
                    cmd.Parameters.AddWithValue("@contactTitle", customer.ContactTitle);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@city", customer.City);
                    cmd.Parameters.AddWithValue("@region", customer.Region);
                    cmd.Parameters.AddWithValue("@postalCode", customer.PostalCode);
                    cmd.Parameters.AddWithValue("@country", customer.Country);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@fax", customer.Fax);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer added successfully.");
                        //this.Close();
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
