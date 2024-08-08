using System;
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId1 = table.Column<Guid>(type: "uuid", nullable: true)
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
                    AttributesId = table.Column<Guid>(type: "uuid", nullable: false),
                    InformationSystemsId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    AttributeStatementsId = table.Column<Guid>(type: "uuid", nullable: false),
                    InformationSystemsId = table.Column<Guid>(type: "uuid", nullable: false)
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
