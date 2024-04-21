using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace FinalProjectMaui
{
    public partial class MainPage : ContentPage
    {
        public static ObservableCollection<Event> events = new ObservableCollection<Event>();

        public MainPage()
        {
            InitializeComponent(); // This initializes the UI components defined in XAML

            // Sample Events
            events.Add(new Event { Name = "Museam Visting Event", Date = DateTime.Now.AddDays(1), AvailableSlots = 2 });
            events.Add(new Event { Name = "Shinjuku Visiting Event", Date = DateTime.Now.AddDays(2), AvailableSlots = 3 });
            events.Add(new Event { Name = "Wano Visiting Event", Date = DateTime.Now.AddDays(3), AvailableSlots = 3 });


            eventListView.ItemsSource = events;

        }

       
                      

        // Event handler for Edit Event button
        private async void EditEvent_Clicked(object sender, EventArgs e)
        {
            // Get the selected event
            var selectedEvent = eventListView.SelectedItem as Event;

            // If an event is selected
            if (selectedEvent != null)
            {
                // Prompt the user to enter the new event title
                string newEventTitle = await DisplayPromptAsync("Edit Event", "Enter the new event title:", "OK", "Cancel", selectedEvent.Name);

                // If the user entered a new title
                if (!string.IsNullOrWhiteSpace(newEventTitle))
                {
                    // Prompt the user to enter the new event description
                    string newEventDescription = await DisplayPromptAsync("Edit Event", "Enter the new event description:", "OK", "Cancel", selectedEvent.Description);

                    // If the user entered a new description
                    if (!string.IsNullOrWhiteSpace(newEventDescription))
                    {
                        // Update the event title and description
                        selectedEvent.Name = newEventTitle;
                        selectedEvent.Description = newEventDescription;

                        // Refresh the ListView by setting its ItemsSource again
                        eventListView.ItemsSource = null;
                        eventListView.ItemsSource = events;
                    }
                    else
                    {
                        // Show an error message if the description is empty
                        await DisplayAlert("Error", "Event description cannot be empty.", "OK");
                    }
                }
                else
                {
                    // Show an error message if the title is empty
                    await DisplayAlert("Error", "Event title cannot be empty.", "OK");
                }
            }
            else
            {
                // Show an error message if no event is selected
                await DisplayAlert("Error", "Please select an event to edit.", "OK");
            }
        }

        // Event handler for Delete Event button
        private void DeleteEvent_Clicked(object sender, EventArgs e)
        {
            var selectedEvent = eventListView.SelectedItem as Event;
            if (selectedEvent != null)
            {
                events.Remove(selectedEvent);
            }
            else
            {
                DisplayAlert("Error", "Please select an event to delete.", "OK");
            }
        }

        // Event handler for booking an event
        private void BookEvent_Clicked(object sender, EventArgs e)
        {
            var selectedEvent = eventListView.SelectedItem as Event;
            if (selectedEvent != null)
            {
                if (selectedEvent.AvailableSlots > 0)
                {
                    // Update event booking status
                    selectedEvent.AvailableSlots--;

                    // Display booking confirmation message
                    DisplayAlert("Booking Confirmation", "Event booked successfully!", "OK");
                }
                else
                {
                    // Display error message if no available slots
                    DisplayAlert("Error", "No available slots for this event.", "OK");
                }
            }
            else
            {
                // Display error message if no event is selected
                DisplayAlert("Error", "Please select an event to book.", "OK");
            }
        }

        private async void CreateEvent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventCreationPage());
        }


        private async void GoToBudgetManager_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BudgetManagerPage());
        }


        // Sample Event class representing an event
        public class Event
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public int AvailableSlots { get; set; }
            public EventStatus Status { get; set; }

            public enum EventStatus
            {
                Pending,
                Ongoing,
                Completed
            }
        }

    }
}
