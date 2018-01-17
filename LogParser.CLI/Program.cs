using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogParser.CLI.Commands;
using LogParser.CLI.Constants;

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
            CommandInvoker invoker = new CommandInvoker();
            invoker.Configure();
            try
            {
                var resultTask = invoker.Invoke(args[0], args.Length < 2 ? null : args[1]);
                resultTask.Wait();
                Console.WriteLine("Succeeded");
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
        }
    }

    
}
