using LoggingEngine;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace RedisBackup
{
    public class Program
    {
        public static readonly Logger Log = new Logger(typeof(Program));
        public static readonly string ExecPath = Directory.GetCurrentDirectory();
        public static readonly Settings Settings = Settings.Load("settings.json");

        public static void Main(string[] args)
        {
            Console.Title = "Redis Backup";
            AppDomain.CurrentDomain.ProcessExit += Goodbye;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (!Directory.Exists(Settings.RedisFolder))
                throw new DirectoryNotFoundException(Settings.RedisFolder);
            if (!File.Exists(Settings.RedisFolder + "/dump.rdb"))
                throw new FileNotFoundException("dump.rdb doesn't exist");

            var dest = ExecPath + "/" + Settings.DestFolder;
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            Log.Info("Application successfully started.");

            LoopWatch();
        }

        private static void LoopWatch()
        {
            var sw = Stopwatch.StartNew();
            while (true)
            {
                if (sw.Elapsed.Minutes >= Settings.TimeLimit)
                {
                    if (!Directory.Exists(Settings.RedisFolder))
                        throw new DirectoryNotFoundException(Settings.RedisFolder);
                    if (!File.Exists(Settings.RedisFolder + "/dump.rdb"))
                        throw new FileNotFoundException("dump.rdb doesn't exist");

                    var fileName = $"dump {DateTime.Now.ToString().ToSafe()}.rdb";
                    var finalDir = Settings.DestFolder + $"/{DateTime.Today.ToShortDateString().ToSafe()}";
                    if (!Directory.Exists(finalDir))
                        Directory.CreateDirectory(finalDir);

                    var data = File.ReadAllBytes(Settings.RedisFolder + "/dump.rdb");
                    File.WriteAllBytes($"{finalDir}/{fileName}", data);
                    Log.Debug($"Backup created at {finalDir}/{fileName}");

                    sw.Restart();
                }
            }
        }

        private static void Goodbye(object sender, EventArgs args)
        {
            Log.Info("See you later.");
            Thread.Sleep(300);
        }
    }
}
