using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FUForum.BackendServer.Migrations
{
    /// <inheritdoc />
    public partial class addReplyCommnet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
                table: "Comments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Comments");
        }
    }
}
