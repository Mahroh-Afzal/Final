using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace FinalProjectMaui
{
    public partial class BudgetManagerPage : ContentPage
    {
        private decimal budget = 0;
        private decimal totalExpenses = 0;
        private ObservableCollection<string> expenses = new ObservableCollection<string>();

        public BudgetManagerPage()
        {
            InitializeComponent();
        }

        private void AddExpense_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(expenseEntry.Text))
            {
                decimal expenseAmount = decimal.Parse(expenseEntry.Text);
                totalExpenses += expenseAmount;
                expenses.Add($"Expense: {expenseAmount}");

                UpdateUI(); // Update the UI after adding each expense
            }
        }

        private void UpdateUI()
        {
            expenseListView.ItemsSource = expenses;

            if (budget > 0)
            {
                // Calculate the spending percentage
                decimal spendingPercentage = (totalExpenses / budget) * 100;

                // Update the spending meter text
                spendingMeter.Text = $"Spent: {totalExpenses:C} ({spendingPercentage:F0}%) of Budget";

                // Update the warning label
                warningLabel.Text = totalExpenses > budget ? "Expenses exceed budget!" : "";
            }
        }

        private void BudgetEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(budgetEntry.Text))
            {
                budget = decimal.Parse(budgetEntry.Text);
            }
            else
            {
                budget = 0; // Reset budget if entry is empty or invalid
            }
            UpdateUI(); // Update the UI whenever budget changes
        }
    }
}
