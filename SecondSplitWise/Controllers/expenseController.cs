using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DataModel;
using SecondSplitWise.Model;
using SecondSplitWise.Repository;
using SecondSplitWise.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondSplitWise.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class expenseController : Controller
    {
        IExpenseRepository _ExpenseRepository;
        ILogger _Logger;

        public expenseController(IExpenseRepository expenseReop,ILoggerFactory loggerFactory)
        {
            _ExpenseRepository = expenseReop;
            _Logger = loggerFactory.CreateLogger(nameof(expenseController));
        }

        // GET: /<controller>/
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExpenseResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> OneExpense(int id)
        {
            try
            {
                var exp = await _ExpenseRepository.GetExpenseAsync(id);
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
                
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiGeneralResponse), 201)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> CreateExpense([FromBody]expenseModel expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var newExpense = await _ExpenseRepository.InsertExpenseAsync(expense);
                if (newExpense == null)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return CreatedAtRoute("GetExpenseRoute", new { id = newExpense.expenseID },
                        new ApiGeneralResponse { Status = true, id = newExpense.expenseID });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> UpdateExpense(int id,[FromBody]expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var status = await _ExpenseRepository.UpdateExpenseAsync(expense);
                if (!status)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return Ok(new ApiGeneralResponse { Status = true, id = expense.expenseID });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("allexpense/{id}")]
        [ProducesResponseType(typeof(List<ExpenseResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult>AllExpenses(int id)
        {
            try
            {
                var expense = await _ExpenseRepository.GetAllExpensesAsync(id);
                return Ok(expense);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("all/{Userid}/{Friendid}")]
        [ProducesResponseType(typeof(List<ExpenseResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult>IndividualExpense(int Userid,int Friendid)
        {
            try
            {
                var expense = await _ExpenseRepository.GetIndividualExpenses(Userid, Friendid);
                return Ok(expense);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("all/{Groupid}")]
        [ProducesResponseType(typeof(List<ExpenseResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult>GroupExpense(int Groupid)
        {
            try
            {
                var expense = await _ExpenseRepository.GetGroupExpensesAsync(Groupid);
                return Ok(expense);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiGeneralResponse), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult>DeleteExpense(int id)
        {
            try
            {
                var status = await _ExpenseRepository.DeleteExpenseAsync(id);
                if (!status)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return Ok(new ApiGeneralResponse { Status = true, id = id });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }
    }
}
