using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalyearproject.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "conpany_id", "Address", "Email_conpany", "User_Name", "conpany_name", "position", "status" },
                values: new object[] { 999, "open", "b", "d", "a", "c", "open" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "Birthday", "Email", "Gender", "Name", "Password", "Phone", "Status", "Viewable", "conpany_id", "role" },
                values: new object[] { 1, "13/05/2002", "abc@gmail.com", "Male", "nhan", "123456", "07777", "Ok", "public", 999, "user" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companys",
                keyColumn: "conpany_id",
                keyValue: 999);
        }
    }
}
