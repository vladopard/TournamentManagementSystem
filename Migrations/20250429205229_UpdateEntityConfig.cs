using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_StartDate_EndDate_Name_Location_SportType",
                table: "Tournaments",
                columns: new[] { "StartDate", "EndDate", "Name", "Location", "SportType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name_TournamentId",
                table: "Teams",
                columns: new[] { "Name", "TournamentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_FirstName_LastName_DateOfBirth",
                table: "Players",
                columns: new[] { "FirstName", "LastName", "DateOfBirth" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_Name_ContactInfo",
                table: "Organizers",
                columns: new[] { "Name", "ContactInfo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StartDate_EndDate_HomeTeamId_AwayTeamId",
                table: "Matches",
                columns: new[] { "StartDate", "EndDate", "HomeTeamId", "AwayTeamId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tournaments_StartDate_EndDate_Name_Location_SportType",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Name_TournamentId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_FirstName_LastName_DateOfBirth",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Organizers_Name_ContactInfo",
                table: "Organizers");

            migrationBuilder.DropIndex(
                name: "IX_Matches_StartDate_EndDate_HomeTeamId_AwayTeamId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Matches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 1,
                column: "TeamId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Matches",
                keyColumn: "MatchId",
                keyValue: 2,
                column: "TeamId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamId",
                table: "Matches",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }
    }
}
