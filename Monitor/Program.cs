using System;

namespace Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Monitor.GetArguments(args, out string processName, out int lifeTime, out int timeToCheck);
                Monitor monitor = new(processName, lifeTime, timeToCheck);
                monitor.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
