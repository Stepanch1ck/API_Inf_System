using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Core.Models;
using RegisterOfInformationSystems.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = RegisterOfInformationSystems.Core.Models.Attribute;

namespace RegisterOfInformationSystems.Service
{
    internal class InformationSystemService(PostgresDbContext dbContext) : IActionsOnTheInformationSystem
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        public async Task<bool> CreateInformationSystem(string name, string description, Dictionary<Core.Models.Attribute, string> attributes)
        {
            if(_dbContext.InfSystems.Where(infsys => infsys.Name.ToLower() == name.ToLower()).Any())
            {
                return false;
            }
            await _dbContext.InfSystems.AddAsync(new InformationSystem(name, description, attributes));
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInformationSystem(string name)
        {
            var infSystem = _dbContext.InfSystems.Where(infsys => infsys.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (infSystem == null)
            {
                return false;
            }
            _dbContext.InfSystems.Remove(infSystem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<InformationSystem>> GetInformationSystemsByAttributeContain(Attribute attribute)
        {
            var infSystems = _dbContext.InfSystems.Where(sys => sys.Attributes.Keys.Contains(attribute));
            if(infSystems.FirstOrDefault() == null)
            {
                return null;
            }
            return infSystems.ToList();
        }

        public Task<List<InformationSystem>> GetInformationSystemsByAttributeContain(string state)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InformationSystem>> GetInformationSystemsByPartOfName(string partOfName)
        {
            var infSystems = _dbContext.InfSystems.Where(sys => sys.Name.ToLower().Contains(partOfName.ToLower()));
            if (infSystems.FirstOrDefault() == null)
            {
                return null;
            }
            return infSystems.ToList();
        }

        public async Task<bool> SetAttributeToInformationalSystem(string name, Attribute attribute, string state)
        {
            var infSystem = _dbContext.InfSystems.Where(infsys => infsys.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (infSystem == null)
            {
                return false;
            }
            infSystem.Attributes[attribute] = state;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateInformationSystem(string name, Dictionary<Core.Models.Attribute, string> attributes)
        {
            var infSystem = _dbContext.InfSystems.Where(infsys => infsys.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (infSystem == null)
            {
                return false;
            }
            infSystem.Attributes = attributes;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
