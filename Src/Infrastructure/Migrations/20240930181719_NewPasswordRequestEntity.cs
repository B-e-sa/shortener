using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shortener.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewPasswordRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailVerifications_UserId",
                table: "EmailVerifications");

            migrationBuilder.CreateTable(
                name: "NewPasswordRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewPasswordRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewPasswordRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerifications_UserId",
                table: "EmailVerifications",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewPasswordRequests_UserId",
                table: "NewPasswordRequests",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewPasswordRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmailVerifications_UserId",
                table: "EmailVerifications");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerifications_UserId",
                table: "EmailVerifications",
                column: "UserId");
        }
    }
}
