using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;

namespace RegisterOfIS.Database.DbConfig
{
    public class AttributeConfig : IEntityTypeConfiguration<Models.Attribute>
    {
        public void Configure(EntityTypeBuilder<Models.Attribute> e)
        {

            e.ToTable("attributes");

            e.HasMany(a => a.States)
                .WithOne(s => s.Attribute)
                .HasForeignKey(a => a.AttributeId);

            e.HasData(
                new Models.Attribute("Purpose", "Назначение", new List<AttributeStatement>() { }) { Id = -1 },
                new Models.Attribute("WhenStartedUsing", "Дата ввода в эксплуатацию", new List<AttributeStatement>() { }) { Id = -2 },
                new Models.Attribute("Developer", "Разработчик", new List<AttributeStatement>() { }) { Id = -3 },
                new Models.Attribute("LicenseInformation", "Информация по лицензиям", new List<AttributeStatement>() { }) { Id = -4 },
                new Models.Attribute("Business-Planning", "Бизнес-Планирование", new List<AttributeStatement>() { }) { Id = -5 },
                new Models.Attribute("UsingSphere", "Область применения(бизнес-процессы)", new List<AttributeStatement>() { }) { Id = -6 },
                new Models.Attribute("BaseFunctions", "Основные Функции", new List<AttributeStatement>() { }) { Id = -7 },
                new Models.Attribute("ResponsibleDepartment", "Ответственное за работу ИС структурноое подразделение", new List<AttributeStatement>() { }) { Id = -8 },
                new Models.Attribute("ResponsiblePersonContacts", "Контакты ответственного должностного лица", new List<AttributeStatement>() { }) { Id = -9 },
                new Models.Attribute("InformationEditDate", "Дата внесения изменений в сведения о ИС", new List<AttributeStatement>() { }) { Id = -10 },
                new Models.Attribute("ServerOS", "Серверная ОС", new List<AttributeStatement>() { }) { Id = -11 },
                new Models.Attribute("CLientOS", "Клиентские ОС", new List<AttributeStatement>() { }) { Id = -12 },
                new Models.Attribute("DataBase", "СУБД", new List<AttributeStatement>() { }) { Id = -13 },
                new Models.Attribute("AdditionalSoftware", "Дополнительное прикладное ПО", new List<AttributeStatement>() { }) { Id = -14 },
                new Models.Attribute("NumberOfJobs", "Количество рабочих мест", new List<AttributeStatement>() { }) { Id = -15 },
                new Models.Attribute("RoleAccessAvailability", "Наличие ролевого доступа", new List<AttributeStatement>() { }) { Id = -16 },
                new Models.Attribute("UserAuthorizationMethods", "Способы авторизации пользователей", new List<AttributeStatement>() { }) { Id = -17 },
                new Models.Attribute("InformationUpdateFrequency", "Периодичность обновления информации", new List<AttributeStatement>() { }) { Id = -18 },
                new Models.Attribute("InformationStoragePeriod", "Срок хранения информации", new List<AttributeStatement>() { }) { Id = -19 },
                new Models.Attribute("ISArchitecture", "Архитектура ИС", new List<AttributeStatement>() { }) { Id = -20 },
                new Models.Attribute("ListOfServersAndRoles", "Список серверов и их функциональные роли", new List<AttributeStatement>() { }) { Id = -21 },
                new Models.Attribute("UserManualAvailability", "Наличие руководства пользователя ИС", new List<AttributeStatement>() { }) { Id = -22 },
                new Models.Attribute("AdminManualAvailability", "Наличие руководства администратора ИС", new List<AttributeStatement>() { }) { Id = -23 },
                new Models.Attribute("ListOfISComponentsWithStack", "Список компонентов ИС с указанием стека (средств разработки)", new List<AttributeStatement>() { }) { Id = -24 },
                new Models.Attribute("SourceCodeInCorporateREpositoryAvailability", "Наличие исходных кодов ИС в корпоративном репозитории", new List<AttributeStatement>() { }) { Id = -25 },
                new Models.Attribute("DescriptonOfExternalAPIs", "Описание внешнего (интеграционного) API ИС (при наличии)", new List<AttributeStatement>() { }) { Id = -26 },
                new Models.Attribute("ListOfIntegratedISAndDescriptionOfTransmittedData", "Список интегрируемых ИС, направление и описание передаваемых данных", new List<AttributeStatement>() { }) { Id = -27 },
                new Models.Attribute("OtherIPInformationLinks", "Прочие информационные ссылки по ИС", new List<AttributeStatement>() { }) { Id = -28 }
                );
        }
    }
}
