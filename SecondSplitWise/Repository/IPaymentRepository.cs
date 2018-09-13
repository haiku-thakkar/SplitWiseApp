using SecondSplitWise.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondSplitWise.Repository
{
    public interface IPaymentRepository
    {
        Task<PaymentResponse> GetPaymentAsync(int id);
        Task<List<PaymentResponse>> GetPaymentAsync(int Userid, int Friendid);
        Task<List<PaymentResponse>> GetGroupPaymentAsync(int Groupid);
    }
}
