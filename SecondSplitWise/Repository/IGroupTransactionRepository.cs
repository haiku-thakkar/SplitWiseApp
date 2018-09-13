using SecondSplitWise.Model;
using SecondSplitWise.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Repository
{
    public interface IGroupTransactionRepository
    {
        Task<GroupTransactionResponse> GetTransactionAsync(int id);
        Task<group_transactions> RecordPaymentAsync(group_transactions payment);
        Task<List<GroupTransactionResponse>> GetGroupTransactionsAsync(int Groupid);
        Task<List<GroupTransactionResponse>> GetIndividualTransactionsAsync(int Userid, int Friendid);
        Task<List<GroupTransactionResponse>> GetAllTransactionsAsync(int Userid);

    }
}
