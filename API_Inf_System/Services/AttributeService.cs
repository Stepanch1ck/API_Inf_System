using API_Inf_System.DBContext;
using API_Inf_System.IServices;
using API_Inf_System.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Inf_System.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly MyDbContext _context;

        public AttributeService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttributeDto>> GetAllAttributesAsync()
        {
            return await _context.Attributes
                .Select(a => new AttributeDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DataType = a.DataType
                })
                .ToListAsync();
        }

        public async Task<AttributeDto> GetAttributeByIdAsync(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null) return null;

            return new AttributeDto
            {
                Id = attribute.Id,
                Name = attribute.Name,
                DataType = attribute.DataType
            };
        }

        public async Task<DBContext.Attribute> CreateAttributeAsync(AttributeCreateDto attributeCreateDto)
        {
            var newAttribute = new DBContext.Attribute
            {
                Name = attributeCreateDto.Name,
                DataType = attributeCreateDto.DataType,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Attributes.Add(newAttribute);
            await _context.SaveChangesAsync();

            return newAttribute;
        }
        
        public async Task<bool> UpdateAttributeAsync(int id, AttributeDto attributeDto)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null) return false;

            attribute.Name = attributeDto.Name;
            attribute.DataType = attributeDto.DataType;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAttributeAsync(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null) return false;

            _context.Attributes.Remove(attribute);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AttributeDto> GetOrCreateAttributeByNameAsync(string attributeName, string dataType)
        {
            var existingAttribute = await _context.Attributes
                .FirstOrDefaultAsync(a => a.Name.ToLower() == attributeName.ToLower());

            if (existingAttribute != null)
            {
                return new AttributeDto
                {
                    Name = existingAttribute.Name,
                    DataType = existingAttribute.DataType
                };
            }

            var newAttribute = new DBContext.Attribute
            {
                Name = attributeName,
                DataType = dataType
            };

            _context.Attributes.Add(newAttribute);
            await _context.SaveChangesAsync();

            return new AttributeDto
            {
                Name = newAttribute.Name,
                DataType = newAttribute.DataType
            };
        }

    }
}
