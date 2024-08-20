using RegisterOfIS.Interfaces;
using RegisterOfIS.Models;
using RegisterOfIS.Database;
using Attribute = RegisterOfIS.Models.Attribute;
using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Requests;
using System.Diagnostics.CodeAnalysis;
using LinqKit;
using Microsoft.IdentityModel.Tokens;

namespace RegisterOfIS.Services
{
    public class InfSystemService(PostgresDbContext dbContext) : IInfSystemService
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        private readonly AttributeService _attributeService = new(dbContext);


        private async Task<bool> ExistsById(int id)
        {
            return await _dbContext.InfSystems.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync() != null;
        }

        private async Task<bool> ExistsByName(string name)
        {
            return await _dbContext.InfSystems.Where(a => a.Name.Equals(name)).FirstOrDefaultAsync() != null;
        }

        public bool IsValidFixedAttributeValue(int id, string Value)
        {
            return _dbContext.AttributeDict
                .Where(d => d.AttributeId.Equals(id))
                .Select(d => d.Value)
                .Contains(Value);
        }

        public InfSystem? Find([NotNull] int id)
        {
            return _dbContext.InfSystems
                .Include(infs => infs.AttributeStates)
                .Where(infs => infs.Id.Equals(id))
                .FirstOrDefault();
        }

        public InfSystem Get(int id)
        {
            return Find(id) ?? throw new ArgumentNullException();
        }

        public async Task<List<InfSystem>> Search(SearchInfSystemsForm form)
        {
            var predicate = PredicateBuilder.New<InfSystem>(true);

            var result = _dbContext.InfSystems
                .AsNoTracking()
                .AsExpandable();

            if (!form.Name.IsNullOrEmpty())
            {
                predicate.And(val => val.Name.StartsWith(form.Name));
            }
            if (!form.Attr.IsNullOrEmpty())
            {
                predicate.And(val => val.Attributes.Select(a => a.Name).Contains(form.Attr));
            }
            return await result.Where(predicate).ToListAsync();

        }

        public async Task<InfSystem?> Create(CreateInfSystemRequest data)
        {
            if (await ExistsByName(data.Name)) return null;

            else
            {
                InfSystem infSystem = new InfSystem(data.Name, data.Description);
                await _dbContext.AddAsync(infSystem);
                var attrs = _dbContext.Attributes.Where(a => data.attributes.Keys.Contains(a.Name));
                infSystem.AttributeStates = new List<AttributeState>();

                string value;
                foreach (var attr in attrs)
                {
                    value = data.attributes[attr.Name];
                    if (!attr.IsFixed)
                    {
                        infSystem.AttributeStates.Add(new AttributeState(attr, infSystem, value));
                    }
                    else
                    {
                        if (IsValidFixedAttributeValue(attr.Id, value))
                        {
                            infSystem.AttributeStates.Add(new AttributeState(attr, infSystem, value));
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
                return infSystem;
            }
        }

        public async Task<InfSystem?> Update(int id, ChangeInfSystemRequest data)
        {
            InfSystem infSystem = Get(id);

            infSystem.Name = data.Name;
            infSystem.Description = data.Description;
            await _dbContext.SaveChangesAsync();
            return infSystem;
        }

        public void Delete(int id)
        {
            _dbContext.InfSystems.Remove(Get(id));
        }

        public List<AttributeStateDto> GetAttrs(int id)
        {
            return _dbContext.AttributeStates.Include(st => st.Attribute).Where(st => st.InfSystemId.Equals(id)).ToList()
                .ConvertAll(st => new AttributeStateDto(st.Attribute.Name, st.Value));
        }

        public async Task<bool> AddAttrs(int id, Dictionary<int, string> data)
        {
            var infSystem = Get(id);
            var values = infSystem.Attributes.Select(a => a.Id);
            var vals = _dbContext.Attributes.Select(a => a.Id);

            List<AttributeState> states = data.Where(d => !values.Contains(d.Key) && vals.Contains(d.Key))
                .ToList()
                .ConvertAll(val => new AttributeState(val.Key, infSystem.Id, val.Value));
            ((List<AttributeState>)infSystem.AttributeStates).AddRange(states);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAttrs(int id, List<int> data)
        {
            var infSystem = Get(id);

            bool result = ((List<AttributeState>)infSystem.AttributeStates).RemoveAll(st => data.Contains(st.AttributeId)) >= 1;
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}