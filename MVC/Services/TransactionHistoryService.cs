using DTO;
using MVC.Services.Interfaces;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;

namespace MVC.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public TransactionHistoryService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WebAPI:BaseUrl"] + "/TransactionHistories";
        }

        public async Task<IEnumerable<TransactionHistoryDTO>?> GetAllTransactionHistories()
        {
            return await SQS.GetAll<TransactionHistoryDTO>(_client, _baseUrl);
        }

        public async Task<TransactionHistoryDTO?> GetTransactionHistoryById(int id)
        {
            return await SQS.Get<TransactionHistoryDTO>(_client, $"{_baseUrl}/{id}");
        }
    }
}
