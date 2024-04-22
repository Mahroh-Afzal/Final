using FinalEM;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace FinalProjectMaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent(); // This initializes the UI components defined in XAML


        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;

            UserSQLManager userManager = new UserSQLManager();
            var user = userManager.GetUserByUsernameAndPassword(username, password);

            if (user != null)
            {
                // Login success
                switch (user.UserType)
                {
                    case 0:
                        await Navigation.PushModalAsync(new UserPage(user));
                        break;

                    case 1:
                        // await Navigation.PushModalAsync(new AdminOptions(user));
                        AdminField.IsVisible = true;
                        LoginField.IsVisible = false;
                        UserLabel.Text += user.FirstName;
                        CurrentTask.IsVisible = true;
                        CurrentTask.Text += user.Assignment;
                        break;
                }
            }
            else
            {
                // Login failed
                await DisplayAlert("Login Failed", "Please try again", "OK");
            }
        }

        private async void Budget(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BudgetManagerPage());

        }
        private async void EventManageOnClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EventCreationPage());

        }
        private void update(object sender, EventArgs e)
        {
            TaskButton.Text = "Assign task to " + Name.Text;
        }
        private void AssignTasks(object sender, EventArgs e)
        {
            AdminField.IsVisible = false;
            TaskField.IsVisible = true;
            UserSQLManager userManager = new UserSQLManager();
            List<User> allUsers = userManager.GetAllUsers();

            TableSection usersTableSection = new TableSection("Users");

            foreach (User user in allUsers)
            {
                TextCell userCell = new TextCell
                {
                    Text = $"{user.FirstName} {user.LastName} {user.Age}",
                    Detail = "Current task:" + user.Assignment 
                };

                usersTableSection.Add(userCell);
            }

            // Clear previous data and add the new section
            UserView.Root.Clear();
            UserView.Root.Add(usersTableSection);
        }
        private void Back(object sender, EventArgs e)
        {
            AdminField.IsVisible = true;
            TaskField.IsVisible = false;
        }
        private async void Assign(object sender, EventArgs e)
        {
            UserSQLManager userManager = new UserSQLManager();
            User user = userManager.GetUserByName(Name.Text);
            CurrentTask.Text += user.Assignment;
            user.Assignment = Task.Text;
            userManager.UpdateUser(user);
            await DisplayAlert(Name.Text + " has been asigned to: " + Task.Text, "Press OK to continue", "OK");
            Name.Text = "";
            Task.Text = "";
        }
        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        private async void RegisterPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }

    }
}