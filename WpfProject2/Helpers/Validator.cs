using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject2.Helpers
{
    public static class Validator
    {
        public static void UpdateCustomerIfValid(DataGrid grid)
        {
            if (grid.SelectedItem != null)
            {
                DAL.CustomerManager.UpdateCustomer(
                    (DataRowView)grid.SelectedItem);

                DAL.CustomerManager.GetCustomers(grid);
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
        }

        public static void RemoveCustomerIfValid(DataGrid grid)
        {
            if (grid.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to remove.");
            }
            else
            {
                try
                {
                    DAL.CustomerManager.RemoveCustomer(
                        (DataRowView)grid.SelectedItem);
                    DAL.CustomerManager.GetCustomers(grid);
                }
                catch (System.Exception)
                {
                    MessageBox.Show("This customer has an order, so cannot be deleted.");
                }
            }
        }
    }
}
