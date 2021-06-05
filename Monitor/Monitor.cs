using System;
using System.Diagnostics;
using System.Threading;

namespace Monitor
{
    public class Monitor
    {
        public string ProcessName { get; set; }
        public int AllowedLifeTime { get; set; }
        public int TimeToCheck { get; set; }
        public string ErrorMessage { get; set; }

        public Monitor(string name, int allowedTime, int time)
        {
            ProcessName = name;
            AllowedLifeTime = allowedTime;
            TimeToCheck = time;
            ErrorMessage = "To start the program, you must enter three parameters separated by a space.The first is the name of the process, the second is the allowed lifetime(in minutes and more then 0), and the third is the check frequency(in minutes and more then 0).";
        }

        public static void GetArguments(string[] args, out string processName, out int lifeTime, out int timeToCheck)
        {
            processName = args[0];
            lifeTime = int.Parse(args[1]);
            timeToCheck = int.Parse(args[2]);
        }

        public void Start()
        {
            try
            {
                if (AllowedLifeTime <= 0 || TimeToCheck <= 0)
                {
                    throw new FormatException(ErrorMessage);
                }
                else
                {
                    Process[] result = Process.GetProcessesByName(ProcessName);
                    if (result.Length == 0)
                    {
                        Console.WriteLine($"The process is not found");
                        return;
                    }

                    while (result.Length > 0)
                    {
                        KillProcesses(result);
                        result = Process.GetProcessesByName(ProcessName);
                        Boolean empty = IsEmpty(result);

                        if (!empty)
                        {
                            int timeToCheck = GetMlSeconds(TimeToCheck);
                            Console.WriteLine($"Found {result.Length} processes. Allowed time is not exceeded. Next check will be after {TimeToCheck} min");
                            Thread.Sleep(timeToCheck);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private void KillProcesses(Array array)
        {
            foreach (Process item in array)
            {
                double actualLifeTime = GetLifeMinutes(item.StartTime);

                if (actualLifeTime > AllowedLifeTime)
                {
                    item.Kill(true);
                    Console.WriteLine($"Allowed time is exceeded.The {ProcessName} process was stopped.");
                }
            }
        }
        public double GetLifeMinutes(DateTime startTime)
        {
            double difference = (DateTime.Now - startTime).TotalMinutes;
            return difference;
        }
        public Boolean IsEmpty(Array array)
        {
            if (array.Length == 0)
            {
                return true;
            }
            return false;
        }
        public int GetMlSeconds(int time)
        {
            int mlSeconds = time * 60 * 1000;
            return mlSeconds;
        }
    }
}
