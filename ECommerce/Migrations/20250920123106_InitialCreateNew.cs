using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8152a3a2-fa24-48ef-b41d-f924c569049b", "AQAAAAIAAYagAAAAEGGNmcRp7f1rf0nvhAw7gFi/c9FDy64RoBg+ccS9rwkDmxodu4VtYX+Nc+zfJL7n3g==", "f46b4309-944b-4310-8429-2cc9352cb517" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edcb512e-9b2d-408b-84dc-47b57d9f6d50", "AQAAAAIAAYagAAAAEIU0ni/20tuPx58a41Tu4MUhkpipDgBSfPodptwuRdizT4kstswRwSiMyzDNz4/NCw==", "02a81f15-5891-4307-a603-ba7967ca3ac2" });
        }
    }
}
