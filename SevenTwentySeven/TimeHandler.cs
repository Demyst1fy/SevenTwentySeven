using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;

namespace SevenTwentySeven
{
    public class TimeHandler
    {
        private DateTime _dateTime;
        private string DateTimeString { get; }

        public TimeHandler(DateTime dateTime, string cultureInfo = "en-US")
        {
            _dateTime = dateTime;
            string[] formats = _dateTime.GetDateTimeFormats('G', CultureInfo.GetCultureInfo(cultureInfo));
            DateTimeString = formats[0];
        }
        
        public static int UserInput()
        {
            var input = 9;
            Console.Write("\n1. Enter new Datetime\n" +
                          "2. Print the Timezones\n" +
                          "3. Print the saved Datetimes\n" +
                          "4. Print ASCII\n" +
                          "5. Quit\n" +
                          "Choose one menu point: ");
            
            while (input is < 1 or > 5)
            {
                if (!int.TryParse(Console.ReadLine(), out input) && input < 1 || input > 5) {
                    Console.Write("Unknown entry! Try again: ");
                }
            }

            return input;
        }
        
        public static DateTime EnterDateTime()
        {
            DateTime dateTime = DateTime.Now;
            var date = Array.Empty<string>();
            var time = Array.Empty<string>();

            int day = 0, month = 0, year = 0;
            int hour = -1, minute = -1, second = -1;
            
            Console.Write("Enter the due date [Format: DD MM YYYY]: ");
            
            try
            {
                string line;
                while (day is < 1 or > 31 || month is < 1 or > 12 || year is < 0 or > 9999 || date.Length < 3)
                {
                    line = Console.ReadLine();
                    if (line != null)
                        date = line.Split(' ');

                    if (date.Length < 3)
                    {
                        Console.Write("Invalid format! Try again: ");
                        continue;
                    }

                    day = Convert.ToInt32(date[0]);
                    month = Convert.ToInt32(date[1]);
                    year = Convert.ToInt32(date[2]);

                    if (day is < 1 or > 31 || month is < 1 or > 12 || year is < 0 or > 9999)
                    {
                        Console.Write("Invalid format! Try again: ");
                    }
                }

                Console.Write("Enter the time [Format: hh mm ss]: ");
                while (hour is < 0 or > 23 || minute is < 0 or > 59 || second is < 0 or > 59 || time.Length < 3)
                {
                    line = Console.ReadLine();
                    if (line != null)
                        time = line.Split(' ');

                    if (time.Length < 3)
                    {
                        Console.Write("Invalid format! Try again: ");
                        continue;
                    }

                    hour = Convert.ToInt32(time[0]);
                    minute = Convert.ToInt32(time[1]);
                    second = Convert.ToInt32(time[2]);

                    if (hour is < 0 or > 23 || minute is < 0 or > 59 || second is < 0 or > 59)
                    {
                        Console.Write("Invalid format! Try again: ");
                    }
                }

                dateTime = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return dateTime;
        }
        
        public static string EnterCulture()
        {
            var input = 9;
            Console.Write("\n1. en-US\n" +
                          "2. de-DE\n" +
                          "Choose one menu point: ");
            
            while (input is < 1 or > 2)
            {
                if (!int.TryParse(Console.ReadLine(), out input) && input < 1 || input > 2) {
                    Console.Write("Unknown entry! Try again: ");
                }
            }

            string culture = "en-US";
            if (input == 2)
            {
                culture = "de-DE";
            }

            return culture;
        }

        public static void PrintListDiffs(List<TimeHandler> times)
        {
            foreach (TimeHandler time in times)
            {
                time.PrintDiff(EDateTime.Tage, ConsoleColor.Red);
                time.PrintDiff(EDateTime.Stunden, ConsoleColor.Yellow);
                time.PrintDiff(EDateTime.Minuten, ConsoleColor.Green);
                time.PrintDiff(EDateTime.Sekunden, ConsoleColor.Blue);
                time.PrintDiff(EDateTime.Millisekunden, ConsoleColor.Cyan);
                Console.WriteLine();
            }
        }
        public void PrintDiff(EDateTime type, ConsoleColor color)
        {
            double diff = 0;
            
            string preposition = "In";
            string verb = "ist";

            switch (type)
            {
                case EDateTime.Tage:
                    diff = Math.Floor((_dateTime - DateTime.Now).TotalDays);
                    break;
                case EDateTime.Stunden:
                    diff = Math.Floor((_dateTime - DateTime.Now).TotalHours);
                    break;
                case EDateTime.Minuten:
                    diff = Math.Floor((_dateTime - DateTime.Now).TotalMinutes);
                    break;
                case EDateTime.Sekunden:
                    diff = Math.Floor((_dateTime - DateTime.Now).TotalSeconds);
                    break;
                case EDateTime.Millisekunden:
                    diff = Math.Floor((_dateTime - DateTime.Now).TotalMilliseconds);
                    break;
            }
            
            if (diff < 0)
            {
                diff = Math.Abs(diff);
                
                preposition = "Vor";
                verb = "war";
            }
            
            Console.Write($"{preposition} ");
            Console.ForegroundColor = color;
            Console.Write($"{diff,15} {type,-20}");
            Console.ResetColor();
            Console.WriteLine($" {verb} {DateTimeString,30}");
        }

        public static bool If727(int currentHour, int currentMinute)
        {
            return (currentHour is 6 or 16 && currentMinute > 27)||(currentHour is 7 or 17 or 19 && currentMinute < 27)||
                   (currentHour is 7 or 17 or 19 && currentMinute > 27)||(currentHour is 8 or 18 or 20 && currentMinute < 27)||
                   (currentHour is 18 && currentMinute > 27)||(currentHour is 7 or 17 or 19 && currentMinute == 27);
        }

        public static void PrintTimeZones()
        {
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                DateTime currentTimeZone = DateTime.Now.Add(z.BaseUtcOffset).AddHours(-1);
                string amOrPm = "";
                if (TimeHandler.If727(currentTimeZone.Hour, currentTimeZone.Minute))
                {
                    int currentHour = currentTimeZone.Hour;
                    switch (currentTimeZone.Hour)
                    {
                        case 6: case 7: case 8:
                            amOrPm = "AM";
                            break;
                        case 16: case 17: case 18:
                            amOrPm = "";
                            break;
                        case 19: case 20:
                            amOrPm = "PM";
                            currentHour %= 12;
                            break;
                    }
                    string place = z.DisplayName.Substring(12);
                    string output = "";
                    switch (currentHour)
                    {
                        case 6 or 16 when currentTimeZone.Minute > 27:
                            output = $"In {Math.Abs(27 - currentTimeZone.Minute + 60)} Minuten ist es in {place} {currentHour+1}:27 {amOrPm}";
                            break;
                        case 7 or 17 when currentTimeZone.Minute < 27:
                            output = $"In {Math.Abs(27 - currentTimeZone.Minute)} Minuten ist es in {place} {currentHour}:27 {amOrPm}";
                            break;
                        case 7 or 17 when currentTimeZone.Minute > 27:
                            output = $"Vor {Math.Abs(currentTimeZone.Minute - 27)} Minuten war es in {place} {currentHour}:27 {amOrPm}";
                            break;
                        case 8 or 18 when currentTimeZone.Minute < 27:
                            output = $"Vor {Math.Abs(currentTimeZone.Minute + 60 - 27)} Minuten war es in {place} {currentHour-1}:27 {amOrPm}";
                            break;
                        case 18 when currentTimeZone.Minute > 27:
                            amOrPm = "PM";
                            currentHour %= 12;
                            output = $"In {Math.Abs(27 - currentTimeZone.Minute + 60)} Minuten ist es in {place} {currentHour+1}:27 {amOrPm}";
                            break;
                        case 7 or 17 when currentTimeZone.Minute == 27:
                            output = $"Es ist grad in {place} {currentHour}:27 {amOrPm}";
                            break;
                    }
                    Console.WriteLine($"[{z.DisplayName}: {currentTimeZone}]");
                    Console.WriteLine($"{output}{Environment.NewLine}");
                }
            }
        }

        public static void PrintASCII()
        {
            Console.WriteLine(@".--------. .-----. .--------. ");
            Console.WriteLine(@"|   __   '/ ,-.   \|   __   ' ");
            Console.WriteLine(@"`--' .  / '-'  |  |`--' .  /  ");
            Console.WriteLine(@"    /  /     .'  /     /  /   ");
            Console.WriteLine(@"   .  /    .'  /__    .  /    ");
            Console.WriteLine(@"  /  /    |       |  /  /     ");
            Console.WriteLine(@" `--'     `-------' `--'      ");
            Console.WriteLine();
        }  
    }
}