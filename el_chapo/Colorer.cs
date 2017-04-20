using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class Colorer
    {
        public static void ColorAttack(Catcheur c) // affiche l'action "attack" en rouge
        {
            Console.Write($"\n{c.Pseudo} - {c.Health}HP - Action : ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(c.action + "\n");
            Console.ResetColor();

        }

        public static void ColorDefend(Catcheur c) // affiche l'action "defend" en  vert
        {
            Console.Write($"\n{c.Pseudo} - {c.Health}HP - Action : ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(c.action + "\n");
            Console.ResetColor();

        }

        public static void ColorAttackSpe(Catcheur c) // affiche l'action "AttackSpecial" en violet
        {
            Console.Write($"\n{c.Pseudo} - {c.Health}HP - Action : ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(c.action + "\n");
            Console.ResetColor();
        }
    }
}
