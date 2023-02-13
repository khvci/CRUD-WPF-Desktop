using System.Windows;

namespace WpfProject2
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Authenticator.Authenticate(txtName.Text, txtEmployeeId.Text);
        }
    }
}
