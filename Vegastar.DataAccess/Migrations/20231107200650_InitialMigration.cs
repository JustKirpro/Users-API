using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vegastar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_groups",
                columns: table => new
                {
                    user_group_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_groups", x => x.user_group_id);
                });

            migrationBuilder.CreateTable(
                name: "user_states",
                columns: table => new
                {
                    user_state_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_states", x => x.user_state_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    group_id = table.Column<long>(type: "bigint", nullable: false),
                    state_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_users_user_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "user_groups",
                        principalColumn: "user_group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_user_states_state_id",
                        column: x => x.state_id,
                        principalTable: "user_states",
                        principalColumn: "user_state_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "user_groups",
                columns: new[] { "user_group_id", "code", "description" },
                values: new object[,]
                {
                    { 1L, 1, "Admin" },
                    { 2L, 2, "Regular user" }
                });

            migrationBuilder.InsertData(
                table: "user_states",
                columns: new[] { "user_state_id", "code", "description" },
                values: new object[,]
                {
                    { 1L, 1, "Active" },
                    { 2L, 2, "Blocked" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_group_id",
                table: "users",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_state_id",
                table: "users",
                column: "state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_groups");

            migrationBuilder.DropTable(
                name: "user_states");
        }
    }
}
