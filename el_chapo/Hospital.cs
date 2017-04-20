using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class Hospital
    {
        public static void SetConvalAndHeal(Catcheur winner, Catcheur looser) // on heal le gagnant puis on verifie la vie du perdant et on lui change son état si besoin 
        {
            HealWinner(winner);
            if (looser.Health < (looser.maxHealth / 2))
            {
                looser.DayRemainingBeforeOp = MatchManager.instance.dice.Next(2, 6);
                looser.CatcheurState = CatcheurState.Convalescent;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Le perdant {looser.Pseudo} part en convalescence pour {looser.DayRemainingBeforeOp} jours suite à ses blessures...\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Le perdant {looser.Pseudo} est assez en forme pour continuer la saison !\n");
                Console.ResetColor();
            }
        }

        public static void HealWinner(Catcheur winner) // heal le gagnant
        {
            winner.Health = winner.maxHealth;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Le gagnant {winner.Pseudo} a été soigné de la totalité de ses points de vie !");
            Console.ResetColor();
            UpdateConvalAndChangeState();
        }

        public static void  UpdateConvalAndChangeState() // gere le temps de convalescence des catcheurs
        {
            foreach (Catcheur catcheur in Contacts.Catcheurs)
            {
                if (catcheur.CatcheurState == CatcheurState.Convalescent && catcheur.DayRemainingBeforeOp >= 0)
                {
                    if (catcheur.DayRemainingBeforeOp > 0)
                    {
                        catcheur.DayRemainingBeforeOp -= 1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{catcheur.Pseudo} peut de nouveau combattre !");
                        Console.ResetColor();
                        catcheur.CatcheurState = CatcheurState.Opérationnel;
                        catcheur.Health = catcheur.maxHealth;
                    }
                }
            }
        }

        public static Boolean IsEveryoneDead() // fonction qui permet de verifier si il reste des catcheurs opérationel 
        {
            List<Catcheur> isNotDead = new List<Catcheur>();
            foreach (Catcheur catcheur in Contacts.Catcheurs)
            {

                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                {
                    isNotDead.Add(catcheur);
                }

            }

            if (isNotDead.Count <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
