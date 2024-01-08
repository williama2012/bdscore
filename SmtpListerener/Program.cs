using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using bds.web.smtp;
namespace SmtpListerener
{
    class Program {

        

        static void Main(string[] args)         {

            using (var listener = new SmtpListener()) {
                while (listener.IsAlive) { 
                    Thread.Sleep(500);
                    if (Console.KeyAvailable) { listener.Dispose(); }
                    
                }
            }

            Console.WriteLine("Done.");
            var wait = Console.ReadLine();

        }



    }
}
