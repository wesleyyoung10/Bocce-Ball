using Bocce_Ball.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Bocce_Ball.Models;

namespace Bocce_Ball
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BocceBallDataContext())
            {
           
                    db.Players.Add(new Player
                    {
                        FullName = "Wesley Young",
                        NickName = "Wes",
                        Number = 17,
                        ThrowingArm = "Right"

                    });
                    db.Players.Add(new Player
                    {
                        FullName = "John Doe",
                        NickName = "Jay",
                        Number = 16,
                        ThrowingArm = "Left"
                    });
                    db.SaveChanges();
                    db.Teams.Add(new Team
                    {
                        Mascot = "Hawks",
                        Color = "Red"
                    });
                    db.Teams.Add(new Team
                    {
                        Mascot = "Rams",
                        Color = "Blue"
                    });
                    db.SaveChanges();
                    var hawks = db.Teams.Include(t => t.Players).First(f => f.Mascot == "Hawks");
                    var playe0r = db.Players.First(p => p.FullName == "Wesley Young");
                    hawks.Players.Add(playe0r);

                    var rams = db.Teams.Include(c => c.Players).First(v => v.Mascot == "Rams");
                    var player2 = db.Players.First(x => x.FullName == "John Doe");
                    rams.Players.Add(player2);
                    db.SaveChanges();

                    db.Games.Add(new Game
                    {
                        HomeTeam = hawks,
                        AwayTeam = rams,
                        HomeScore = 42,
                        AwayScore = 8,
                        DateHappened = DateTime.Today,
                        Notes = "plz work"
                    });
                    db.Games.Add(new Game
                    {
                        HomeTeam = hawks,
                        AwayTeam = rams,
                        DateHappened = DateTime.MaxValue,
                        Notes = "why"
                    });
                    db.SaveChanges();
                

                Console.WriteLine("Printing all teams with their records, all players and their current teams, all upcoming games and past games.");

                var scores = db.Teams.Include(s => s.HomeGames).Include(s => s.AwayGames).Where(w => db.Games.Any(l => l.DateHappened < DateTime.Now));
                foreach (var score in scores)
                {
                    int wins = score.HomeGames.Where(g => g.HomeScore > 0 && g.HomeScore > g.AwayScore).Count();
                    int awayWins = score.AwayGames.Where(g => g.AwayScore > 0 && g.AwayScore > g.HomeScore).Count();
                    int homeCount = score.HomeGames.Where(g => g.DateHappened < DateTime.Now).Count();
                    int awayCount = score.AwayGames.Where(g => g.DateHappened < DateTime.Now).Count();

                    Console.WriteLine("Team {0}: {1} wins, {2} loses", score.Mascot, wins + awayWins, homeCount + awayCount - wins - awayWins);
                }
                var allPlayers = db.Players.Include(f => f.Team);
                foreach (var player in allPlayers)
                {
                    Console.WriteLine("{0}-{1}", player.FullName, player.Team.Mascot);
                }

                var allFutureGames = db.Games.Where(g => g.DateHappened > DateTime.Now);
                foreach (var game in allFutureGames)
                {
                    Console.WriteLine("{0}", game.DateHappened);
                }
                var allPastGames = db.Games.Where(y => y.DateHappened < DateTime.Now);
                foreach (var game in allPastGames)
                {
                    Console.WriteLine("{0}", game.DateHappened);
                }
                db.SaveChanges();
            }
            Console.WriteLine("IT WORKS!");
            Console.ReadLine();
        } 
    }
}




