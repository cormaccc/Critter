using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterCloneApp.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatedAtColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Posts");


            migrationBuilder.AddColumn<DateTime>(
                 name: "CreatedAt",
                 table: "Posts",
                 nullable: true,
                 type: "DATETIME");
        }
    }
}
