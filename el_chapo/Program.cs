using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuManager menuManager = new MenuManager();
            menuManager.AskToLoadOrCreate();
            //menuManager.DisplayMenu();
            Console.ReadKey();
        }
    }
}
