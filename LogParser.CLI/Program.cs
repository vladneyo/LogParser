using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.NetworkInformation;
using LogParser.CLI.Commands;
using LogParser.CLI.Constants;
using LogParser.CLI.Services;

namespace LogParser.CLI
{
    class Program
    {
        /// <summary>
        /// Entry point of CLI
        /// </summary>
        /// <param name="args">
        /// [0] - log type (-a, -e) / --help
        /// [1] - file path
        /// </param>
        static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentException("No arguments passed");
            }

            Stopwatch watch = new Stopwatch();
            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings[AppSettingsConstants.PingBeforeSend]))
                {
                    var endpoint = ConfigurationManager.AppSettings[AppSettingsConstants.HostEndpoint];
                    if (!ServerAvailabilityService.IsHostAvailable(endpoint))
                    {
                        throw new PingException($"{endpoint} is unavailable");
                    }
                }

                CommandInvoker invoker = new CommandInvoker();
                invoker.Configure();
                watch.Start();
                var resultTask = invoker.Invoke(args[0], args.Length < 2 ? null : args[1]);
                resultTask.Wait();
                watch.Stop();
                if (!resultTask.IsCanceled && !resultTask.IsFaulted)
                {
                    Console.WriteLine("Succeeded");
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Errors");
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Completed in {watch.ElapsedMilliseconds / 1000} sec");            
        }
    }

    
}
