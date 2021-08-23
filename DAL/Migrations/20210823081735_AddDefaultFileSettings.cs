using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddDefaultFileSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Settings (FileExtensions, FileSizeInBytes, FileSizeInMegaBytes) VALUES ('pdf,docx,xlsx,jpg,png,doc,xls,mp3,mp4,djvu', 5242880,5)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Settings");
        }
    }
}
