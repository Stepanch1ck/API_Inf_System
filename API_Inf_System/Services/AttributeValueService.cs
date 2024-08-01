using API_Inf_System.DBContext;
using API_Inf_System.IServices;
using API_Inf_System.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Inf_System.Services
{
    public class AttributeValueService : IAttributeValueService
    {
        private readonly MyDbContext _context;

        public AttributeValueService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttributeValueDto>> GetAllAttributeValuesAsync()
        {
            var attributeValues = await _context.AttributeValues
        .Include(av => av.Attribute) 
        .ToListAsync();

            var attributeValueDtos = attributeValues.Select(av => new AttributeValueDto
            {
                Id = av.Id,
                InformationSystemId = av.InformationSystemId,
                Attribute = new AttributeDto
                {
                    Id = av.Attribute.Id,
                    Name = av.Attribute.Name,
                    DataType = av.Attribute.DataType,
                },
                Value = av.Value
            }).ToList();

            return attributeValueDtos;
        }

        public async Task<AttributeValue> GetAttributeValueByIdAsync(int id)
        {
            return await _context.AttributeValues.FindAsync(id);
        }

        public async Task<AttributeValue> CreateAttributeValueAsync(int informationSystemId, int attributeId, string value)
        {
            var newAttributeValue = new AttributeValue
            {
                InformationSystemId = informationSystemId,
                AttributeId = attributeId,
                Value = value
            };

            _context.AttributeValues.Add(newAttributeValue);
            await _context.SaveChangesAsync();

            return newAttributeValue;
        }

        public async Task<bool> UpdateAttributeValueAsync(int id, int informationSystemId, int attributeId, string value)
        {
            var attributeValue = await _context.AttributeValues.FindAsync(id);
            if (attributeValue == null) return false;

            attributeValue.InformationSystemId = informationSystemId;
            attributeValue.AttributeId = attributeId;
            attributeValue.Value = value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAttributeValueAsync(int id)
        {
            var attributeValue = await _context.AttributeValues.FindAsync(id);
            if (attributeValue == null) return false;

            _context.AttributeValues.Remove(attributeValue);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
