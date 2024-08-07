using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "AttributeInformationSystem",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uuid", nullable: false),
                    InformationSystemsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeInformationSystem", x => new { x.AttributesId, x.InformationSystemsId });
                    table.ForeignKey(
                        name: "FK_AttributeInformationSystem_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeInformationSystem_InfSystems_InformationSystemsId",
                        column: x => x.InformationSystemsId,
                        principalTable: "InfSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "AttributeStatementInformationSystem",
                columns: table => new
                {
                    AttributeStatementsId = table.Column<Guid>(type: "uuid", nullable: false),
                    InformationSystemsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeStatementInformationSystem", x => new { x.AttributeStatementsId, x.InformationSystemsId });
                    table.ForeignKey(
                        name: "FK_AttributeStatementInformationSystem_AttributeStatements_Att~",
                        column: x => x.AttributeStatementsId,
                        principalTable: "AttributeStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeStatementInformationSystem_InfSystems_InformationS~",
                        column: x => x.InformationSystemsId,
                        principalTable: "InfSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeInformationSystem_InformationSystemsId",
                table: "AttributeInformationSystem",
                column: "InformationSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeStatementInformationSystem_InformationSystemsId",
                table: "AttributeStatementInformationSystem",
                column: "InformationSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeStatements_AttributeId",
                table: "AttributeStatements",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeStatements_AttributeId1",
                table: "AttributeStatements",
                column: "AttributeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeInformationSystem");

            migrationBuilder.DropTable(
                name: "AttributeStatementInformationSystem");

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
