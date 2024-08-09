using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.Token);
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    InfSystemId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    InfSystemId = table.Column<Guid>(type: "uuid", nullable: false)
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
                table: "inf_systems",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("431a4b04-eea9-4c78-a803-d77692f559e2"), "Desc2", "IS2" },
                    { new Guid("fbe3c341-c619-47f1-b5bc-058fe07cdd17"), "Desc1", "IS1" }
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
                    { new Guid("019ad33d-ea76-4315-8995-87968281df74"), "admin", "$2a$11$hmuUGLf.gBactT0CH3GWzOiQyRkZf3oDy2TNgYd1ttc2AL5rm.h.y", "ADMIN" },
                    { new Guid("4c102e5f-18d3-436e-a186-05eb6a719f6c"), "reader", "$2a$11$ibU9O5Nsfi0TeS7J7GHpqOCE6FapVU3Y5RtzAEOKKQph3CgDLnrJy", "READER" }
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
