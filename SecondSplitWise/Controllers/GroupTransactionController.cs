using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondSplitWise.Model;
using SecondSplitWise.Repository;
using SecondSplitWise.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondSplitWise.Controllers
{
    [Route("api/grpTransaction")]
    [ApiController]
    public class GroupTransactionController : Controller
    {
        IGroupTransactionRepository _GroupTransactionRepository;
        ILogger _Logger;
        public GroupTransactionController(IGroupTransactionRepository groupTransactionRepo,ILoggerFactory loggerFactory) 
        {
            _GroupTransactionRepository = groupTransactionRepo;
            _Logger = loggerFactory.CreateLogger(nameof(GroupTransactionController));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<GroupTransactionResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetTransaction(int id) 
        {
            try
            {
                var trans = await _GroupTransactionRepository.GetGroupTransactionsAsync(id);
                return Ok(trans);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("all/{Groupid}")]
        [ProducesResponseType(typeof(List<GroupTransactionResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetGroupTransactions(int Groupid)
        {

            try
            {
                var transactions = await _GroupTransactionRepository.GetGroupTransactionsAsync(Groupid);
                return Ok(transactions);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse{ Status = false });
            }
        }

        [HttpGet("all/{Userid}/{Friendid}")]
        [ProducesResponseType(typeof(List<GroupTransactionResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetIndividualTransactions(int Userid, int Friendid)
        {

            try
            {
                var transactions = await _GroupTransactionRepository.GetIndividualTransactionsAsync(Userid, Friendid);
                return Ok(transactions);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("alltrans/{Userid}")]
        [ProducesResponseType(typeof(List<GroupTransactionResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetAllTransactions(int Userid)
        {

            try
            {
                var transactions = await _GroupTransactionRepository.GetAllTransactionsAsync(Userid);
                return Ok(transactions);
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
        public async Task<ActionResult> RecordPayment([FromBody]group_transactions payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiGeneralResponse { Status = false });
            }

            try
            {
                var newTrans = await _GroupTransactionRepository.RecordPaymentAsync(payment);
                if (newTrans == null)
                {
                    return BadRequest(new ApiGeneralResponse { Status = false });
                }
                return CreatedAtAction("GetTransactionRoute", new { id = newTrans.grouptransactionID },
                            new ApiGeneralResponse { Status = true, id = newTrans.grouptransactionID });
                            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse{ Status = false });
            }
        }
    }
}
