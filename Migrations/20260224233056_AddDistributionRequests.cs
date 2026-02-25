using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharityApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDistributionRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodDonationId = table.Column<int>(type: "int", nullable: false),
                    TargetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionRequests_FoodDonations_FoodDonationId",
                        column: x => x.FoodDonationId,
                        principalTable: "FoodDonations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributionRequests_FoodDonationId",
                table: "DistributionRequests",
                column: "FoodDonationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributionRequests");
        }
    }
}
