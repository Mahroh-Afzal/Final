using Microsoft.Maui.Controls;
using System;
using static FinalProjectMaui.MainPage;

namespace FinalProjectMaui
{
    public partial class EventCreationPage : ContentPage
    {
        public EventCreationPage()
        {
            InitializeComponent();
        }

        private async void CreateEvent_Clicked(object sender, EventArgs e)
        {
            // Get input values
            string eventName = eventNameEntry.Text;
            string eventDescription = eventDescriptionEntry.Text;
            int availableSlots;

            // Validate input
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(eventDescription) || !int.TryParse(availableSlotsEntry.Text, out availableSlots) || eventStatusPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please fill in all fields with valid values.", "OK");
                return;
            }

            // Create new event
            Event newEvent = new Event
            {
                Name = eventName,
                Description = eventDescription,
                Date = DateTime.Now,
                AvailableSlots = availableSlots,
                Status = (Event.EventStatus)eventStatusPicker.SelectedIndex // Convert picker index to enum value
            };

            // Add event to the shared collection
            MainPage.events.Add(newEvent);

            // Show confirmation
            await DisplayAlert("Success", "Event created successfully!", "OK");

            // Clear input fields
            eventNameEntry.Text = "";
            eventDescriptionEntry.Text = "";
            availableSlotsEntry.Text = "";

            // Navigate back to MainPage
            await Navigation.PopAsync();
        }


        private async void BackToEventList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }


}
