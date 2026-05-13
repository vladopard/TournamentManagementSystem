using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TournamentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    OrganizerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.OrganizerId);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SportType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OrganizerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                    table.CheckConstraint("CK_Tournament_Dates", "[StartDate] < [EndDate]");
                    table.ForeignKey(
                        name: "FK_Tournaments_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "OrganizerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Coach = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScoreHome = table.Column<int>(type: "INTEGER", nullable: false),
                    ScoreAway = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.CheckConstraint("CK_Match_Dates", "[StartDate] < [EndDate]");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsScored = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchRating = table.Column<int>(type: "INTEGER", nullable: false),
                    FoulsCommited = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_Results_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "OrganizerId", "ContactInfo", "Name" },
                values: new object[,]
                {
                    { 1, "contact@globalsports.org", "Global Sports Org" },
                    { 2, "info@eliteevents.com", "Elite Events" }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "TournamentId", "EndDate", "Location", "Name", "OrganizerId", "SportType", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "New York", "Champions Cup", 1, "Basketball", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Chicago", "Winter League", 2, "Basketball", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "Coach", "Name", "TournamentId" },
                values: new object[,]
                {
                    { 1, "Coach Carter", "NY Eagles", 1 },
                    { 2, "Phil Jackson", "LA Hawks", 1 },
                    { 3, "Mike Brown", "Chicago Bears", 2 },
                    { 4, "Rick Johnson", "Miami Sharks", 2 }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "AwayTeamId", "EndDate", "HomeTeamId", "ScoreAway", "ScoreHome", "StartDate", "TeamId", "TournamentId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, 74, 78, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, 1 },
                    { 2, 4, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 92, 88, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1995, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "John", "Doe", "Guard", 1 },
                    { 2, new DateTime(1992, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Mike", "Smith", "Forward", 1 },
                    { 3, new DateTime(1990, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Tom", "Brown", "Center", 2 },
                    { 4, new DateTime(1994, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), "James", "White", "Guard", 2 },
                    { 5, new DateTime(1991, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Chris", "Green", "Forward", 3 },
                    { 6, new DateTime(1993, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Steve", "Blue", "Center", 4 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "MatchId", "PlayerId", "FoulsCommited", "MatchRating", "PointsScored" },
                values: new object[,]
                {
                    { 1, 1, 2, 8, 20 },
                    { 1, 2, 1, 7, 15 },
                    { 1, 3, 3, 7, 18 },
                    { 1, 4, 2, 9, 21 },
                    { 2, 5, 2, 9, 22 },
                    { 2, 6, 1, 10, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlayerId",
                table: "Results",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TournamentId",
                table: "Teams",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganizerId",
                table: "Tournaments",
                column: "OrganizerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Organizers");
        }
    }
}
