using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegisterOfInformationSystems.Database.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolePower = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    AttributeId = table.Column<int>(type: "integer", nullable: false),
                    AttributeId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeStatements_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeStatements_Attributes_AttributeId1",
                        column: x => x.AttributeId1,
                        principalTable: "Attributes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IStoAtributes",
                columns: table => new
                {
                    AttributesId = table.Column<int>(type: "integer", nullable: false),
                    InformationSystemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IStoAtributes", x => new { x.AttributesId, x.InformationSystemsId });
                    table.ForeignKey(
                        name: "FK_IStoAtributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IStoAtributes_InfSystems_InformationSystemsId",
                        column: x => x.InformationSystemsId,
                        principalTable: "InfSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IStoAtStatements",
                columns: table => new
                {
                    AttributeStatementsId = table.Column<int>(type: "integer", nullable: false),
                    InformationSystemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IStoAtStatements", x => new { x.AttributeStatementsId, x.InformationSystemsId });
                    table.ForeignKey(
                        name: "FK_IStoAtStatements_AttributeStatements_AttributeStatementsId",
                        column: x => x.AttributeStatementsId,
                        principalTable: "AttributeStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IStoAtStatements_InfSystems_InformationSystemsId",
                        column: x => x.InformationSystemsId,
                        principalTable: "InfSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { -28, "Прочие информационные ссылки по ИС", "OtherIPInformationLinks" },
                    { -27, "Список интегрируемых ИС, направление и описание передаваемых данных", "ListOfIntegratedISAndDescriptionOfTransmittedData" },
                    { -26, "Описание внешнего (интеграционного) API ИС (при наличии)", "DescriptonOfExternalAPIs" },
                    { -25, "Наличие исходных кодов ИС в корпоративном репозитории", "SourceCodeInCorporateREpositoryAvailability" },
                    { -24, "Список компонентов ИС с указанием стека (средств разработки)", "ListOfISComponentsWithStack" },
                    { -23, "Наличие руководства администратора ИС", "AdminManualAvailability" },
                    { -22, "Наличие руководства пользователя ИС", "UserManualAvailability" },
                    { -21, "Список серверов и их функциональные роли", "ListOfServersAndRoles" },
                    { -20, "Архитектура ИС", "ISArchitecture" },
                    { -19, "Срок хранения информации", "InformationStoragePeriod" },
                    { -18, "Периодичность обновления информации", "InformationUpdateFrequency" },
                    { -17, "Способы авторизации пользователей", "UserAuthorizationMethods" },
                    { -16, "Наличие ролевого доступа", "RoleAccessAvailability" },
                    { -15, "Количество рабочих мест", "NumberOfJobs" },
                    { -14, "Дополнительное прикладное ПО", "AdditionalSoftware" },
                    { -13, "СУБД", "DataBase" },
                    { -12, "Клиентские ОС", "CLientOS" },
                    { -11, "Серверная ОС", "ServerOS" },
                    { -10, "Дата внесения изменений в сведения о ИС", "InformationEditDate" },
                    { -9, "Контакты ответственного должностного лица", "ResponsiblePersonContacts" },
                    { -8, "Ответственное за работу ИС структурноое подразделение", "ResponsibleDepartment" },
                    { -7, "Основные Функции", "BaseFunctions" },
                    { -6, "Область применения(бизнес-процессы)", "UsingSphere" },
                    { -5, "Бизнес-Планирование", "Business-Planning" },
                    { -4, "Информация по лицензиям", "LicenseInformation" },
                    { -3, "Разработчик", "Developer" },
                    { -2, "Дата ввода в эксплуатацию", "WhenStartedUsing" },
                    { -1, "Назначение", "Purpose" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name", "RolePower" },
                values: new object[,]
                {
                    { -3, "Can Take Information", "Reader", 3 },
                    { -2, "Can Edit Information Systems", "Editor", 2 },
                    { -1, "Can do anything", "admin", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeStatements_AttributeId",
                table: "AttributeStatements",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeStatements_AttributeId1",
                table: "AttributeStatements",
                column: "AttributeId1");

            migrationBuilder.CreateIndex(
                name: "IX_IStoAtributes_InformationSystemsId",
                table: "IStoAtributes",
                column: "InformationSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_IStoAtStatements_InformationSystemsId",
                table: "IStoAtStatements",
                column: "InformationSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IStoAtributes");

            migrationBuilder.DropTable(
                name: "IStoAtStatements");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AttributeStatements");

            migrationBuilder.DropTable(
                name: "InfSystems");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Attributes");
        }
    }
}
