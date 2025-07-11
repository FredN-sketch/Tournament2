using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Data.Data;

public static class SeedData
{
    public static List<TournamentDetails> GenerateTournaments()
    {
        string tournamentTitle;
        var tournaments = new List<TournamentDetails>();
        for (int i = 0; i < 10; i++) 
        {
            tournamentTitle = $"Spring Championship {i}";
            tournaments.Add(new TournamentDetails
            {

                Title = tournamentTitle,
                StartDate = DateTime.UtcNow.AddDays(-90+i),
                Games = GenerateGames(tournamentTitle)
            });
            tournamentTitle = $"Summer Showdown {i}";
            tournaments.Add(new TournamentDetails
            {
                Title = tournamentTitle,
                StartDate = DateTime.UtcNow.AddDays(-30+i),
                Games = GenerateGames(tournamentTitle)
            });
            tournamentTitle = $"Autumn Clash {i}";
            tournaments.Add(new TournamentDetails
            {
                Title = tournamentTitle,
                StartDate = DateTime.UtcNow.AddDays(60+i),
                Games = GenerateGames(tournamentTitle)
            });
            tournamentTitle = $"Winter Whiplash {i}";
            tournaments.Add(new TournamentDetails
            {
                Title = tournamentTitle,
                StartDate = DateTime.UtcNow.AddDays(90 + i),
                Games = GenerateGames(tournamentTitle)
            });
        }

            //{              
            //    new TournamentDetails
            //    {
                   
            //        Title = "Spring Championship",                           
            //        StartDate = DateTime.UtcNow.AddDays(-60),
            //        Games = GenerateGames("Spring Championship")
            //    },
            //    new TournamentDetails
            //    {
            //        Title = "Summer Showdown",                           
            //        StartDate = DateTime.UtcNow.AddDays(0), 
            //        Games = GenerateGames("Summer Showdown")
            //    },
            //    new TournamentDetails
            //    {
            //        Title = "Autumn Clash",                           
            //        StartDate = DateTime.UtcNow.AddDays(90),
            //        Games = GenerateGames("Autumn Clash")
            //    },
            //};
        return tournaments;
    }
    public static List<Game> GenerateGames(string tournamentTitle)
    {
        var games = new List<Game>();
        for (int i = 1; i < 10; i++)
        {
            games.Add(new Game
            {
                Title = $"{tournamentTitle} Game {i}",
                Time = DateTime.UtcNow.AddDays(i * 10) // Staggering game times for demonstration
            });
        }
        //{
        //    new Game
        //    {
        //        Title = tournamentTitle + " Game 1",
        //        Time = DateTime.UtcNow
        //    },
        //     new Game
        //    {
        //        Title = tournamentTitle + " Game 2",
        //        Time = DateTime.UtcNow
        //    },
        //      new Game
        //    {
        //        Title = tournamentTitle + " Game 3",
        //        Time = DateTime.UtcNow
        //    }
        //};
        return games;
    }

}
