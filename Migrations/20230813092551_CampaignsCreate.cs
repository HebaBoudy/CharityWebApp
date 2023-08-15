using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTutorial.Migrations
{
    /// <inheritdoc />
    public partial class CampaignsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAmount = table.Column<int>(type: "int", nullable: false),
                    CollectedAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Campaign_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donor_Campaign",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    DonorID = table.Column<int>(type: "int", nullable: false),
                    UsersRegistrationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor_Campaign", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Donor_Campaign_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donor_Campaign_UsersRegistration_UsersRegistrationID",
                        column: x => x.UsersRegistrationID,
                        principalTable: "UsersRegistration",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_CompanyID",
                table: "Campaign",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_Campaign_CampaignId",
                table: "Donor_Campaign",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_Campaign_UsersRegistrationID",
                table: "Donor_Campaign",
                column: "UsersRegistrationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donor_Campaign");

            migrationBuilder.DropTable(
                name: "Campaign");
        }
    }
}
