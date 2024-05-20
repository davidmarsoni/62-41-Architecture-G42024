using DTO;

namespace MVC.Services.Interfaces
{
    public interface IConversionService
    {
        public Task<ConversionDTO?> GetConversionById(int id);
        public Task<IEnumerable<ConversionDTO>?> GetAllConversions();
        public Task<ConversionDTO?> CreateConversion(ConversionDTO conversionDTO);
        public Task<Boolean> UpdateConversion(ConversionDTO conversionDTO);
        public Task<Boolean> DeleteConversion(int id);
    }
}
