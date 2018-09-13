using SecondSplitWise.DataModel;
using SecondSplitWise.Model;
using SecondSplitWise.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Repository
{
    public interface IExpenseRepository
    {
        Task<ExpenseResponse> GetExpenseAsync(int id);

        Task<expense> InsertExpenseAsync(expenseModel expense);

        Task<bool> UpdateExpenseAsync(expense expense);
        Task<bool> DeleteExpenseAsync(int id);
        Task<List<ExpenseResponse>> GetAllExpensesAsync(int id);
        Task<List<ExpenseResponse>> GetIndividualExpenses(int Userid, int Friendid);
        Task<List<ExpenseResponse>> GetGroupExpensesAsync(int Groupid);
    }
}
