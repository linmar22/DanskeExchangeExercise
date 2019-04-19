using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeExercise.ExchangeLib.Controllers;
using ExchangeExercise.ExchangeLib.Util;

namespace ExchangeExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var bootstrapper = new WorkBootstrapper();
                Console.WriteLine(bootstrapper.Work(args));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}