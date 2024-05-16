using DTO;

namespace MVC.Services.Interfaces
{
    public interface IConversionService
    {
        public ConversionDTO GetConversion(int id);
    }
}
