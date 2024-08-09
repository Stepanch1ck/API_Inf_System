using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegisterOfIS.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inf_systems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inf_systems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    token = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("refresh_token_pkey", x => x.token);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pkey", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    InfSystemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attributes_inf_systems_InfSystemId",
                        column: x => x.InfSystemId,
                        principalTable: "inf_systems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "user_role_id_fkey",
                        column: x => x.RoleName,
                        principalTable: "roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "attribute_statements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttributeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    InfSystemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_statements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attribute_statements_attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attribute_statements_inf_systems_InfSystemId",
                        column: x => x.InfSystemId,
                        principalTable: "inf_systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "Id", "Description", "InfSystemId", "Name" },
                values: new object[,]
                {
                    { -28, "Прочие информационные ссылки по ИС", null, "OtherIPInformationLinks" },
                    { -27, "Список интегрируемых ИС, направление и описание передаваемых данных", null, "ListOfIntegratedISAndDescriptionOfTransmittedData" },
                    { -26, "Описание внешнего (интеграционного) API ИС (при наличии)", null, "DescriptonOfExternalAPIs" },
                    { -25, "Наличие исходных кодов ИС в корпоративном репозитории", null, "SourceCodeInCorporateREpositoryAvailability" },
                    { -24, "Список компонентов ИС с указанием стека (средств разработки)", null, "ListOfISComponentsWithStack" },
                    { -23, "Наличие руководства администратора ИС", null, "AdminManualAvailability" },
                    { -22, "Наличие руководства пользователя ИС", null, "UserManualAvailability" },
                    { -21, "Список серверов и их функциональные роли", null, "ListOfServersAndRoles" },
                    { -20, "Архитектура ИС", null, "ISArchitecture" },
                    { -19, "Срок хранения информации", null, "InformationStoragePeriod" },
                    { -18, "Периодичность обновления информации", null, "InformationUpdateFrequency" },
                    { -17, "Способы авторизации пользователей", null, "UserAuthorizationMethods" },
                    { -16, "Наличие ролевого доступа", null, "RoleAccessAvailability" },
                    { -15, "Количество рабочих мест", null, "NumberOfJobs" },
                    { -14, "Дополнительное прикладное ПО", null, "AdditionalSoftware" },
                    { -13, "СУБД", null, "DataBase" },
                    { -12, "Клиентские ОС", null, "CLientOS" },
                    { -11, "Серверная ОС", null, "ServerOS" },
                    { -10, "Дата внесения изменений в сведения о ИС", null, "InformationEditDate" },
                    { -9, "Контакты ответственного должностного лица", null, "ResponsiblePersonContacts" },
                    { -8, "Ответственное за работу ИС структурноое подразделение", null, "ResponsibleDepartment" },
                    { -7, "Основные Функции", null, "BaseFunctions" },
                    { -6, "Область применения(бизнес-процессы)", null, "UsingSphere" },
                    { -5, "Бизнес-Планирование", null, "Business-Planning" },
                    { -4, "Информация по лицензиям", null, "LicenseInformation" },
                    { -3, "Разработчик", null, "Developer" },
                    { -2, "Дата ввода в эксплуатацию", null, "WhenStartedUsing" },
                    { -1, "Назначение", null, "Purpose" }
                });

            migrationBuilder.InsertData(
                table: "inf_systems",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { -2, "Desc2", "IS2" },
                    { -1, "Desc1", "IS1" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "name", "description" },
                values: new object[,]
                {
                    { "ADMIN", "full access" },
                    { "CREATOR", "create and read access" },
                    { "READER", "read access" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Login", "Password", "RoleName" },
                values: new object[,]
                {
                    { -2, "reader", "$2a$11$ibU9O5Nsfi0TeS7J7GHpqOCE6FapVU3Y5RtzAEOKKQph3CgDLnrJy", "READER" },
                    { -1, "admin", "$2a$11$hmuUGLf.gBactT0CH3GWzOiQyRkZf3oDy2TNgYd1ttc2AL5rm.h.y", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_attribute_statements_AttributeId",
                table: "attribute_statements",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_statements_InfSystemId",
                table: "attribute_statements",
                column: "InfSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_attributes_InfSystemId",
                table: "attributes",
                column: "InfSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleName",
                table: "users",
                column: "RoleName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attribute_statements");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "inf_systems");
        }
    }
}
