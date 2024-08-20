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

            e.HasMany(a => a.Dict)
                .WithOne(d => d.Attribute)
                .HasForeignKey(d => d.AttributeId);

            e.HasMany(a => a.States)
                .WithOne(s => s.Attribute)
                .HasForeignKey(a => a.AttributeId);

            e.HasMany(a => a.InfSystems)
                .WithMany(ins => ins.Attributes)
                .UsingEntity<AttributeState>();

            e.HasData(
                new Models.Attribute("Purpose", "Назначение", false, new List<AttributeState>() { }) { Id = -1 },
                new Models.Attribute("WhenStartedUsing", "Дата ввода в эксплуатацию", false, new List<AttributeState>() { }) { Id = -2 },
                new Models.Attribute("Developer", "Разработчик", false, new List<AttributeState>() { }) { Id = -3 },
                new Models.Attribute("LicenseInformation", "Информация по лицензиям", false, new List<AttributeState>() { }) { Id = -4 },
                new Models.Attribute("Business-Planning", "Бизнес-Планирование", false, new List<AttributeState>() { }) { Id = -5 },
                new Models.Attribute("UsingSphere", "Область применения(бизнес-процессы)", false, new List<AttributeState>() { }) { Id = -6 },
                new Models.Attribute("BaseFunctions", "Основные Функции", false, new List<AttributeState>() { }) { Id = -7 },
                new Models.Attribute("ResponsibleDepartment", "Ответственное за работу ИС структурноое подразделение", false, new List<AttributeState>() { }) { Id = -8 },
                new Models.Attribute("ResponsiblePersonContacts", "Контакты ответственного должностного лица", false, new List<AttributeState>() { }) { Id = -9 },
                new Models.Attribute("InformationEditDate", "Дата внесения изменений в сведения о ИС", false, new List<AttributeState>() { }) { Id = -10 },
                new Models.Attribute("ServerOS", "Серверная ОС", false, new List<AttributeState>() { }) { Id = -11 },
                new Models.Attribute("CLientOS", "Клиентские ОС", false, new List<AttributeState>() { }) { Id = -12 },
                new Models.Attribute("DataBase", "СУБД", false, new List<AttributeState>() { }) { Id = -13 },
                new Models.Attribute("AdditionalSoftware", "Дополнительное прикладное ПО", false, new List<AttributeState>() { }) { Id = -14 },
                new Models.Attribute("NumberOfJobs", "Количество рабочих мест", false, new List<AttributeState>() { }) { Id = -15 },
                new Models.Attribute("RoleAccessAvailability", "Наличие ролевого доступа", false, new List<AttributeState>() { }) { Id = -16 },
                new Models.Attribute("UserAuthorizationMethods", "Способы авторизации пользователей", false, new List<AttributeState>() { }) { Id = -17 },
                new Models.Attribute("InformationUpdateFrequency", "Периодичность обновления информации", false, new List<AttributeState>() { }) { Id = -18 },
                new Models.Attribute("InformationStoragePeriod", "Срок хранения информации", false, new List<AttributeState>() { }) { Id = -19 },
                new Models.Attribute("ISArchitecture", "Архитектура ИС", false, new List<AttributeState>() { }) { Id = -20 },
                new Models.Attribute("ListOfServersAndRoles", "Список серверов и их функциональные роли", false, new List<AttributeState>() { }) { Id = -21 },
                new Models.Attribute("UserManualAvailability", "Наличие руководства пользователя ИС", false, new List<AttributeState>() { }) { Id = -22 },
                new Models.Attribute("AdminManualAvailability", "Наличие руководства администратора ИС", false, new List<AttributeState>() { }) { Id = -23 },
                new Models.Attribute("ListOfISComponentsWithStack", "Список компонентов ИС с указанием стека (средств разработки)", false, new List<AttributeState>() { }) { Id = -24 },
                new Models.Attribute("SourceCodeInCorporateREpositoryAvailability", "Наличие исходных кодов ИС в корпоративном репозитории", false, new List<AttributeState>() { }) { Id = -25 },
                new Models.Attribute("DescriptonOfExternalAPIs", "Описание внешнего (интеграционного) API ИС (при наличии)", false, new List<AttributeState>() { }) { Id = -26 },
                new Models.Attribute("ListOfIntegratedISAndDescriptionOfTransmittedData", "Список интегрируемых ИС, направление и описание передаваемых данных", false, new List<AttributeState>() { }) { Id = -27 },
                new Models.Attribute("OtherIPInformationLinks", "Прочие информационные ссылки по ИС", false, new List<AttributeState>() { }) { Id = -28 }
                );
        }
    }
}
