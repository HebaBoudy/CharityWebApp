using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTutorial.Migrations
{
    /// <inheritdoc />
    public partial class updatedDonorCampaign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_Company_CompanyID",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Donor_Campaign_Campaign_CampaignId",
                table: "Donor_Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Donor_Campaign_UsersRegistration_UsersRegistrationID",
                table: "Donor_Campaign");

            migrationBuilder.DropIndex(
                name: "IX_Donor_Campaign_CampaignId",
                table: "Donor_Campaign");

            migrationBuilder.DropIndex(
                name: "IX_Donor_Campaign_UsersRegistrationID",
                table: "Donor_Campaign");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Donor_Campaign");

            migrationBuilder.DropColumn(
                name: "DonorID",
                table: "Donor_Campaign");

            migrationBuilder.RenameColumn(
                name: "UsersRegistrationID",
                table: "Donor_Campaign",
                newName: "amount");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Donor_Campaign",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Donor_Campaign",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Campaign",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Campaign",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_Company_CompanyID",
                table: "Campaign",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_Company_CompanyID",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Donor_Campaign");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Donor_Campaign");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Donor_Campaign",
                newName: "UsersRegistrationID");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Donor_Campaign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DonorID",
                table: "Donor_Campaign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Campaign",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Campaign",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donor_Campaign_CampaignId",
                table: "Donor_Campaign",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Donor_Campaign_UsersRegistrationID",
                table: "Donor_Campaign",
                column: "UsersRegistrationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_Company_CompanyID",
                table: "Campaign",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_Campaign_Campaign_CampaignId",
                table: "Donor_Campaign",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_Campaign_UsersRegistration_UsersRegistrationID",
                table: "Donor_Campaign",
                column: "UsersRegistrationID",
                principalTable: "UsersRegistration",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
