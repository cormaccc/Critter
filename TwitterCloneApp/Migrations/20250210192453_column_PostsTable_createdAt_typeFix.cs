using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterCloneApp.Migrations
{
    /// <inheritdoc />
    public partial class column_PostsTable_createdAt_typeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(name: "CreatedAt", table: "Posts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Posts",
                nullable: true); 

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
