using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Server.Application
{
    class Application
    {
        static void Main(string[] args)
        {
            var serverCore = new Core();
            serverCore.Initialize(40404, 8);
            

            while(!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
            {
                serverCore.Run();
            }
        }
    }
}
