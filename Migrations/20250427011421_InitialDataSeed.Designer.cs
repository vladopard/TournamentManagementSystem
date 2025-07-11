﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentManagementSystem.DbContexts;

#nullable disable

namespace TournamentManagementSystem.Migrations
{
    [DbContext(typeof(TournamentDbContext))]
    [Migration("20250427011421_InitialDataSeed")]
    partial class InitialDataSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.15");

            modelBuilder.Entity("TournamentManagementSystem.Entities.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScoreAway")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScoreHome")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MatchId");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches", null, t =>
                        {
                            t.HasCheckConstraint("CK_Match_Dates", "\"StartDate\" < \"EndDate\"");
                        });

                    b.HasData(
                        new
                        {
                            MatchId = 1,
                            AwayTeamId = 2,
                            EndDate = new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc),
                            HomeTeamId = 1,
                            ScoreAway = 74,
                            ScoreHome = 78,
                            StartDate = new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc),
                            TournamentId = 1
                        },
                        new
                        {
                            MatchId = 2,
                            AwayTeamId = 4,
                            EndDate = new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            HomeTeamId = 3,
                            ScoreAway = 92,
                            ScoreHome = 88,
                            StartDate = new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            TournamentId = 2
                        });
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Organizer", b =>
                {
                    b.Property<int>("OrganizerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("OrganizerId");

                    b.ToTable("Organizers");

                    b.HasData(
                        new
                        {
                            OrganizerId = 1,
                            ContactInfo = "contact@globalsports.org",
                            Name = "Global Sports Org"
                        },
                        new
                        {
                            OrganizerId = 2,
                            ContactInfo = "info@eliteevents.com",
                            Name = "Elite Events"
                        });
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            DateOfBirth = new DateTime(1995, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "John",
                            LastName = "Doe",
                            Position = "Guard",
                            TeamId = 1
                        },
                        new
                        {
                            PlayerId = 2,
                            DateOfBirth = new DateTime(1992, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Mike",
                            LastName = "Smith",
                            Position = "Forward",
                            TeamId = 1
                        },
                        new
                        {
                            PlayerId = 3,
                            DateOfBirth = new DateTime(1990, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Tom",
                            LastName = "Brown",
                            Position = "Center",
                            TeamId = 2
                        },
                        new
                        {
                            PlayerId = 4,
                            DateOfBirth = new DateTime(1994, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "James",
                            LastName = "White",
                            Position = "Guard",
                            TeamId = 2
                        },
                        new
                        {
                            PlayerId = 5,
                            DateOfBirth = new DateTime(1991, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Chris",
                            LastName = "Green",
                            Position = "Forward",
                            TeamId = 3
                        },
                        new
                        {
                            PlayerId = 6,
                            DateOfBirth = new DateTime(1993, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Steve",
                            LastName = "Blue",
                            Position = "Center",
                            TeamId = 4
                        });
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.PlayerMatchStats", b =>
                {
                    b.Property<int>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoulsCommited")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatchRating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PointsScored")
                        .HasColumnType("INTEGER");

                    b.HasKey("MatchId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Results");

                    b.HasData(
                        new
                        {
                            MatchId = 1,
                            PlayerId = 1,
                            FoulsCommited = 2,
                            MatchRating = 8,
                            PointsScored = 20
                        },
                        new
                        {
                            MatchId = 1,
                            PlayerId = 2,
                            FoulsCommited = 1,
                            MatchRating = 7,
                            PointsScored = 15
                        },
                        new
                        {
                            MatchId = 1,
                            PlayerId = 3,
                            FoulsCommited = 3,
                            MatchRating = 7,
                            PointsScored = 18
                        },
                        new
                        {
                            MatchId = 1,
                            PlayerId = 4,
                            FoulsCommited = 2,
                            MatchRating = 9,
                            PointsScored = 21
                        },
                        new
                        {
                            MatchId = 2,
                            PlayerId = 5,
                            FoulsCommited = 2,
                            MatchRating = 9,
                            PointsScored = 22
                        },
                        new
                        {
                            MatchId = 2,
                            PlayerId = 6,
                            FoulsCommited = 1,
                            MatchRating = 10,
                            PointsScored = 25
                        });
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Coach")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            TeamId = 1,
                            Coach = "Coach Carter",
                            Name = "NY Eagles",
                            TournamentId = 1
                        },
                        new
                        {
                            TeamId = 2,
                            Coach = "Phil Jackson",
                            Name = "LA Hawks",
                            TournamentId = 1
                        },
                        new
                        {
                            TeamId = 3,
                            Coach = "Mike Brown",
                            Name = "Chicago Bears",
                            TournamentId = 2
                        },
                        new
                        {
                            TeamId = 4,
                            Coach = "Rick Johnson",
                            Name = "Miami Sharks",
                            TournamentId = 2
                        });
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SportType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("TournamentId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Tournaments", null, t =>
                        {
                            t.HasCheckConstraint("CK_Tournament_Dates", "\"StartDate\" < \"EndDate\"");
                        });

                    b.HasData(
                        new
                        {
                            TournamentId = 1,
                            EndDate = new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Location = "New York",
                            Name = "Champions Cup",
                            OrganizerId = 1,
                            SportType = "Basketball",
                            StartDate = new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            TournamentId = 2,
                            EndDate = new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc),
                            Location = "Chicago",
                            Name = "Winter League",
                            OrganizerId = 2,
                            SportType = "Basketball",
                            StartDate = new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Match", b =>
                {
                    b.HasOne("TournamentManagementSystem.Entities.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TournamentManagementSystem.Entities.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TournamentManagementSystem.Entities.Team", null)
                        .WithMany("Matches")
                        .HasForeignKey("TeamId");

                    b.HasOne("TournamentManagementSystem.Entities.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Player", b =>
                {
                    b.HasOne("TournamentManagementSystem.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.PlayerMatchStats", b =>
                {
                    b.HasOne("TournamentManagementSystem.Entities.Match", "Match")
                        .WithMany("PlayerStats")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentManagementSystem.Entities.Player", "Player")
                        .WithMany("MatchStats")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Team", b =>
                {
                    b.HasOne("TournamentManagementSystem.Entities.Tournament", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Tournament", b =>
                {
                    b.HasOne("TournamentManagementSystem.Entities.Organizer", "Organizer")
                        .WithMany("Tournaments")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Match", b =>
                {
                    b.Navigation("PlayerStats");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Organizer", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Player", b =>
                {
                    b.Navigation("MatchStats");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Team", b =>
                {
                    b.Navigation("AwayMatches");

                    b.Navigation("HomeMatches");

                    b.Navigation("Matches");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("TournamentManagementSystem.Entities.Tournament", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
