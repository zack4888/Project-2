using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SuperBowlWinner> SuperBowlWinners;
            SuperBowlWinners = ReadFile();
            WriteFile(SuperBowlWinners);


        }
        public static List<SuperBowlWinner> ReadFile()
        {
            //Declarations
            List<SuperBowlWinner> SuperBowlWinners = new List<SuperBowlWinner>();
            SuperBowlWinner ASuperbowlwinner;
            const string FilePath = @"Super_Bowl_Project.csv";
            string[] Values;
            try
            {
                FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                StreamReader Reader = new StreamReader(file);

                while(!Reader.EndOfStream)
                {
                    Values = Reader.ReadLine().Split(',');
                    if (Values[0] != "Date")
                    {
                        ASuperbowlwinner = new SuperBowlWinner(Values[0], Values[1], Convert.ToInt32(Values[2]), Values[3], Values[4], Values[5], Convert.ToInt32(Values[6]), Values[7], Values[8], Values[9], Convert.ToInt32(Values[10]), Values[11], Values[12], Values[13], Values[14], Convert.ToInt32(Values[6]) - Convert.ToInt32(Values[10]));
                        SuperBowlWinners.Add(ASuperbowlwinner);
                    }
                   
                }
                file.Close();
                Reader.Close();
            }
            catch(Exception i)
            {
                Console.WriteLine(i);
            }
            return SuperBowlWinners;
        }
        public static string EnterPath()
        {
            string FilePath = @"";
            Console.WriteLine("Welcome to the super bowl data organizer");
            Console.WriteLine("Please enter a valid file path and name for your txt file you do not need to include .txt");
            FilePath = FilePath + Console.ReadLine();
            return FilePath;
        }
        public static void WriteFile(List<SuperBowlWinner> SuperBowlWinners)
        {
           
            string FileName = EnterPath() + ".txt";
            try
            {
                FileStream file2 = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                StreamWriter Writer = new StreamWriter(file2);
                WinningTeams(SuperBowlWinners, Writer);
                Top5Attended(SuperBowlWinners, Writer);
                MostHosted(SuperBowlWinners, Writer);
                MVP(SuperBowlWinners, Writer);
                MostLosingCoach(SuperBowlWinners, Writer);
                MostWinningCoach(SuperBowlWinners, Writer);
                MostWinningTeam(SuperBowlWinners, Writer);
                MostLosingTeam(SuperBowlWinners, Writer);
                GreatestPointDiff(SuperBowlWinners, Writer);
                AverageAttendance(SuperBowlWinners, Writer);

                Writer.Close();
                file2.Close();
            }
            catch(DirectoryNotFoundException)
            {
                Console.WriteLine("The Directory you selected does not exsist");
                Console.WriteLine("Press any key to end the program");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("an unexpected error has occured press any key to end the program");
                Console.ReadKey();
            }
           
        }
        public static void MostWinningTeam(List<SuperBowlWinner> SuperBowlWinners, StreamWriter Writer)
        {
            var MostWins = (
              from i in SuperBowlWinners
              group i by i.WinningTeam into Winners
              where Winners.Count() > 2
              orderby Winners.Count() descending
              select Winners).Take(1);

            foreach (var e in MostWins)
            {
                Writer.WriteLine("The Team who Won the most super bowls is {0}.", e.Key);

                Writer.WriteLine();
            }
        }
        public static void MostLosingTeam(List<SuperBowlWinner> SuperBowlWinners, StreamWriter Writer)
        {
            var MostLosses = (
               from i in SuperBowlWinners
               group i by i.LosingTeam into Losers
               where Losers.Count() > 2
               orderby Losers.Count() descending
               select Losers).Take(1);

            foreach (var e in MostLosses)
            {
                Writer.WriteLine("The team who lost the most super bowls is {0}.", e.Key);

                Writer.WriteLine();
            }
        }
        public static void GreatestPointDiff(List<SuperBowlWinner> SuperBowlWinners, StreamWriter Writer)
        {
            int MaxPointDiff = 0;
            SuperBowlWinner MaxDiff = SuperBowlWinners[1];
           

            foreach(var i in SuperBowlWinners)
            {
                if(i.PointDiff > MaxPointDiff)
                {
                    MaxPointDiff = i.PointDiff;
                    MaxDiff = i;
                }
            }
            Writer.WriteLine("The game with the greatest point difference was Super Bowl #: {0} \n with the {1} beating the {2} {3} to {4}", MaxDiff.SBNumber,MaxDiff.WinningTeam,MaxDiff.LosingTeam,MaxDiff.WinningPoints,MaxDiff.LosingPoints);

        }
        public static void AverageAttendance(List<SuperBowlWinner> SuperBowlWinners, StreamWriter Writer)
        {
            double AverageAttendance=0;
            int Counting = 0;
            foreach(var i in SuperBowlWinners)
            {
                AverageAttendance = AverageAttendance + i.Attendees;
                Counting++;
            }
            AverageAttendance = AverageAttendance / Counting;
            Writer.WriteLine();
            Writer.WriteLine("The Average attendance of all Super Bowls is {0}", AverageAttendance);
        }
        public static void MostWinningCoach(List<SuperBowlWinner> SuperBowlWinners, StreamWriter Writer)
        {
            var MostWins = (
               from i in SuperBowlWinners
               group i by i.CoachWinner into Winners
               where Winners.Count() > 2
               orderby Winners.Count() descending
               select Winners).Take(1);

            foreach (var e in MostWins)
            {
                Writer.WriteLine("The Coach who Won the most super bowls is {0}.", e.Key);

                Writer.WriteLine();
            }
        }
        public static void MostLosingCoach(List<SuperBowlWinner> SuperBowlWinners,StreamWriter Writer)
        {
            var MostLosses =(
                from i in SuperBowlWinners
                group i by i.CoachLoser into Losers
                where Losers.Count() > 2
                orderby Losers.Count() descending
                select Losers).Take(1);

            foreach (var e in MostLosses)
            {
                Writer.WriteLine("The Coach who lost the most super bowls is {0}.",e.Key);

                Writer.WriteLine();
            }

        }
        public static void WinningTeams(List<SuperBowlWinner> SuperBowlWinners,StreamWriter Writer)
        {
            Writer.WriteLine();
            Writer.WriteLine("             Winning Teams            \n");
            foreach (SuperBowlWinner i in SuperBowlWinners)
            {
                
                Writer.WriteLine("Winning Team: {0,-25} Date: {1,-12} Winning QuarterBack: {2,-30} Winning coach: {3,-20} MVP: {4,-28} Point Difference: {5,-5}", i.WinningTeam, i.Date, i.QBWinner, i.CoachWinner, i.MVP, i.WinningPoints - i.LosingPoints);
            }

        }
        public static void Top5Attended(List<SuperBowlWinner> SuperBowlWinners,StreamWriter Writer)
        {
            string[] DateSplit;
            int Year;
            List<SuperBowlWinner> Top5Attended = new List<SuperBowlWinner>();

            var Top5 =(
                from i in SuperBowlWinners
                where i.Attendees != 0
                orderby i.Attendees descending
                select i).Take(5);
            Writer.WriteLine();
            Writer.WriteLine("\n                       Top 5 Attended Super Bowls:\n");
            foreach(var x in Top5)
            {
                DateSplit = x.Date.Split('-');
                if(Convert.ToInt32(DateSplit[2]) > 17)
                {
                    Year = Convert.ToInt32(DateSplit[2]) + 1900;
                }
                else
                {
                    Year = Convert.ToInt32(DateSplit[2]) + 2000;
                }
                Writer.WriteLine("Year: {0,-5} Winning Team: {1,-25} Losing team: {2,-25} City: {3,-15} State: {4,-15} Stadium: {5,-20}", Year,x.WinningTeam,x.LosingTeam,x.City,x.State,x.Stadium);
            }

        }
        public static void MostHosted(List<SuperBowlWinner> SuperBowlWinners,StreamWriter Writer)
        {
            var MostHosted =
                from i in SuperBowlWinners
                group i by i.State into igroup
                where igroup.Count() > 2
                orderby igroup.Count() descending
                select igroup;


            Writer.WriteLine();
            Writer.WriteLine("           States that host the super bowl the most:");

            
           
            foreach(var e in MostHosted)
            {
                Writer.WriteLine(e.Key);
                foreach(var i in e)
                {
                    Writer.WriteLine(i);
                }
                Writer.WriteLine();
            }
     
        }
        public static void MVP(List<SuperBowlWinner> SuperBowlWinners,StreamWriter Writer)
        {
            var TopMVP =
                from i in SuperBowlWinners
                group i by i.MVP into MVPGroup
                where MVPGroup.Count() > 2
                orderby MVPGroup.Count() descending
                select MVPGroup;

            Writer.WriteLine();
            Writer.WriteLine("         Winningest MVP's");

            foreach(var e in TopMVP)
            {
                Writer.WriteLine(e.Key);
                foreach(var i in e)
                {
                    Writer.WriteLine("MVP: {0,-15} Winning Team: {1,-20} Losing Team: {2,-20}",i.MVP,i.WinningTeam,i.LosingTeam);

                }
                Writer.WriteLine();
            }
        }
    }
    class SuperBowlWinner
    {
        public string Date { get; set; }

        public int Attendees { get; set; }
        public string SBNumber { get; set; }
        public string QBWinner { get; set; }
        public string Stadium { get; set; }
        public string CoachWinner { get; set; }

        public string WinningTeam { get; set; }
        public string QBLoser { get; set; }
        public string CoachLoser { get; set; }

        public string LosingTeam { get; set; }
        public int WinningPoints { get; set; }
        public int LosingPoints { get; set; }
        public int PointDiff { get; set; }
        public string MVP { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public SuperBowlWinner(string Date,string SBNumber,int Attendees,string QBWinner,string CoachWinner,string WinningTeam, int WinningPoints, string QBLoser, string CoachLoser,string LosingTeam,int LosingPoints,string MVP,string Stadium,string City,string State,int PointDiff)
        {
            this.Date = Date;
            this.Attendees = Attendees;
            this.QBWinner = QBWinner;
            this.CoachWinner = CoachWinner;
            this.WinningTeam = WinningTeam;
            this.QBLoser = QBLoser;
            this.CoachLoser = CoachLoser;
            this.LosingTeam = LosingTeam;
            this.WinningPoints = WinningPoints;
            this.LosingPoints = LosingPoints;
            this.MVP = MVP;
            this.SBNumber = SBNumber;
            this.Stadium = Stadium;
            this.City = City;
            this.State = State;
            this.PointDiff = PointDiff;

          
        }
        public override string ToString()
        {
            return String.Format("City: {0,-15} State: {1,-15} Stadium: {2,-15} ",City,State,Stadium);
        }

    }
}
