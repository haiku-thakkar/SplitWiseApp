using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondSplitWise.Repository;
using SecondSplitWise.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondSplitWise.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : Controller
    {
        IPaymentRepository _PaymentRepository;
        ILogger _Logger;

        public PaymentController(IPaymentRepository paymentRepo,ILoggerFactory loggerFactory)
        {
            _PaymentRepository = paymentRepo;
            _Logger = loggerFactory.CreateLogger(nameof(PaymentController));
        }

       [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<PaymentResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetPayment(int id)
        {

            try
            {
                var data = await _PaymentRepository.GetPaymentAsync(id);
                return Ok(data);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("group/{id}")]
        [ProducesResponseType(typeof(List<PaymentResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetGroupPayment(int id)
        {

            try
            {
                var data = await _PaymentRepository.GetGroupPaymentAsync(id);
                return Ok(data);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }

        [HttpGet("{uid}/{fid}")]
        [ProducesResponseType(typeof(List<PaymentResponse>), 200)]
        [ProducesResponseType(typeof(ApiGeneralResponse), 400)]
        public async Task<ActionResult> GetPayment(int uid, int fid)
        {

            try
            {
                var data = await _PaymentRepository.GetPaymentAsync(uid, fid);
                return Ok(data);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new ApiGeneralResponse { Status = false });
            }
        }
    }
}
