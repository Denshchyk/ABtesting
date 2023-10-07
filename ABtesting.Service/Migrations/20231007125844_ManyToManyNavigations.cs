using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABtesting.Service.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyNavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevicesExperiments_Devices_DeviceToken1",
                table: "DevicesExperiments");

            migrationBuilder.DropIndex(
                name: "IX_DevicesExperiments_DeviceToken1",
                table: "DevicesExperiments");

            migrationBuilder.DropColumn(
                name: "DeviceToken1",
                table: "DevicesExperiments");

            migrationBuilder.CreateIndex(
                name: "IX_DevicesExperiments_DeviceToken",
                table: "DevicesExperiments",
                column: "DeviceToken");

            migrationBuilder.AddForeignKey(
                name: "FK_DevicesExperiments_Devices_DeviceToken",
                table: "DevicesExperiments",
                column: "DeviceToken",
                principalTable: "Devices",
                principalColumn: "DeviceToken",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevicesExperiments_Devices_DeviceToken",
                table: "DevicesExperiments");

            migrationBuilder.DropIndex(
                name: "IX_DevicesExperiments_DeviceToken",
                table: "DevicesExperiments");

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceToken1",
                table: "DevicesExperiments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DevicesExperiments_DeviceToken1",
                table: "DevicesExperiments",
                column: "DeviceToken1");

            migrationBuilder.AddForeignKey(
                name: "FK_DevicesExperiments_Devices_DeviceToken1",
                table: "DevicesExperiments",
                column: "DeviceToken1",
                principalTable: "Devices",
                principalColumn: "DeviceToken",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
