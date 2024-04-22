using System;

namespace FinalProjectMaui
{
    internal class Constants
    {
        //name of db is made constant 
        public const string DatabaseFilename = "Users.db3";

        public static string DatabasePath
        {
            get
            {
                // Gets the base directory of the application.
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                // Combines the base directory with the database filename.
                var databasePath = Path.Combine(baseDirectory, DatabaseFilename);
                return databasePath;
            }
        }

        public Constants()
        {
        }
    }
}
