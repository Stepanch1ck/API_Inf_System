using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Interfaces;
using RegisterOfIS.Models;
using RegisterOfIS.Database;
using Attribute = RegisterOfIS.Models.Attribute;
using Microsoft.IdentityModel.Tokens;
using RegisterOfIS.Requests;
using System.Diagnostics.CodeAnalysis;
using LinqKit;

namespace RegisterOfIS.Services
{
    public class AttributeService(PostgresDbContext dbContext) : IAttributeService
    {
        private readonly PostgresDbContext _dbContext = dbContext;

        private async Task<bool> ExistsById(int id)
        {
            return await _dbContext.Attributes.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync() != null;
        }

        private async Task<bool> ExistsByName(string name)
        {
            return await _dbContext.Attributes.Where(a => a.Name.Equals(name)).FirstOrDefaultAsync() != null;
        }

        public Attribute? Find([NotNull] int id)
        {
            return _dbContext.Attributes
                .Include(a => a.Dict)
                .Where(attr => attr.Id.Equals(id))
                .FirstOrDefault();
        }

        public Attribute Get(int id)
        {
            return Find(id) ?? throw new ArgumentNullException();
        }

        public async Task<List<Attribute>> Search(SearchAtrributesForm form)
        {
            var predicate = PredicateBuilder.New<Attribute>(true);

            var result = _dbContext.Attributes
                .AsNoTracking()
                .AsExpandable();

            if (!form.Name.IsNullOrEmpty())
            {
                predicate.And(a => a.Name.StartsWith(form.Name));
            }
            if (!form.State.IsNullOrEmpty())
            {
                predicate.And(a => a.Dict.Select(d => d.Value).Contains(form.State));
            }
            return await result.Where(predicate).ToListAsync();
        }

        public async Task<Attribute?> Create(CreateAttributeRequest data)
        {
            if (await ExistsByName(data.Name)) return null;


            else
            {
                Attribute attribute = new Attribute(data.Name, data.Description, data.IsFixed);
                await _dbContext.Attributes.AddAsync(attribute);

                List<AttributeDict> dict = data.States.ConvertAll(s => new AttributeDict(attribute, s));
                attribute.Dict = dict;
                await _dbContext.SaveChangesAsync();
                return attribute;
            }
        }

        public async Task<Attribute?> Update(int id, ChangeAttributeRequest data)
        {
            Attribute attribute = Get(id);

            attribute.Name = data.Name;
            attribute.Description = data.Description;
            attribute.IsFixed = data.IsFixed;
            await _dbContext.SaveChangesAsync();
            return attribute;
        } 

        public void Delete(int id)
        {
            _dbContext.Attributes.Remove(Get(id));
        }

        public List<string> GetDict(int id)
        {
            return Get(id).Dict.Select(d => d.Value).ToList();
        }

        public async Task<bool> AddDict(int id, List<string> data)
        {
            var attribute = Get(id);
            var values = attribute.Dict.Select(st => st.Value);
            List<AttributeDict> dict = data.Where(d => !values.Contains(d))
                .ToList()
                .ConvertAll(d => new AttributeDict(attribute, d));
            ((List<AttributeDict>)attribute.Dict).AddRange(dict);
            
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveDict(int id, List<string> data)
        {
            var attribute = Get(id);
            
            bool result = ((List<AttributeDict>)attribute.Dict).RemoveAll(d => data.Contains(d.Value)) >= 1;
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
