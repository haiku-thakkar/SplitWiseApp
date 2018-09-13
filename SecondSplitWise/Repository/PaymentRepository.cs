using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DBContext;
using SecondSplitWise.Response;

namespace SecondSplitWise.Repository

    
{
    public class PaymentRepository : IPaymentRepository
    {
    private readonly SecondSplitWiseContext _Context;
    private readonly ILogger _Logger;

    public PaymentRepository(SecondSplitWiseContext context, ILoggerFactory loggerFactory)
    {
        _Context = context;
        _Logger = loggerFactory.CreateLogger("PaymentRepository");
    }

    public async Task<List<PaymentResponse>> GetGroupPaymentAsync(int Groupid)
    {
        List<PaymentResponse> settlements = new List<PaymentResponse>();
        var sData = await _Context.payment.Where(c => c.groupID == Groupid).ToListAsync();

        for (var i = 0; i < sData.Count; i++)
        {
            var settle = new PaymentResponse();
            settle = await GetPaymentAsync(sData[i].paymentID);
            settlements.Add(settle);
        }

        return settlements;
    }

    public async Task<PaymentResponse> GetPaymentAsync(int id)
    {
        PaymentResponse payments = new PaymentResponse();

        var sData = await _Context.payment.SingleOrDefaultAsync(c => c.paymentID == id);
        payments.ID = sData.paymentID;
        payments.PayreID = (int)sData.payerID;
        payments.ReceiverID = (int)sData.commonmemberID;

        if (sData.groupID != null)
        {
            payments.groupID = sData.groupID.GetValueOrDefault();
        }

        payments.amount = sData.payment_amount;

        return payments;
    }

    public async Task<List<PaymentResponse>> GetPaymentAsync(int Userid, int Friendid)
    {
        List<PaymentResponse> payments = new List<PaymentResponse>();
        var sData = await _Context.payment.Where(c => (c.payerID == Userid && c.commonmemberID == Friendid) || (c.payerID == Friendid && c.commonmemberID == Userid)).ToListAsync();

        for (var i = 0; i < sData.Count; i++)
        {
            var settle = new PaymentResponse();
            settle = await GetPaymentAsync(sData[i].paymentID);
            payments.Add(settle);
        }

        return payments;
    }
}
}
