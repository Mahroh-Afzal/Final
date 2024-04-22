using FinalProjectMaui;

namespace FinalProjectMaui;

public partial class UserPage : ContentPage
{
    User User;
	public UserPage(User user)
	{
        User = user;

        InitializeComponent();
        dysplayEvents(user);
    }
    private void dysplayEvents(User user)
    {
        EventManagerSQLite EventManager = new EventManagerSQLite();
        List<Event> allEvents = EventManager.GetAllEvents();

        TableSection EventsTableSection = new TableSection("Events");

        foreach (Event Event in allEvents)
        {
            // Check if the event status is not Completed and there are available slots
            if (Event.Status != 2 && (Event.AvailableSlots - Event.MaxSlots) < Event.MaxSlots)
            {
                TextCell EventCell = new TextCell
                {
                    Text = $"{Event.Name}",
                    Detail = $"Status: {Event.Status.ToString()} " + (Event.AvailableSlots - Event.MaxSlots) + "/" + Event.MaxSlots,
                    Command = new Command(async () => await OnEventCellTapped(Event)),
                };

                EventsTableSection.Add(EventCell);
            }
            // If the event is Completed or there are no slots available, don't add to the list
        }

        // Clear previous data and add the new section
        EventView.Root.Clear();
        EventView.Root.Add(EventsTableSection);
    }
    public async Task OnEventCellTapped(Event eventObj)
    {
        EventManagerSQLite EventManager = new EventManagerSQLite();

        int remainingSlots = eventObj.MaxSlots - (eventObj.AvailableSlots - eventObj.MaxSlots);
        eventObj.AvailableSlots = remainingSlots;
        EventManager.UpdateEvent(eventObj);
        await DisplayAlert("Remaining Slots", $"There are {remainingSlots} remaining slots for the event {eventObj.Name}.", "OK");
        dysplayEvents(User);
    }
    private async void OnClick(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainPage());
    }
}
