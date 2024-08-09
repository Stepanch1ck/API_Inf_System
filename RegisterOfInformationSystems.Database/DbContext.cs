using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegisterOfInformationSystems.Core.Models;
using Attribute = RegisterOfInformationSystems.Core.Models.Attribute;

namespace RegisterOfInformationSystems.Database
{
    public class PostgresDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users => Set<User>();
        public DbSet<InformationSystem> InfSystems => Set<InformationSystem>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Attribute> Attributes => Set<Attribute>();
        public DbSet<AttributeStatement> AttributeStatements => Set<AttributeStatement>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
        public PostgresDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InformationSystem>().HasMany(inf => inf.AttributeStatements).WithMany(st => st.InformationSystems).UsingEntity("IStoAtStatements");
            modelBuilder.Entity<InformationSystem>().HasMany(inf => inf.Attributes).WithMany(attr => attr.InformationSystems).UsingEntity("IStoAtributes");
            modelBuilder.Entity<Role>().HasMany(r => r.Users).WithOne(us => us.Role).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<Attribute>().HasMany(a => a.States).WithOne(s => s.attribute).HasForeignKey(a => a.AttributeId);
            modelBuilder.Entity<Role>().HasData(
                new Role(1, "admin", "Can do anything") { Id = -1 },
                new Role(2, "Editor", "Can Edit Information Systems") { Id = -2 },
                new Role(3, "Reader", "Can Take Information") { Id = -3 }
                );
            modelBuilder.Entity<Attribute>().HasData(
                new Attribute("Purpose", "Назначение", new List<AttributeStatement>() { }) { Id = -1 },
                new Attribute("WhenStartedUsing", "Дата ввода в эксплуатацию", new List<AttributeStatement>() { }) { Id = -2 },
                new Attribute("Developer", "Разработчик", new List<AttributeStatement>() { }) { Id = -3 },
                new Attribute("LicenseInformation", "Информация по лицензиям", new List<AttributeStatement>() { }) { Id = -4 },
                new Attribute("Business-Planning", "Бизнес-Планирование", new List<AttributeStatement>() { }) { Id = -5 },
                new Attribute("UsingSphere", "Область применения(бизнес-процессы)", new List<AttributeStatement>() { }) { Id = -6 },
                new Attribute("BaseFunctions", "Основные Функции", new List<AttributeStatement>() { }) { Id = -7 },
                new Attribute("ResponsibleDepartment", "Ответственное за работу ИС структурноое подразделение", new List<AttributeStatement>() { }) { Id = -8 },
                new Attribute("ResponsiblePersonContacts", "Контакты ответственного должностного лица", new List<AttributeStatement>() { }) { Id = -9 },
                new Attribute("InformationEditDate", "Дата внесения изменений в сведения о ИС", new List<AttributeStatement>() { }) { Id = -10 },
                new Attribute("ServerOS", "Серверная ОС", new List<AttributeStatement>() { }) { Id = -11 },
                new Attribute("CLientOS", "Клиентские ОС", new List<AttributeStatement>() { }) { Id = -12 },
                new Attribute("DataBase", "СУБД", new List<AttributeStatement>() { }) { Id = -13 },
                new Attribute("AdditionalSoftware", "Дополнительное прикладное ПО", new List<AttributeStatement>() { }) { Id = -14 },
                new Attribute("NumberOfJobs", "Количество рабочих мест", new List<AttributeStatement>() { }) { Id = -15 },
                new Attribute("RoleAccessAvailability", "Наличие ролевого доступа", new List<AttributeStatement>() { }) { Id = -16 },
                new Attribute("UserAuthorizationMethods", "Способы авторизации пользователей", new List<AttributeStatement>() { }) { Id = -17 },
                new Attribute("InformationUpdateFrequency", "Периодичность обновления информации", new List<AttributeStatement>() { }) { Id = -18 },
                new Attribute("InformationStoragePeriod", "Срок хранения информации", new List<AttributeStatement>() { }) { Id = -19 },
                new Attribute("ISArchitecture", "Архитектура ИС", new List<AttributeStatement>() { }) { Id = -20 },
                new Attribute("ListOfServersAndRoles", "Список серверов и их функциональные роли", new List<AttributeStatement>() { }) { Id = -21 },
                new Attribute("UserManualAvailability", "Наличие руководства пользователя ИС", new List<AttributeStatement>() { }) { Id = -22 },
                new Attribute("AdminManualAvailability", "Наличие руководства администратора ИС", new List<AttributeStatement>() { }) { Id = -23 },
                new Attribute("ListOfISComponentsWithStack", "Список компонентов ИС с указанием стека (средств разработки)", new List<AttributeStatement>() { }) { Id = -24 },
                new Attribute("SourceCodeInCorporateREpositoryAvailability", "Наличие исходных кодов ИС в корпоративном репозитории", new List<AttributeStatement>() { }) { Id = -25 },
                new Attribute("DescriptonOfExternalAPIs", "Описание внешнего (интеграционного) API ИС (при наличии)", new List<AttributeStatement>() { }) { Id = -26 },
                new Attribute("ListOfIntegratedISAndDescriptionOfTransmittedData", "Список интегрируемых ИС, направление и описание передаваемых данных", new List<AttributeStatement>() { }) { Id = -27 },
                new Attribute("OtherIPInformationLinks", "Прочие информационные ссылки по ИС", new List<AttributeStatement>() { }) { Id = -28 }
                );
        }
    }
}
