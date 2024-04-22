using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinalProjectMaui
{
    public partial class BudgetManagerPage : ContentPage
    {

        private float totalExpenses = 0;
        Budget Nullbudget = new Budget()
        {
            ID = 0,
            budgetMax = 0,
        };
        public BudgetManagerPage()
        {
            InitializeComponent();
            BudgetSQLManager budgetSQLManager = new BudgetSQLManager();
            Budget budget = budgetSQLManager.GetBudget();
            if (budget == null)
            {
                budgetSQLManager.AddBudget(Nullbudget);
                budget = budgetSQLManager.GetBudget();
            }
            budgetSQLManager.UpdateBudget(budget);
            BudgetText.Text = "$" + budgetSQLManager.GetAllBudgets().ToString();
        }

        private async void AddExpense_Clicked(object sender, EventArgs e)
        {
            BudgetSQLManager budgetSQLManager = new BudgetSQLManager();
            if (!string.IsNullOrWhiteSpace(expenseEntry.Text))
            {
                int.TryParse(expenseEntry.Text, out int number);
                totalExpenses += number;
                if (totalExpenses < budgetSQLManager.GetAllBudgets())
                {

                    BudgetText.Text = "$" + budgetSQLManager.GetAllBudgets().ToString();
                    UpdateUI(number); // Update the UI after adding each expense
                }
                else
                {
                    await DisplayAlert("OVER BUDGET", "", "Cancel");

                }
            }
        }
        private void UpdateBudgetMax(object sender, EventArgs e)
        {
            BudgetSQLManager budgetSQLManager = new BudgetSQLManager();
            int.TryParse(budgetEntry.Text, out int number);

            Budget budget = budgetSQLManager.GetBudget();
            
            budget.budgetMax = number;
            budgetSQLManager.UpdateBudget(budget);
           
            BudgetText.Text = "$" + budgetSQLManager.GetAllBudgets().ToString();
        }
        private async void UpdateUI(int expenses)
        {
            
            await DisplayAlert("Expense issued", "$" + expenses.ToString() + " charged", "OK");

            Sum.Text = "SUM: " + totalExpenses;
            

        }
        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

    }
}
