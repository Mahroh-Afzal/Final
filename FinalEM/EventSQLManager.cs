using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProjectMaui
{


       public class Event
       {
            [PrimaryKey, AutoIncrement]
           public int ID { get; set; }
           public string Name { get; set; }
           public DateTime Date { get; set; }
           public string Description { get; set; }
           public int AvailableSlots { get; set; }
           public int MaxSlots { get; set; }
           public int Status { get; set; }
       }

       public class EventManagerSQLite
       {
           //create an object for connection to db
           private SQLiteConnection database;

           public EventManagerSQLite()
           {
               //instantiating the connection object with the path to the db
               //path to db is specifed in Constants class
               this.database = new SQLiteConnection(Constants.DatabasePath);

               //replaces sql quary to create a table for Event class
               this.database.CreateTable<Event>();
           }


           //add Event 
           public void AddEvent(Event Event)
           {
               this.database.Insert(Event);
           }

           //Get Event by name
           public List<Event> GetEventsByName(string filter)
           {
               return this.database.Table<Event>().Where(u => u.Name.Contains(filter)).ToList();
           }

           // Find Event by ID
           public Event GetEventbyID(int ID)
           {
               return this.database.Table<Event>().FirstOrDefault(b => b.ID == ID);
           }
           //Find Events by Status
           public List<Event> GetEventsbyStatus(int b)
           {
                return this.database.Table<Event>().Where(u => u.Status.Equals(b)).ToList();
           }

           // Find Events by email
           public List<Event> GetEventByDate(DateTime b)
           {
               return this.database.Table<Event>().Where(u => u.Date.Equals(b)).ToList();
           }


           //delete Events 
           public void DeleteEvent(int id)
           {
               var Event = this.database.Table<Event>().FirstOrDefault(b => b.ID == id);
               if (Event != null)
               {
                   this.database.Delete(Event);

               }
           }

           //update a Events by id
           public void UpdateEvent(Event student)
           {
               this.database.Update(student);
           }

           //Get a list of all Events in db
           public List<Event> GetAllEvents()
           {
               return this.database.Table<Event>().ToList();
           }

           public void DeleteAllEvents()
           {
               database.DeleteAll<Event>();
           }
           public bool IsDatabaseEmpty()
           {
               int EventCount = database.Table<Event>().Count();
               return EventCount == 0;
           }

       }
   
}
