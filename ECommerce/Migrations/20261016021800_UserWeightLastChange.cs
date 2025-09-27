using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class UserWeightLastChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LastChange",
                table: "UserWeight",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa4d4b20-7312-4aa8-b559-b383ce77a079", "AQAAAAIAAYagAAAAEKXsAt6r/Z8/6uHyohOe6MLQoIKIiIvHUcFOefQvtNm35sQ2l8vVneU0UZyWZLts+A==", "84100ca4-66a3-4987-875e-4a9411ab848f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChange",
                table: "UserWeight");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0675a3d-4440-4f2a-b670-2a92f2f21770", "AQAAAAIAAYagAAAAEALmSOUzLMIupAAytUF7HxNOjUGNuF4d7vIXSvOwsHZh0VgmVMsxOy1RZ6nYA7m/+Q==", "c3b71cc7-e507-43d7-b16c-53c6f61fb87b" });
        }
    }
}
