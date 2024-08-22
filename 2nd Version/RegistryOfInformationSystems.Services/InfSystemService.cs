using Microsoft.EntityFrameworkCore;
using RegistryOfInformationSystems.Database;
using RegistryOfInformationSystems.Interfaces;
using RegistryOfInformationSystems.Models;

namespace RegistryOfInformationSystems.Services
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
                if(tempAttr != null)
                {
                    if(tempAttr.States.Where(st => st.Value == attributeValues[i]).FirstOrDefault() == null)
                    {
                        await _attributeService.AddNewState(attributeNames[i], attributeValues[i]);
                    }
                    tempInfSystem.AttributeStatements.Add(tempAttr.States.Where(st => st.Value == attributeValues[i]).FirstOrDefault());
                }
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

        public async Task<List<InfSystem>> GetInformationSystemsByPartOfName(string partOfName)
        {
            #region Вернуть все если нет фильтра
            
            if (partOfName == null) return [.. _dbContext.InfSystems.Include(iS => iS.AttributeStatements)];
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
                _dbContext.SaveChanges();
            }
            var tempAttr = await _attributeService.GetAttributeByFullName(attribute);
            if (tempAttr == null)
            {
                return false;
            }
            if (tempAttr.States.Where(st => st.Value.Equals(state)).FirstOrDefault() == null)
            {
                if(!(await _attributeService.AddNewState(attribute, state)))
                {
                    return false;
                }
            }
            var statement = _dbContext.AttributeStatements.Include(aS => aS.Attribute).Where(aS => aS.Attribute.Name.Equals(attribute)).Where(aS => aS.Value.Equals(state)).FirstOrDefault();
            infSystem.AttributeStatements.Add(statement);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateInformationSystem(string name,string newName, string newDescription, List<string> keys, List<string> values)
        {
            var infSystem = await GetInformationSystemByFullName(name);
            if (infSystem == null)
            {
                return false;
            }
            else
            {
                if (newName != null)
                {
                    infSystem.Name = newName;
                }
                if(newDescription != null)
                { 
                    infSystem.Description = newDescription;
                }
                await _dbContext.SaveChangesAsync();
                #region Перезаполнить аттрибуты
                for (int i = 0; i < keys.Count; i++)
                {
                    await SetAttributeToInformationalSystem(newName, keys[i], values[i]);
                }
                #endregion
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<InfSystem?> GetInformationSystemByFullName(string fullName)
        {
            #region Найти ИС по названию
            return _dbContext.InfSystems.Include(iS => iS.AttributeStatements).Where(infSys => infSys.Name.Equals(fullName)).FirstOrDefault();
            #endregion
        }
    }
}