using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FinalProjectMaui
{
    
        public class User
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int UserType { get; set; }
        public string Assignment {  get; set; }
        }
        public class UserSQLManager
        {
            //create an object for connection to db
            private SQLiteConnection database;

            public UserSQLManager()
            {
                //instantiating the connection object with the path to the db
                //path to db is specifed in Constants class
                this.database = new SQLiteConnection(Constants.DatabasePath);


                //replaces sql quary to create a table for User class
                this.database.CreateTable<User>();
            }

            public User GetUserByUsernameAndPassword(string username, string password)
            {
                // Attempt to retrieve the user with the provided username
                var user = this.database.Table<User>().FirstOrDefault(u => u.Username == username);

                // If a user is found and the password matches, return the user
                if (user != null && user.Password == password)
                {
                    return user;
                }

                // If no user is found or the password does not match, return null
                return null;
            }

            //add User 
            public void AddUser(User user)
            {
                this.database.Insert(user);
            }

            //Get User by name
            public User GetUserByName(string filter)
            {
                return this.database.Table<User>().FirstOrDefault(b => b.FirstName == filter);
        }

            // Find users by user type
            public List<User> GetUsersByUserType(int userType)
            {
                return this.database.Table<User>().Where(u => u.UserType == userType).ToList();
            }

            // Find users by username
            public List<User> GetUsersByUsername(string username)
            {
                return this.database.Table<User>().Where(u => u.Username.Equals(username)).ToList();
            }

            // Find users by email
            public List<User> GetUsersByEmail(string email)
            {
                return this.database.Table<User>().Where(u => u.Email.Equals(email)).ToList();
            }

            // Find users by phone number
            public List<User> GetUsersByPhoneNumber(string phoneNumber)
            {
                return this.database.Table<User>().Where(u => u.PhoneNumber.Equals(phoneNumber)).ToList();
            }

            //delete users 
            public void DeleteUser(int id)
            {
                this.database.Delete<User>(id);
            }

            //update a users by id
            public void UpdateUser(User user)
            {
                this.database.Update(user);
            }

            //Get a list of all Users in db
            public List<User> GetAllUsers()
            {
                return this.database.Table<User>().ToList();
            }

            public void DeleteAllUser()
            {
                database.DeleteAll<User>();
            }

        }
    }
