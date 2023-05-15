using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Industry_4._0.Data.Migrations
{
    public partial class agreementbutton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Agree",
                table: "DiscussionForum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disagree",
                table: "DiscussionForum",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agree",
                table: "DiscussionForum");

            migrationBuilder.DropColumn(
                name: "Disagree",
                table: "DiscussionForum");
        }
    }
}
