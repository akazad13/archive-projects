using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hostel",
                columns: table => new
                {
                    Hostel_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hostel_Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Hostel_Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Corporate_Person = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Intro = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Business_License_Img = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Registration_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext1 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Ext2 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostel", x => x.Hostel_Id);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Record_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Record_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Record_Fee = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Record_Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext1 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext2 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    HostelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Record_Id);
                    table.ForeignKey(
                        name: "FK_Record_Hostel_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostel",
                        principalColumn: "Hostel_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Room_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Number = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Room_Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext1 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext2 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    HostelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Room_Id);
                    table.ForeignKey(
                        name: "FK_Room_Hostel_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostel",
                        principalColumn: "Hostel_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Account = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Serial_Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Ext1 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext2 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    HostelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_User_Hostel_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostel",
                        principalColumn: "Hostel_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Booking_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_Time = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Booking_Fee = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Room_Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext1 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Ext2 = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Booking_Id);
                    table.ForeignKey(
                        name: "FK_Booking_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Chat_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Chat_Id);
                    table.ForeignKey(
                        name: "FK_Chat_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Review_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Review_Id);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                table: "Booking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserId",
                table: "Chat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_HostelId",
                table: "Record",
                column: "HostelId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HostelId",
                table: "Room",
                column: "HostelId");

            migrationBuilder.CreateIndex(
                name: "IX_User_HostelId",
                table: "User",
                column: "HostelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Hostel");
        }
    }
}
