using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Industry_4._0.Data.Migrations
{
    public partial class Heading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "DiscussionForum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heading",
                table: "DiscussionForum");
        }
    }
}
