using System.Diagnostics;

namespace FinalProjectMaui
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            UserSQLManager sqliteDb = new UserSQLManager();
            var firstName = firstNameEntry.Text;
            var lastName = lastNameEntry.Text;
            var ageText = personAge.Text;
            var email = emailEntry.Text;
            var phoneNumber = phoneNumberEntry.Text;
            var username = userNameEntry.Text;
            var password = passwordEntry.Text;
            var userTypeText = userType.SelectedIndex;

            if (!int.TryParse(ageText, out int age))
            {
                // If parsing fails, inform the user and exit the method
                await DisplayAlert("Error", "Please enter a valid age.", "OK");
                return;
            }

            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                PhoneNumber = phoneNumber,
                Username = username,
                Password = password,
                UserType = userTypeText
            };

            sqliteDb.AddUser(newUser);

            await Navigation.PushModalAsync(new MainPage());
        }

    }
}