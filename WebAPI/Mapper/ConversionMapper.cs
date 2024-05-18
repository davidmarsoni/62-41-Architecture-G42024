using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class ConversionMapper
    {
        public static ConversionDTO toDTO (Conversion conversion)
        {
            ConversionDTO conversionDTO = new ConversionDTO
            {
                ConversionId = conversion.Id,
                ConversionName = conversion.Name,
                ConversionValue = conversion.Value
            };
            return conversionDTO;
        }

        public static Conversion toDAL(ConversionDTO conversionDTO)
        {
            Conversion conversion = new Conversion
            {
                Id = conversionDTO.ConversionId,
                Name = conversionDTO.ConversionName,
                Value = conversionDTO.ConversionValue
            };
            return conversion;
        }
    }
}
