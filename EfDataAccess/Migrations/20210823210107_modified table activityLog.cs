using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfDataAccess.Migrations
{
    public partial class modifiedtableactivityLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<string>(
                name: "Actor",
                table: "ActivityLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "ActivityLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UseCaseName",
                table: "ActivityLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actor",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UseCaseName",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ActivityLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ActivityLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ActivityLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ActivityLogs",
                type: "datetime2",
                nullable: true);
        }
    }
}
