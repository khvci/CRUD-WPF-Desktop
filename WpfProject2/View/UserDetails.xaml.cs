using System.Windows;

namespace WpfProject2
{
    public partial class UserDetails : Window
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer() 
            {
                CustomerID = txtCustomerID.Text,
                CompanyName = txtCompanyName.Text,
                ContactName = txtContactName.Text,
                ContactTitle = txtContactTitle.Text,
                Address = txtAddress.Text,
                City = txtCity.Text,
                Region = txtRegion.Text,
                PostalCode = txtPostalCode.Text,
                Country = txtCountry.Text,
                Phone = txtPhone.Text,
                Fax = txtFax.Text,
            };

            DAL.CustomerManager.AddCustomerToDB(customer);
            this.Close();
        }
    }
}
