using System.Windows;

namespace WpfProject2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            DAL.CustomerManager.GetCustomers(DataGrid);
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            DAL.CustomerManager.AddCustomer(DataGrid);
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Validator.UpdateCustomerIfValid(DataGrid);
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Validator.RemoveCustomerIfValid(DataGrid);
        }
    }
}
