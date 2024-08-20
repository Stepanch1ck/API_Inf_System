using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegisterOfIS.Migrations
{
    /// <inheritdoc />
    public partial class _10Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsFixed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.Id);
                });

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
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("refresh_token_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attribute_dict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttributeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_dict", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attribute_dict_attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attribute_states",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "integer", nullable: false),
                    InfSystemId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("attribute_state_pkey", x => new { x.InfSystemId, x.AttributeId });
                    table.ForeignKey(
                        name: "attribute_state_attribute_fkey",
                        column: x => x.AttributeId,
                        principalTable: "attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "attribute_state_inf_system_fkey",
                        column: x => x.InfSystemId,
                        principalTable: "inf_systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "user_role_id_fkey",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "Id", "Description", "IsFixed", "Name" },
                values: new object[,]
                {
                    { -28, "Прочие информационные ссылки по ИС", false, "OtherIPInformationLinks" },
                    { -27, "Список интегрируемых ИС, направление и описание передаваемых данных", false, "ListOfIntegratedISAndDescriptionOfTransmittedData" },
                    { -26, "Описание внешнего (интеграционного) API ИС (при наличии)", false, "DescriptonOfExternalAPIs" },
                    { -25, "Наличие исходных кодов ИС в корпоративном репозитории", false, "SourceCodeInCorporateREpositoryAvailability" },
                    { -24, "Список компонентов ИС с указанием стека (средств разработки)", false, "ListOfISComponentsWithStack" },
                    { -23, "Наличие руководства администратора ИС", false, "AdminManualAvailability" },
                    { -22, "Наличие руководства пользователя ИС", false, "UserManualAvailability" },
                    { -21, "Список серверов и их функциональные роли", false, "ListOfServersAndRoles" },
                    { -20, "Архитектура ИС", false, "ISArchitecture" },
                    { -19, "Срок хранения информации", false, "InformationStoragePeriod" },
                    { -18, "Периодичность обновления информации", false, "InformationUpdateFrequency" },
                    { -17, "Способы авторизации пользователей", false, "UserAuthorizationMethods" },
                    { -16, "Наличие ролевого доступа", false, "RoleAccessAvailability" },
                    { -15, "Количество рабочих мест", false, "NumberOfJobs" },
                    { -14, "Дополнительное прикладное ПО", false, "AdditionalSoftware" },
                    { -13, "СУБД", false, "DataBase" },
                    { -12, "Клиентские ОС", false, "CLientOS" },
                    { -11, "Серверная ОС", false, "ServerOS" },
                    { -10, "Дата внесения изменений в сведения о ИС", false, "InformationEditDate" },
                    { -9, "Контакты ответственного должностного лица", false, "ResponsiblePersonContacts" },
                    { -8, "Ответственное за работу ИС структурноое подразделение", false, "ResponsibleDepartment" },
                    { -7, "Основные Функции", false, "BaseFunctions" },
                    { -6, "Область применения(бизнес-процессы)", false, "UsingSphere" },
                    { -5, "Бизнес-Планирование", false, "Business-Planning" },
                    { -4, "Информация по лицензиям", false, "LicenseInformation" },
                    { -3, "Разработчик", false, "Developer" },
                    { -2, "Дата ввода в эксплуатацию", false, "WhenStartedUsing" },
                    { -1, "Назначение", false, "Purpose" }
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
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { -3, "read access", "READER" },
                    { -2, "create and read access", "CREATOR" },
                    { -1, "full access", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Login", "Password", "RoleId" },
                values: new object[,]
                {
                    { -2, "reader", "$2a$11$ibU9O5Nsfi0TeS7J7GHpqOCE6FapVU3Y5RtzAEOKKQph3CgDLnrJy", -3 },
                    { -1, "admin", "$2a$11$hmuUGLf.gBactT0CH3GWzOiQyRkZf3oDy2TNgYd1ttc2AL5rm.h.y", -1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_attribute_dict_AttributeId",
                table: "attribute_dict",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_states_AttributeId",
                table: "attribute_states",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attribute_dict");

            migrationBuilder.DropTable(
                name: "attribute_states");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "inf_systems");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
