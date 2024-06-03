using DTO;
using MVC.Services.Interfaces;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;

namespace MVC.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public TransactionService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WebAPI:BaseUrl"] + "/Transaction";
        }

        public Task<bool> PostTransaction(TransactionHistoryDTO transactionHistoryDTO)
        {
            return SQS.PostNoReturn(_client, $"{_baseUrl}/Account", transactionHistoryDTO);
        }
    }
}
