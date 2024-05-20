using DTO;
using MVC.Services.Interfaces;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;

namespace MVC.Services
{
    public class ConversionService : IConversionService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://localhost:7176/api/conversions";

        public ConversionService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ConversionDTO?> GetConversionById(int id)
        {
            return await SQS.Get<ConversionDTO>(_client, $"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<ConversionDTO>?> GetAllConversions()
        {
            return await SQS.GetAll<ConversionDTO>(_client, $"{_baseUrl}");
        }

        public async Task<ConversionDTO?> CreateConversion(ConversionDTO conversionDTO)
        {
            return await SQS.Post<ConversionDTO?>(_client, _baseUrl, conversionDTO);
        }

        public async Task<Boolean> UpdateConversion(ConversionDTO conversionDTO)
        {
            return await SQS.PutNoReturn(_client, $"{_baseUrl}/{conversionDTO.ConversionId}", conversionDTO);
        }

        public async Task<Boolean> DeleteConversion(int id)
        {
            return await SQS.Delete(_client, $"{_baseUrl}/{id}") != null;
        }
    }
}
