using RegisterOfIS.Interfaces;
using RegisterOfIS.Models;
using RegisterOfIS.Database;
using RegisterOfIS.Services;
using Attribute = RegisterOfIS.Models.Attribute;
using Microsoft.EntityFrameworkCore;

namespace RegisterOfIS.Services
{
    public class InfSystemService(PostgresDbContext dbContext) : IInfSystemService
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        private readonly AttributeService _attributeService = new(dbContext);
        public async Task<bool> CreateInformationSystem(string name, string description, List<string> attributeNames, List<string> attributeValues)
        {
            #region Вернуть false если уже есть ИС
            if (await GetInformationSystemByFullName(name) != null) return false;
            #endregion

            #region Создание временной ИС
            var tempInfSystem = new InfSystem();
            tempInfSystem.Name = name;
            tempInfSystem.Description = description;
            #endregion

            #region Заполнить ИС аттрибутами
            for (int i = 0;i<attributeNames.Count;i++)
            {
                var tempAttr = await _attributeService.GetAttributeByFullName(attributeNames[i]);
                tempInfSystem.Attributes.Add(tempAttr);
                if(tempAttr.States.Where(st => st.Value == attributeValues[i]).FirstOrDefault() == null)
                {
                    _attributeService.AddNewState(attributeNames[i], attributeValues[i]);
                }
                tempInfSystem.AttributeStatements.Add(tempAttr.States.Where(st => st.Value == attributeValues[i]).FirstOrDefault());
            }
            #endregion

            #region Добавить ИС в БД
            await _dbContext.InfSystems.AddAsync(tempInfSystem);
            await _dbContext.SaveChangesAsync();
            return true;
            #endregion
        }

        public async Task<bool> DeleteInformationSystem(string name)
        {
            #region Найти ИС по Названию
            var infSystem = await GetInformationSystemByFullName(name);
            #endregion

            #region Вернуть false если нет такой ИС
            if (infSystem == null) return false;
            #endregion

            #region Удалить ИС из бд
            _dbContext.InfSystems.Remove(infSystem);
            await _dbContext.SaveChangesAsync();
            return true;
            #endregion
        }

        public async Task<List<InfSystem>> GetInformationSystemsByAttributeContain(string attribute)
        {
            #region найти аттрибут для поиска
            var tempattr = await _attributeService.GetAttributeByFullName(attribute);
            #endregion
            if (tempattr != null)
            {
                #region найти ИС с этим аттрибутом
                var infSystems = _dbContext.InfSystems.Include(iS => iS.AttributeStatements).Include(iS => iS.Attributes).Where(sys => sys.Attributes.Contains(tempattr));
                #endregion

                #region Вернуть null если таких нет
                if (infSystems.FirstOrDefault() == null) return null;
                #endregion

                #region Вернуть список ИС
                return [.. infSystems];
                #endregion
            }
            return null;
        }

        public async Task<List<InfSystem>> GetInformationSystemsByPartOfName(string partOfName)
        {
            #region Вернуть все если нет фильтра
            if (partOfName == null) return [.. _dbContext.InfSystems.Include(iS => iS.AttributeStatements).Include(iS => iS.Attributes)];
            #endregion

            #region Найти все ИС по поисковому запросу
            var infSystems = _dbContext.InfSystems.Include(iS => iS.AttributeStatements).Where(sys => sys.Name.Contains(partOfName));
            #endregion

            #region Вернуть список ИС
            return [.. infSystems];
            #endregion
        }

        public async Task<bool> SetAttributeToInformationalSystem(string name, string attribute, string state)
        {
            var infSystem = await GetInformationSystemByFullName(name);
            if (infSystem == null) return false;
            var tempAttrState = infSystem.AttributeStatements.Where(aS => aS.Attribute.Name.Equals(attribute)).FirstOrDefault();
            if (tempAttrState != null)
            { 
                infSystem.AttributeStatements.Remove(tempAttrState); 
            }
            var tempAttr = await _attributeService.GetAttributeByFullName(attribute);
            if (tempAttr.States.Where(st => st.Value.Equals(state)).FirstOrDefault() == null)
            {
                if(!(await _attributeService.AddNewState(attribute, state)))
                {
                    return false;
                }
                infSystem.AttributeStatements.Add(tempAttr.States.Where(aS => aS.Value.Equals(state)).FirstOrDefault());
                return true;
            }
            infSystem.AttributeStatements.Add(tempAttr.States.Where(st => st.Value.Equals(state)).FirstOrDefault());
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateInformationSystem(string name, List<string> keys, List<string> values)
        {
            #region Перезаполнить аттрибуты
            for (int i = 0; i < keys.Count;i++)
            {
                await SetAttributeToInformationalSystem(name, keys[i], values[i]);
            }
            #endregion
            return true;
        }

        public async Task<InfSystem?> GetInformationSystemByFullName(string fullName)
        {
            #region Найти ИС по названию
            return _dbContext.InfSystems.Include(iS => iS.AttributeStatements).Include(iS => iS.Attributes).Where(infSys => infSys.Name.Equals(fullName)).FirstOrDefault();
            #endregion
        }
    }
}