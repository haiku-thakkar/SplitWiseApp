using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SecondSplitWise.DBContext;
using SecondSplitWise.Model;
using SecondSplitWise.Response;

namespace SecondSplitWise.Repository
{
    public class GroupTransactionRepository : IGroupTransactionRepository
    {
        private readonly SecondSplitWiseContext _Context;
        private ILogger _Logger;

        public GroupTransactionRepository(SecondSplitWiseContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("GroupTransactionRepository");

        }


        public async Task<List<GroupTransactionResponse>> GetAllTransactionsAsync(int Userid)
        {
            List<GroupTransactionResponse> transactions = new List<GroupTransactionResponse>();

            var tData = _Context.group_transaction.Where(c => (c.grouptransPayerID == Userid || c.grouptransReceiverID == Userid) || c.groupsID.members.Any(aa => aa.userID == Userid)).ToList();
            for (var i = 0; i < tData.Count; i++)
            {
                var trans = new GroupTransactionResponse();
                trans = await GetTransactionAsync(tData[i].grouptransactionID);
                transactions.Add(trans);
            }
            return transactions;
        }

        public async Task<List<GroupTransactionResponse>> GetGroupTransactionsAsync(int Groupid)
        {
            List<GroupTransactionResponse> transactions = new List<GroupTransactionResponse>();

            var tData = _Context.group_transaction.Where(c => c.groupID == Groupid).ToList();
            for (var i = 0; i < tData.Count; i++)
            {
                var trans = new GroupTransactionResponse();
                trans = await GetTransactionAsync(tData[i].grouptransactionID);
                transactions.Add(trans);
            }

            return transactions;
        }

        public async Task<List<GroupTransactionResponse>> GetIndividualTransactionsAsync(int Userid, int Friendid)
        {
            List<GroupTransactionResponse> transactions = new List<GroupTransactionResponse>();

            var tData = _Context.group_transaction.Where(c => (c.grouptransPayerID == Userid || c.grouptransPayerID == Friendid) && (c.grouptransReceiverID == Userid || c.grouptransReceiverID == Friendid)).ToList();
            for (var i = 0; i < tData.Count; i++)
            {
                var trans = new GroupTransactionResponse();
                trans = await GetTransactionAsync(tData[i].grouptransactionID);
                transactions.Add(trans);
            }
            return transactions;
        }

        public async Task<GroupTransactionResponse> GetTransactionAsync(int id)
        {
            GroupTransactionResponse transaction = new GroupTransactionResponse();
            var payer = new MemberResponse();
            var receiver = new MemberResponse();

            var tData = _Context.group_transaction.SingleOrDefault(c => c.grouptransactionID == id);
            var payerData = _Context.user.SingleOrDefault(c => c.userID == tData.grouptransPayerID);
            payer.ID = payerData.userID;
            payer.Name = payerData.first_name;

            var recData = _Context.user.SingleOrDefault(c => c.userID == tData.grouptransReceiverID);
            receiver.ID = recData.userID;
            receiver.Name = recData.first_name;

            transaction.ID = tData.grouptransactionID;
            transaction.payer = payer;
            transaction.receiver = receiver;
            transaction.groupID = tData.groupID.GetValueOrDefault();

            if (transaction.groupID != 0)
            {
                var name = _Context.group.SingleOrDefault(c => c.groupID == transaction.groupID);
                transaction.groupName = name.group_name;
            }

            transaction.paid_share = tData.paid_share;
            transaction.created_at = tData.created_at;
            return transaction;
        }

        public async Task<group_transactions> RecordPaymentAsync(group_transactions payment)
        {
            _Context.group_transaction.Add(payment);

            if (payment.groupID == null)
            {
                var settleData = _Context.payment.SingleOrDefault(c => c.payerID == payment.grouptransPayerID && c.commonmemberID == payment.grouptransReceiverID && c.groupID == null);
                if (settleData != null)
                {
                    settleData.payment_amount = settleData.payment_amount - payment.paid_share;
                    _Context.payment.Attach(settleData);
                }
                else
                {
                    var set = _Context.payment.SingleOrDefault(c => c.payerID == payment.grouptransReceiverID && c.commonmemberID == payment.grouptransPayerID && c.groupID == null);
                    if (set != null)
                    {
                        set.payment_amount = set.payment_amount + payment.paid_share;
                        _Context.payment.Attach(set);
                    }
                    else
                    {
                        payment settle = new payment();
                        settle.payerID =(int) payment.grouptransReceiverID;
                        settle.commonmemberID= (int)payment.grouptransPayerID;
                        settle.payment_amount = payment.paid_share;
                        _Context.payment.Add(settle);
                    }
                }
            }
            else
            {
                var settleData = _Context.payment.SingleOrDefault(c => c.payerID == payment.grouptransPayerID && c.commonmemberID == payment.grouptransReceiverID && c.groupID == payment.groupID);
                if (settleData != null)
                {
                    settleData.payment_amount = settleData.payment_amount- payment.paid_share;
                    _Context.payment.Attach(settleData);
                }
                else
                {
                    var set = _Context.payment.SingleOrDefault(c => c.payerID == payment.grouptransReceiverID && c.commonmemberID == payment.grouptransPayerID && c.groupID == payment.groupID);
                    if (set != null)
                    {
                        set.payment_amount = set.payment_amount + payment.paid_share;
                        _Context.payment.Attach(set);
                    }
                    else
                    {
                        payment settle = new payment();
                        settle.commonmemberID = (int)payment.grouptransPayerID;
                        settle.payerID = (int)payment.grouptransReceiverID;
                        settle.groupID = payment.groupID;
                        settle.payment_amount = payment.paid_share;
                        _Context.payment.Add(settle);
                    }


                }
            }
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
                _Logger.LogError($"Error in {nameof(RecordPaymentAsync)}: " + exp.Message);
            }

            return payment;

        }
    }
}
