using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Database;

namespace Services
{
    public class AttributeService(PostgresDbContext dbContext) : IActionsOnTheAttributes
    {
        private readonly PostgresDbContext _dbContext = dbContext;

        public async Task<bool> AddNewState(string name, string state)
        {
            var attribute = _dbContext.Attributes.Where(attribute => attribute.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (attribute == null)
            {
                return false;
            }
            attribute.States.Add(state);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateAttribute(string name, string description, List<string> states)
        {
            var attribute = _dbContext.Attributes.Where(attribute => attribute.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (attribute != null)
            {
                return false;
            }
            await _dbContext.Attributes.AddAsync(new RegisterOfInformationSystems.Core.Models.Attribute(name, description, states));
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public Task<bool> DeleteAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RegisterOfInformationSystems.Core.Models.Attribute>> GetAttributesByPartOfName(string partOfName)
        {
            var attributes = _dbContext.Attributes.Where(attribute => attribute.Name.ToLower().Contains(partOfName.ToLower()));
            if(attributes.FirstOrDefault() == null)
            {
                return null;
            }
            return attributes.ToList();
        }

        public async Task<List<RegisterOfInformationSystems.Core.Models.Attribute>> GetAttributesByStateContain(string state)
        {
            var attributes = _dbContext.Attributes.Where(attribute => attribute.States.Contains(state.ToLower()));
            if (attributes.FirstOrDefault() == null)
            {
                return null;
            }
            return attributes.ToList();
        }

        public async Task<bool> RemoveState(string name, string state)
        {
            var attribute = _dbContext.Attributes.Where(attribute => attribute.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (attribute == null)
            {
                return false;
            }
            else if (!attribute.States.Contains(state.ToLower()))
            {
                return false;
            }
            attribute.States.Remove(state);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
