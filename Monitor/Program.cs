using System;

namespace Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                GetArguments(args, out string processName, out int lifeTime, out int timeToCheck);
                Monitor monitor = new(processName, lifeTime, timeToCheck);
                monitor.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetArguments(string[] args, out string processName, out int lifeTime, out int timeToCheck)
        {
            processName = args[0];
            lifeTime = int.Parse(args[1]);
            timeToCheck = int.Parse(args[2]);
        }
    }
}
