using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Staff_Management.Infrasructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_Staff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffId = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staffs");
        }
    }
}
