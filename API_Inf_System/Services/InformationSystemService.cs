using API_Inf_System.DBContext;
using API_Inf_System.DTO;
using API_Inf_System.IServices;
using API_Inf_System.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Inf_System.Services
{
    public class InformationSystemService : IInformationSystemService
    {
        private readonly MyDbContext _context;
        private readonly IAttributeService _attributeService;
        private readonly IAttributeValueService _attributeValueService;
        public InformationSystemService(MyDbContext context, IAttributeService attributeService, IAttributeValueService attributeValueService)
        {
            _context = context;
            _attributeService = attributeService;
            _attributeValueService = attributeValueService;
        }
        //public async Task<List<InformationSystemDto>> GetAllInformationSystemsAsync()
        //{
        //    var informationSystems = await _context.InformationSystems
        //.Include(isEntity => isEntity.AttributeValues)
        //.ThenInclude(av => av.Attribute)
        //.ToListAsync();

        //    var informationSystemDtos = informationSystems.Select(isEntity => new InformationSystemDto
        //    {
        //        Id = isEntity.Id,
        //        Name = isEntity.Name,
        //        Attributes = isEntity.AttributeValues
        //    .GroupBy(av => av.Attribute.Name)
        //    .Select(group => new AttributeDto
        //    {
        //        Id = group.First().AttributeId,
        //        Name = group.Key,
        //        DataType = group.First().Attribute.DataType,
        //    })
        //    .ToList()
        //    }).ToList();
        //    return informationSystemDtos;
        //}
        public async Task<InformationSystemDto> GetInformationSystemByIdAsync(int id)
        {
            var informationSystem = await _context.InformationSystems
                .Include(isEntity => isEntity.AttributeValues)
                .ThenInclude(av => av.Attribute)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (informationSystem == null)
            {
                return null;
            }

            var infoSystemDto = new InformationSystemDto
            {
                Id = informationSystem.Id,
                Name = informationSystem.Name,
                Attributes = informationSystem.AttributeValues
                    .GroupBy(av => av.Attribute.Name)
                    .Select(group => new AttributeDto
                    {
                        Name = group.Key,
                        DataType = group.First().Attribute.DataType,
                    })
                    .ToList()
            };

            return infoSystemDto;
        }
        public async Task<InformationSystem> CreateInformationSystemAsync(InformationSystemCreateDto informationSystemDto)
        {
            var newInformationSystem = new InformationSystem
            {
                Name = informationSystemDto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.InformationSystems.Add(newInformationSystem);
            await _context.SaveChangesAsync();

            foreach (var attributeDto in informationSystemDto.Attributes)
            {
                var attribute = await _attributeService.GetOrCreateAttributeByNameAsync(attributeDto.Name, attributeDto.DataType);

                if (attribute != null)
                {
                    await _attributeValueService.CreateAttributeValueAsync(newInformationSystem.Id, attribute.Id, attributeDto.DataType);
                }
            }

            await _context.SaveChangesAsync();

            return newInformationSystem;
        }
        //public async Task<bool> UpdateInformationSystemAsync(int id, InformationSystemUpdateDto informationSystemDto)
        //{
        //    var informationSystem = await _context.InformationSystems
        //.Include(iS => iS.AttributeValues)
        //.ThenInclude(av => av.Attribute)
        //.FirstOrDefaultAsync(iS => iS.Id == informationSystemDto.Id);
        //    if (informationSystem == null)
        //    {
        //        return false;
        //    }

        //    informationSystem.Name = informationSystemDto.Name;
        //    informationSystem.UpdatedAt = DateTime.Now;

        //    if (informationSystemDto.Attributes.Any())
        //    {
        //        foreach (var attributeDto in informationSystemDto.Attributes)
        //        {
        //            var attributeName = attributeDto.Name;
        //            var dataType = attributeDto.DataType;

        //            var attribute = await _attributeService.GetOrCreateAttributeByNameAsync(attributeName, dataType);
        //            if (attribute != null)
        //            {
        //                attribute.DataType = dataType;
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //    }
            

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> UpdateInformationSystemAttributesAsync(int id, InformationSystemAttributeUpdateDto informationSystemDto)
        //{
        //    var informationSystem = await _context.InformationSystems
        //        .Include(IS => IS.AttributeValues)
        //        .ThenInclude(av => av.Attribute)
        //        .FirstOrDefaultAsync(IS => IS.Id == id);

        //    if (informationSystem == null)
        //    {
        //        return false;
        //    }

        //    foreach (var attributePair in informationSystemDto.Attributes)
        //    {
        //        var attributeName = attributePair.Key;
        //        var newAttributeValues = attributePair.Value;

        //        var existingAttributeValues = informationSystem.AttributeValues
        //            .Where(av => av.Attribute.Name.ToLower() == attributeName.ToLower())
        //            .ToList();

        //        foreach (var existingAttributeValue in existingAttributeValues)
        //        {
        //            var matchingNewValue = newAttributeValues.FirstOrDefault(v => v == existingAttributeValue.Value);
        //            if (matchingNewValue != null)
        //            {
        //                newAttributeValues.Remove(matchingNewValue);
        //            }
        //            else
        //            {
        //                _context.AttributeValues.Remove(existingAttributeValue);
        //            }
        //        }
        //        foreach (var newAttributeValue in newAttributeValues)
        //        {
        //            var attributeDto = await _attributeService.GetOrCreateAttributeByNameAsync(attributeName, "string");
        //            if (attributeDto != null)
        //            {
        //                await _attributeValueService.CreateAttributeValueAsync(informationSystem.Id, attributeDto.Id, newAttributeValue);
        //            }
        //        }
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return false;
        //    }
        //}

        public async Task<bool> DeleteInformationSystemAsync(int id)
        {
            var informationSystem = await _context.InformationSystems.FindAsync(id);
            if (informationSystem == null)
            {
                return false;
            }

            _context.InformationSystems.Remove(informationSystem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<InformationSystemDetailsDto> GetInformationSystemDetailsAsync(int id)
        {
            var informationSystem = await _context.InformationSystems
                .Include(isEntity => isEntity.AttributeValues)
                .ThenInclude(av => av.Attribute)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (informationSystem == null)
            {
                return null;
            }

            var infoSystemDetailsDto = new InformationSystemDetailsDto
            {
                Id = informationSystem.Id,
                Name = informationSystem.Name,
                Attributes = informationSystem.AttributeValues
                    .Select(av => new AttributeWithValueDto
                    {
                        Id = av.AttributeId,
                        Name = av.Attribute.Name,
                        Value = av.Value
                    })
                    .ToList()
            };

            return infoSystemDetailsDto;
        }

        //public async Task<bool> EditInformationSystemAsync(int id, InformationSystemEditDto informationSystemDto)
        //{
        //    var informationSystem = await _context.InformationSystems
        //        .Include(iS => iS.AttributeValues)
        //        .ThenInclude(av => av.Attribute)
        //        .FirstOrDefaultAsync(iS => iS.Id == id);

        //    if (informationSystem == null)
        //    {
        //        return false;
        //    }

        //    informationSystem.Name = informationSystemDto.Name;
        //    informationSystem.UpdatedAt = DateTime.Now;

        //    _context.AttributeValues.RemoveRange(informationSystem.AttributeValues);

        //    foreach (var attributeDto in informationSystemDto.Attributes)
        //    {
        //        var attribute = await _attributeService.GetOrCreateAttributeByNameAsync(attributeDto.Name, "string"); 
        //        if (attribute != null)
        //        {
        //            await _attributeValueService.CreateAttributeValueAsync(informationSystem.Id, attribute.Id, attributeDto.Value);
        //        }
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return false;
        //    }
        //}
        public async Task<List<InformationSystemWithAttributesDto>> GetAllInformationSystemsWithAttributesAsync()
        {
            var informationSystems = await _context.InformationSystems
                .Include(isEntity => isEntity.AttributeValues)
                .ThenInclude(av => av.Attribute)
                .ToListAsync();

            return informationSystems.Select(isEntity => new InformationSystemWithAttributesDto
            {
                Id = isEntity.Id,
                Name = isEntity.Name,
                Attributes = isEntity.AttributeValues
                    .Select(av => new AttributeWithValueDto
                    {
                        Id = av.Attribute.Id,
                        Name = av.Attribute.Name,
                        Value = av.Value
                    })
                    .ToList()
            }).ToList();
        }
        //public async Task<InformationSystemCreateDto> CreateInformationSystemWithAttributesAsync(InformationSystemCreateDto informationSystemDto)
        //{
        //    var newInformationSystem = new InformationSystem
        //    {
        //        Name = informationSystemDto.Name,
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        AttributeValues = new List<AttributeValue>()
        //    };

        //    _context.InformationSystems.Add(newInformationSystem);
        //    await _context.SaveChangesAsync();

        //    foreach (var attributeDto in informationSystemDto.Attributes)
        //    {
        //        var attribute = await _attributeService.GetOrCreateAttributeByNameAsync(attributeDto.Name, "string");

        //        if (attribute == null) 
        //        {
        //            var newAttributeValue = await _attributeValueService.CreateAttributeValueAsync(
        //                newInformationSystem.Id,
        //                attribute.Id,
        //                attributeDto.Value
        //            );
        //            newInformationSystem.AttributeValues.Add(newAttributeValue);
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //    return new InformationSystemCreateDto
        //    {
        //        //Id = newInformationSystem.Id,
        //        Name = newInformationSystem.Name,
        //        Attributes = newInformationSystem.AttributeValues.Select(av => new AttributeWithValueDto
        //        {
        //            Id = av.AttributeId,
        //            Name = av.Attribute.Name,
        //            Value = av.Value
        //        }).ToList()
        //    };
        //}

        public async Task<bool> EditInformationSystemWithAttributesAsync(int id, InformationSystemEditDto informationSystemDto)
        {
            var informationSystem = await _context.InformationSystems
        .Include(iS => iS.AttributeValues)
        .FirstOrDefaultAsync(iS => iS.Id == id);
            if (informationSystem == null)
            {
                return false;
            }
            informationSystem.Name = informationSystemDto.Name;
            informationSystem.UpdatedAt = DateTime.Now;

            foreach (var attributeDto in informationSystemDto.Attributes)
            {
                var existingAttributeValue = informationSystem.AttributeValues.FirstOrDefault(av => av.AttributeId == attributeDto.Id);
                if (existingAttributeValue != null)
                {
                    existingAttributeValue.Value = attributeDto.Value;
                }
                else
                {
                    await _attributeValueService.CreateAttributeValueAsync(informationSystem.Id, attributeDto.Id, attributeDto.Value);
                }
                await _context.SaveChangesAsync();
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}
