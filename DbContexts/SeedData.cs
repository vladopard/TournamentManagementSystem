using Microsoft.EntityFrameworkCore;
using TournamentManagementSystem.Entities;
using System;

namespace TournamentManagementSystem.DbContexts
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organizer>().HasData(
                new Organizer { OrganizerId = 1, Name = "Global Sports Org", ContactInfo = "contact@globalsports.org" },
                new Organizer { OrganizerId = 2, Name = "Elite Events", ContactInfo = "info@eliteevents.com" }
            );

            modelBuilder.Entity<Tournament>().HasData(
                new Tournament
                {
                    TournamentId = 1,
                    Name = "Champions Cup",
                    StartDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    Location = "New York",
                    SportType = "Basketball",
                    OrganizerId = 1
                },
                new Tournament
                {
                    TournamentId = 2,
                    Name = "Winter League",
                    StartDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    Location = "Chicago",
                    SportType = "Basketball",
                    OrganizerId = 2
                }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team { TeamId = 1, Name = "NY Eagles", Coach = "Coach Carter", TournamentId = 1 },
                new Team { TeamId = 2, Name = "LA Hawks", Coach = "Phil Jackson", TournamentId = 1 },
                new Team { TeamId = 3, Name = "Chicago Bears", Coach = "Mike Brown", TournamentId = 2 },
                new Team { TeamId = 4, Name = "Miami Sharks", Coach = "Rick Johnson", TournamentId = 2 }
            );

            modelBuilder.Entity<Player>().HasData(
                new Player { PlayerId = 1, FirstName = "John", LastName = "Doe", Position = "Guard", DateOfBirth = new DateTime(1995, 4, 10, 0, 0, 0, DateTimeKind.Utc), TeamId = 1 },
                new Player { PlayerId = 2, FirstName = "Mike", LastName = "Smith", Position = "Forward", DateOfBirth = new DateTime(1992, 8, 15, 0, 0, 0, DateTimeKind.Utc), TeamId = 1 },
                new Player { PlayerId = 3, FirstName = "Tom", LastName = "Brown", Position = "Center", DateOfBirth = new DateTime(1990, 3, 20, 0, 0, 0, DateTimeKind.Utc), TeamId = 2 },
                new Player { PlayerId = 4, FirstName = "James", LastName = "White", Position = "Guard", DateOfBirth = new DateTime(1994, 7, 25, 0, 0, 0, DateTimeKind.Utc), TeamId = 2 },
                new Player { PlayerId = 5, FirstName = "Chris", LastName = "Green", Position = "Forward", DateOfBirth = new DateTime(1991, 5, 11, 0, 0, 0, DateTimeKind.Utc), TeamId = 3 },
                new Player { PlayerId = 6, FirstName = "Steve", LastName = "Blue", Position = "Center", DateOfBirth = new DateTime(1993, 9, 17, 0, 0, 0, DateTimeKind.Utc), TeamId = 4 }
            );

            modelBuilder.Entity<Match>().HasData(
                new Match
                {
                    MatchId = 1,
                    StartDate = new DateTime(2025, 5, 5, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 6, 5, 0, 0, 0, DateTimeKind.Utc),
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ScoreHome = 78,
                    ScoreAway = 74,
                    TournamentId = 1
                },
                new Match
                {
                    MatchId = 2,
                    StartDate = new DateTime(2025, 2, 10, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                    HomeTeamId = 3,
                    AwayTeamId = 4,
                    ScoreHome = 88,
                    ScoreAway = 92,
                    TournamentId = 2
                }
            );

            modelBuilder.Entity<PlayerMatchStats>().HasData(
                new { MatchId = 1, PlayerId = 1, PointsScored = 20, MatchRating = 8, FoulsCommited = 2 },
                new { MatchId = 1, PlayerId = 2, PointsScored = 15, MatchRating = 7, FoulsCommited = 1 },
                new { MatchId = 1, PlayerId = 3, PointsScored = 18, MatchRating = 7, FoulsCommited = 3 },
                new { MatchId = 1, PlayerId = 4, PointsScored = 21, MatchRating = 9, FoulsCommited = 2 },
                new { MatchId = 2, PlayerId = 5, PointsScored = 22, MatchRating = 9, FoulsCommited = 2 },
                new { MatchId = 2, PlayerId = 6, PointsScored = 25, MatchRating = 10, FoulsCommited = 1 }
            );
        }
    }
}
