using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] punctuation = { ':', '.' };
            while (true)
            {
                Console.Title = "Format as hh:mm:ss";
                string addition = "";
                int startSecs = 0, finishSecs = 0;
                //Close when no input
                if ((addition = GetInput("Enter start time: (above for format) ", out startSecs, punctuation)) == "")
                    break;
                Console.Title += "    Start time " + addition;
                if ((addition = GetInput("Enter finish time: (above for format)", out finishSecs, punctuation)) == "")
                    break;
                Console.Title += " Finish time " + addition;
                DisplayTimeTaken(startSecs, finishSecs);
            }
        }

        static void DisplayTimeTaken(int startSeconds, int finishSeconds)
        {
            int[] timeTaken = new int[3];
            int difference = finishSeconds - startSeconds;
            //If time is negative, finished before you began
            if (difference < 0)
            {
                Console.WriteLine("You finished before you began\n");
                return;
            }
            //Calculates hours from total seconds, then calculates seconds remainding
            timeTaken[0] = difference / 3600;
            difference %= 3600;
            //Calculates minutes from remaining seconds, then sets seconds
            timeTaken[1] = difference / 60;
            difference %= 60;
            timeTaken[2] = difference;
            //Displays time taken in appropriate format, with placeholder 0's
            Console.WriteLine("Time taken was {0}:{1}:{2}\n", timeTaken[0], String.Format("{0:d2}",timeTaken[1]), String.Format("{0:d2}",timeTaken[2]));
        }

        static string GetInput(string prompt, out int seconds, char[] punctuation)
        {
            seconds = 0;
            //Loops until has correct input
            while (seconds == 0)
            {
                bool error = false;
                //Asks user for input
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input == "")
                {
                    break;
                }
                //Removes colon/period and then checks that there are only numbers left
                char[] checkOnlyNum = input.Replace(":", "").Replace(".", "").ToCharArray();
                foreach (char c in checkOnlyNum)
                {
                    if (!char.IsNumber(c))
                    {
                        //Character was not number and therefore incorrect input
                        DisplayError();
                        error = true;
                        break;
                    }
                }
                //If time is wrong aka 134 seconds, instead of 14 seconds with another 2 minutes
                if (Convert.ToInt32(input.Split(punctuation)[1]) > 59 || Convert.ToInt32(input.Split(punctuation)[2]) > 59)
                {
                    DisplayError();
                    error = true;
                }
                if (!error)
                {
                    //Input was correct so calculate time in seconds for easy calculation.
                    seconds = 60 * (60 * Convert.ToInt32(input.Split(punctuation)[0]) + Convert.ToInt32(input.Split(punctuation)[1])) + Convert.ToInt32(input.Split(punctuation)[2]);
                    return input;
                }
            }
            return "";
        }

        static void DisplayError()
        {
            Console.WriteLine("Please type in correct input!");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
