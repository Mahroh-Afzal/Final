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
            EventManagerSQLite EventManager = new EventManagerSQLite();
            List<Event> allEvents = EventManager.GetAllEvents();

            TableSection EventsTableSection = new TableSection("Events");

            foreach (Event Event in allEvents)
            {
                TextCell EventCell = new TextCell
                {
                    Text = $"{Event.Name}",
                    Detail = "Status: " + Event.Status.ToString() + " " + (Event.AvailableSlots - Event.MaxSlots) + "/" + Event.MaxSlots
                };

                EventsTableSection.Add(EventCell);
            }

            // Clear previous data and add the new section
            EventView.Root.Clear();
            EventView.Root.Add(EventsTableSection);
        }
        private async void OnClick(object sender, EventArgs e)
        {
            DefaultView.IsVisible = !DefaultView.IsVisible;
            CreateEventView.IsVisible = !CreateEventView.IsVisible;
            UtilBut.Text = (DefaultView.IsVisible) ? "Create Event" : "View Events";
            EventManagerSQLite EventManager = new EventManagerSQLite();
            List<Event> allEvents = EventManager.GetAllEvents();

            TableSection EventsTableSection = new TableSection("Events");

            foreach (Event Event in allEvents)
            {
                string temp = "";
                switch (Event.Status)
                {
                    case 0:
                         temp = "Pending";
                        break;
                    case 1:
                         temp = "Ongoing";
                        break;
                    case 2:
                         temp = "Completed";
                        break;
                }
                TextCell EventCell = new TextCell
                {
                    Text = $"{Event.Name}",
                    Detail = "Status: " + temp + " " + (Event.AvailableSlots - Event.MaxSlots) + "/" + Event.MaxSlots
                };

                EventsTableSection.Add(EventCell);
            }

            // Clear previous data and add the new section
            EventView.Root.Clear();
            EventView.Root.Add(EventsTableSection);
        }
        private async void CreateEvent_Clicked(object sender, EventArgs e)
        {

            EventManagerSQLite eventManager = new EventManagerSQLite();
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
                MaxSlots = availableSlots,
                Status = eventStatusPicker.SelectedIndex // Convert picker index to enum value
            };

            // Add event to the shared collection
            eventManager.AddEvent(newEvent);

             // Show confirmation
             await DisplayAlert("Success", "Event created successfully!", "OK");

            // Clear input fields
            eventNameEntry.Text = "";
            eventDescriptionEntry.Text = "";
            availableSlotsEntry.Text = "";

            // Navigate back to MainPage
            
        }


        private async void BackToEventList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

    }


}