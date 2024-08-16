using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Interfaces;
using RegisterOfIS.Models;
using RegisterOfIS.Database;
using Attribute = RegisterOfIS.Models.Attribute;

namespace RegisterOfIS.Services
{
    public class AttributeService(PostgresDbContext dbContext) : IAttributeService
    {
        private readonly PostgresDbContext _dbContext = dbContext;

        public async Task<bool> AddNewState(string name, string state)
        {
            #region Найти аттрибут по имени
            var attribute = await GetAttributeByFullName(name);
            #endregion

            #region Вернуть false если нет аттрибута
            if (attribute == null) return false;
            #endregion

            #region Вернуть false если уже есть такое значение
            if (attribute.States.Select(st => st.Value).Contains(state)) return false;
            #endregion

            #region Добавить новое значение
            var tempAttrStatement = new AttributeStatement(attribute, state);
            attribute.States.Add(tempAttrStatement);
            await _dbContext.AttributeStatements.AddAsync(tempAttrStatement);
            await _dbContext.SaveChangesAsync();
            return true;
            #endregion
        }

        public async Task<bool> CreateAttribute(string name, string description, List<string> states)
        {
            #region Проверка на существование аттрибута
            if (await GetAttributeByFullName(name) != null) return false;
            #endregion

            else
            {
                #region Создать переменные для создания аттрибута
                var tempAttrStatements = new List<AttributeStatement>();
                var tempAttribute = new Attribute();
                #endregion

                #region Заполнить список со значениями
                foreach (var state in states)
                {
                    var tempStatement = new AttributeStatement(tempAttribute, state);
                    await _dbContext.AttributeStatements.AddAsync(tempStatement);
                    tempAttrStatements.Add(tempStatement);
                }
                #endregion

                #region Заполнение аттрибута
                tempAttribute.States = tempAttrStatements;
                tempAttribute.Description = description;
                tempAttribute.Name = name;
                #endregion

                #region Добавление аттрибута в бд
                await _dbContext.Attributes.AddAsync(tempAttribute);
                await _dbContext.SaveChangesAsync();
                return true;
                #endregion
            }

        }

        public async Task<bool> DeleteAttribute(string name)
        {
            #region Нати аттрибут по фулл имени
            var attr = await GetAttributeByFullName(name);
            #endregion

            #region Вернуть false если нет такого
            if (attr == null) return false;
            #endregion

            #region Удалить аттрибут
            _dbContext.Attributes.Remove(attr);
            await _dbContext.SaveChangesAsync();
            return true;
            #endregion

        }

        public List<Attribute>? GetAttributesByPartOfName(string partOfName)
        {
            #region Вытащить аттрибуты с БД
            var attributes = _dbContext.Attributes.Include(attr => attr.States).Where(attribute => attribute.Name.Contains(partOfName));
            #endregion

            #region Вернуть null если аттрибутов нет
            if (attributes.FirstOrDefault() == null) return null;
            #endregion

            #region вернуть аттрибуты
            return [.. attributes];
            #endregion
        }

        public List<Attribute>? GetAttributesByStateContain(string state)
        {
            #region Поиск аттрибутов по наличию значения
            var attributes = _dbContext.Attributes.Include(attr => attr.States).Where(attr => attr.States.Select(st => st.Value).Contains(state));
            #endregion

            #region Вернуть null Если там пусто
            if (attributes.FirstOrDefault() == null) return null;
            #endregion

            #region Вернуть аттрибуты
            return [.. attributes];
            #endregion
        }

        public async Task<bool> RemoveState(string name, string state)
        {
            #region Найти аттрибут по имени
            var attribute = await GetAttributeByFullName(name);
            #endregion

            #region Вернуть false если нет такого аттрибута
            if (attribute == null) return false;
            #endregion

            #region Вернуть false если нет такого значения
            else if (!attribute.States.Select(st => st.Value).Contains(state)) return false;
            #endregion

            #region Найти Значение у аттрибута
            var tempstate = attribute.States.Where(val => val.Value.Equals(state)).Single();
            #endregion

            #region Удалить значение у этого аттрибута
            attribute.States.Remove(tempstate);
            await _dbContext.SaveChangesAsync();
            return true;
            #endregion
        }

        public async Task<Attribute?> GetAttributeByFullName(string fullName)
        {
            #region Вернуть атррибут по фулл имени
            return _dbContext.Attributes.Include(attr => attr.States).Where(attr => attr.Name.Equals(fullName)).FirstOrDefault();
            #endregion
        }

        public async Task<bool> ChangeAttribute(string name, string newName, string description)
        {
            var attr = await GetAttributeByFullName(name);
            if (attr == null) return false;
            if(newName != null) attr.Name = newName;
            if(description != null) attr.Description = description;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
