using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace SevenTwentySeven
{
    class Program
    {
#if DEBUG
        static void Main(string[] args)
        {
            var menuInput = 9;
            List<TimeHandler> times = new List<TimeHandler>();

            while (menuInput != 5)
            {
                Console.WriteLine("Welcome to your WYSI-Timehandler!");
                Console.WriteLine("---------------------------------------");
                menuInput = TimeHandler.UserInput();
                Console.Clear();
                switch (menuInput)
                {
                    case 1:
                        DateTime datetime = TimeHandler.EnterDateTime();
                        string culture = TimeHandler.EnterCulture();
                        
                        times.Add(new TimeHandler(datetime, culture));
                        
                        Console.Clear();
                        TimeHandler.PrintListDiffs(times);
                        break;
                    
                    case 2:
                        TimeHandler.PrintTimeZones();
                        break;
                    
                    case 3:
                        TimeHandler.PrintListDiffs(times);
                        break;
                    
                    case 4:
                        TimeHandler.PrintASCII();
                        break;
                    case 5:
                        return;
                }
            }
        }
#endif
    }
}