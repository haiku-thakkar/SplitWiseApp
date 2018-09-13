using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DataModel;
using SecondSplitWise.DBContext;
using SecondSplitWise.Model;
using SecondSplitWise.Response;

namespace SecondSplitWise.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly SecondSplitWiseContext _Context;
        private readonly ILogger _Logger;

        public ExpenseRepository(SecondSplitWiseContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("ExpenseRepository");
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var expense = await _Context.expense.SingleOrDefaultAsync(c => c.expenseID == id);
            _Context.Remove(expense);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteExpenseAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<List<ExpenseResponse>> GetAllExpensesAsync(int id)
        {
            List<ExpenseResponse> expenses = new List<ExpenseResponse>();
            var expenseData = _Context.expense.Where(c => c.expense_Members.Any(ex => ex.commonmemberID == id) || c.created_by == id || c.user_Expenses.Any(ex => ex.userexpenseID == id)).ToList();

            for (var i = 0; i < expenseData.Count; i++)
            {
                var exp = new ExpenseResponse();
                exp = await GetExpenseAsync(expenseData[i].expenseID);
                expenses.Add(exp);
            }
            return expenses;
        }

        public async Task<ExpenseResponse> GetExpenseAsync(int id)
        {
            ExpenseResponse expense = new ExpenseResponse();
            var payers = new List<ExpenseMemberResponse>();
            var members = new List<ExpenseMemberResponse>();

            var expData = _Context.expense.SingleOrDefault(c => c.expenseID == id);
            expense.expenseID = expData.expenseID;
            expense.expenseName = expData.expenseName;
            expense.created_at = expData.created_at;
            expense.groupID = expData.groupID.GetValueOrDefault();

            if (expense.groupID != 0)
            {
                var groupname = _Context.group.SingleOrDefault(c => c.groupID == expData.groupID);
                expense.group_name = groupname.group_name;
            }
            var name = _Context.user.SingleOrDefault(c => c.userID == expData.created_by);
            expense.createdBy = name.first_name;

            var data = _Context.expense_member.Where(c => c.expenseID == id).ToList();
            for (var i = 0; i < data.Count; i++)
            {
                var member = _Context.user.SingleOrDefault(c => c.userID == data[i].commonmemberID);
                members.Add(new ExpenseMemberResponse(member.userID, member.first_name, data[i].payableAmount));
            }

            var payer = _Context.user_expense.Where(c => c.expenseID == id).ToList();
            for (var i = 0; i < payer.Count; i++)
            {
                var p = _Context.user.SingleOrDefault(c => c.userID == payer[i].user_expense_id);
                payers.Add(new ExpenseMemberResponse(p.userID, p.first_name, payer[i].paid_share));

            }

            expense.payers = payers;
            expense.expenseMembers = members;

            return expense;
        }

        public async Task<List<ExpenseResponse>> GetGroupExpensesAsync(int groupID)
        {
            List<ExpenseResponse> expenses = new List<ExpenseResponse>();
            var expData = _Context.expense.Where(c => c.groupID == groupID).ToList();
            for (var i = 0; i < expData.Count; i++)
            {
                var exp = new ExpenseResponse();
                exp = await GetExpenseAsync(expData[i].expenseID);
                expenses.Add(exp);
            }
            return expenses;
        }

        public async Task<List<ExpenseResponse>> GetIndividualExpenses(int Userid, int Friendid)
        {
            List<ExpenseResponse> expenses = new List<ExpenseResponse>();
            var expData = _Context.expense.Where(c => c.expense_Members.Any(aa => aa.commonmemberID == Friendid || aa.commonmemberID == Userid) && c.user_Expenses.Any(aa => aa.userexpenseID == Userid || aa.userexpenseID == Friendid)).ToList();
            for (var i = 0; i < expData.Count; i++)
            {
                var exp = new ExpenseResponse();
                exp = await GetExpenseAsync(expData[i].expenseID);
                expenses.Add(exp);
            }

            return expenses;
        }

        public async Task<expense> InsertExpenseAsync(expenseModel expense)
        {
            expense newExpense = new expense();
            newExpense.expenseName = expense.expenseName;
            newExpense.created_by = expense.created_by;
            newExpense.created_at = expense.created_at;
            newExpense.groupID = expense.groupID;
            _Context.expense.Add(newExpense);

            foreach (var person in expense.payer)
            {
                user_expense user_Expense = new user_expense();
                user_Expense.expenseID = newExpense.expenseID;
                user_Expense.user_expense_id = person.ID;
                user_Expense.paid_share = person.amount;
                _Context.user_expense.Add(user_Expense);
            }

            foreach (var person in expense.commonmember)
            {
                expense_member member = new expense_member();
                member.expenseID = newExpense.expenseID;
                member.commonmemberID = person.ID;
                member.payableAmount = person.amount;
                _Context.expense_member.Add(member);
            }

            foreach (var data in expense.paymentModels)
            {
                if (data.groupID == null)
                {
                    var settle = await _Context.payment.SingleOrDefaultAsync(c => c.payerID == data.paymentID && c.commonmemberID == data.commonmemberID && c.groupID == null);
                    if (settle != null)
                    {
                        settle.payment_amount = settle.payment_amount + data.payment_amount;
                        _Context.payment.Attach(settle);
                    }
                    else
                    {
                        var setle = await _Context.payment.SingleOrDefaultAsync(c => c.payerID == data.commonmemberID && c.commonmemberID == data.paymentID && c.groupID == null);
                        if (setle != null)
                        {
                            setle.payment_amount = setle.payment_amount - data.payment_amount;
                            _Context.payment.Attach(setle);
                        }
                        else
                        {
                            var newSettle = new payment();
                            newSettle.payerID = data.paymentID;
                            newSettle.commonmemberID = data.commonmemberID;
                            newSettle.payment_amount = data.payment_amount;
                            _Context.payment.Add(newSettle);
                        }
                    }
                }
                else
                {
                    var settle = await _Context.payment.SingleOrDefaultAsync(c => c.payerID == data.paymentID && c.commonmemberID == data.commonmemberID && c.groupID == data.groupID);
                    if (settle != null)
                    {
                        settle.payment_amount = settle.payment_amount + data.payment_amount;
                        _Context.payment.Attach(settle);
                    }
                    else
                    {
                        var setle = await _Context.payment.SingleOrDefaultAsync(c => c.payerID == data.commonmemberID && c.commonmemberID == data.paymentID && c.groupID == data.groupID);
                        if (setle != null)
                        {
                            setle.payment_amount = setle.payment_amount - data.payment_amount;
                            _Context.payment.Attach(setle);
                        }
                        else
                        {
                            var newSettle = new payment();
                            newSettle.payerID = data.paymentID;
                            newSettle.commonmemberID = data.commonmemberID;
                            newSettle.payment_amount = data.payment_amount;
                            newSettle.groupID = data.groupID;
                            _Context.payment.Add(newSettle);
                        }
                    }
                }
            }
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception exp)
            {

                _Logger.LogError($"Error in {nameof(InsertExpenseAsync)}: " + exp.Message);
            }
            return newExpense;
        }

        public async Task<bool> UpdateExpenseAsync(expense expense)
        {
            _Context.expense.Attach(expense);
            _Context.Entry(expense).State = EntityState.Modified;
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {

                _Logger.LogError($"Error in {nameof(UpdateExpenseAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
