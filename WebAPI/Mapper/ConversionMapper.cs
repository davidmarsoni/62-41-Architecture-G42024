using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class ConversionMapper
    {
        public static ConversionDTO toDTO (Conversion conversion)
        {
            ConversionDTO conversionDTO = new ConversionDTO();
            conversionDTO.ConversionId = conversion.Id;
            conversionDTO.ConversionName = conversion.Name;
            conversionDTO.ConversionValue = conversion.Value;
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
