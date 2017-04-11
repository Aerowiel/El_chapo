using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{

    class MoneyManager
    {
        public static MoneyManager instance = new MoneyManager();
        public double Money { get; set; }

        public MoneyManager()
        {
            //Money = 0;
        }

        public double UpdateMoney(int iteration, Boolean someoneIsDead)
        {
            double moneyEarnedThisRound;
            if (someoneIsDead)
            {
                moneyEarnedThisRound = ((iteration * 5000) + 10000) * (Math.Pow(1.13, MatchManager.instance.Season));
                Money += moneyEarnedThisRound;
            }
            else
            {
                moneyEarnedThisRound = ((iteration * 5000) + 1000) * (Math.Pow(1.13, MatchManager.instance.Season));
                Money += moneyEarnedThisRound;
            }
            return moneyEarnedThisRound;
        }

        public void ColorAndDisplayMoney(double matchMoney, double totalMoney)
        {

            Console.Write("Le match vous a rapporté  : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(matchMoney + "$ ");
            Console.ResetColor();
            Console.Write($"Votre fortune s'élève a   : ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(totalMoney + "$\n");
            Console.ResetColor();
        }

    }
}
