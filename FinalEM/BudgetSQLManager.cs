using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectMaui
{

    public class Budget
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int budgetMax { get; set; }
    }
    public class BudgetSQLManager
    {
        //create an object for connection to db
        private SQLiteConnection database;

        public BudgetSQLManager()
        {
            //instantiating the connection object with the path to the db
            //path to db is specifed in Constants class
            this.database = new SQLiteConnection(Constants.DatabasePath);

            //replaces sql quary to create a table for Budget class
            this.database.CreateTable<Budget>();
        }

        //add Budget 
        public void AddBudget(Budget Budget)
        {
            this.database.Insert(Budget);
        }

        // Find Budgets 
        public Budget GetBudget()
        {
            return this.database.Table<Budget>().FirstOrDefault();
        }

        //delete Budgets 
        public void DeleteBudget(int id)
        {
            this.database.Delete<Budget>(id);
        }

        //update a Budgets by id
        public void UpdateBudget(Budget Budget)
        {
            this.database.Update(Budget);
        }

        //Get a list of all Budgets in db
        public float GetAllBudgets()
        {
            return this.database.Table<Budget>().Sum(b => b.budgetMax);
        }

        public void DeleteAllBudget()
        {
            database.DeleteAll<Budget>();
        }

    }
}
