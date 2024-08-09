using Microsoft.EntityFrameworkCore;
using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Core.Models;
using RegisterOfInformationSystems.Database;
using Services;
using Attribute = RegisterOfInformationSystems.Core.Models.Attribute;

namespace RegisterOfInformationSystems.Service
{
    public class InformationSystemService(PostgresDbContext dbContext) : IActionsOnTheInformationSystem
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        private readonly AttributeService _attributeService = new(dbContext);
        public async Task<bool> CreateInformationSystem(string name, string description, List<string> attributeNames, List<string> attributeValues)
        {
            #region Вернуть false если уже есть ИС
            if (GetInformationSystemByFullName(name) != null) return false;
            #endregion

            #region Создание временной ИС
            var tempInfSystem = new InformationSystem
            {
                Name = name,
                Description = description
            };
            #endregion

            #region Заполнить ИС аттрибутами
            for (int i = 0;i<attributeNames.Count;i++)
            {
                var tempAttr = _attributeService.GetAttributeByFullName(attributeNames[i]);
                if(tempAttr == null) continue;
                tempInfSystem.Attributes.Add(tempAttr);
                var tempState = tempAttr.States.Where(st => st.Value == attributeValues[i]).FirstOrDefault();
                if (tempState == null) continue;
                tempInfSystem.AttributeStatements.Add(tempState);
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
            var infSystem = GetInformationSystemByFullName(name);
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

        public List<InformationSystem>? GetInformationSystemsByAttributeContain(string attribute)
        {
            #region найти аттрибут для поиска
            var tempattr = _attributeService.GetAttributeByFullName(attribute);
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

        public List<InformationSystem>? GetInformationSystemsByPartOfName(string partOfName)
        {
            #region Найти все ИС по поисковому запросу
            var infSystems = _dbContext.InfSystems.Include(iS => iS.Attributes).Include(iS => iS.AttributeStatements).Where(sys => sys.Name.Contains(partOfName));
            #endregion

            #region Вернуть null если нет таких ИС
            if (infSystems.FirstOrDefault() == null) return null;
            #endregion

            #region Вернуть список ИС
            return [.. infSystems];
            #endregion
        }

        public async Task<bool> SetAttributeToInformationalSystem(string name, string attribute, string state)
        {
            #region Найти ИС по названию
            var infSystem = GetInformationSystemByFullName(name);
            #endregion

            #region Вернуть false если нет такой ИС
            if (infSystem == null) return false;
            #endregion

            #region Найти аттрибут по названию
            var tempattr = _attributeService.GetAttributeByFullName(attribute);
            #endregion

            #region Вернуть false если нет аттрибута
            if (tempattr == null) return false;
            #endregion

            #region Удалить аттрибуть если таковой есть
            if (infSystem.Attributes.Contains(tempattr))
            {
                infSystem.Attributes.Remove(tempattr);
            }
            #endregion

            #region Добавить аттрибут
            infSystem.Attributes.Add(tempattr);
            #endregion

            #region Заменить значение аттрибута
            var currentState = infSystem.AttributeStatements.Where(attr => attr.Attribute == tempattr).FirstOrDefault();
            var newState = tempattr.States.Where(st => st.Value == state).FirstOrDefault();
            if (currentState != null && newState != null)
            {
                infSystem.AttributeStatements.Remove(currentState);
                infSystem.AttributeStatements.Add(newState);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
            #endregion
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

        public InformationSystem? GetInformationSystemByFullName(string fullName)
        {
            if (fullName == null) return null;
            #region Найти ИС по названию
            return _dbContext.InfSystems.Include(iS => iS.Attributes).Include(iS => iS.Attributes).Where(infSys => infSys.Name.Equals(fullName)).FirstOrDefault();
            #endregion
        }
    }
}