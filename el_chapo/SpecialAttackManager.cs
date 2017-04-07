
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum SpecialAttack { Bloque, Oneshot }

    //BLOQUE : Defense = 
    //L’ordonnateur des pompes funèbres - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Jarvan cinquième du nom           - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Jeff Radis                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Chris Hart                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque

    //ONESHOT :
    //Bret Benoit                       - A : 0.33 | D : 0.33 | S : 0.33 -> 0.08 HP adversaire = 0 Non

    class SpecialAttackManager
    {
        public static SpecialAttackManager instance = new SpecialAttackManager();


        public void SpecialAttackComputing(Catcheur attaquant)
        {
            int random;
            Console.WriteLine($"{attaquant.Pseudo} utilise le pouvoir {attaquant.SpecialAtk}");
            switch (attaquant.SpecialAtk)
            {
                case SpecialAttack.Bloque:
                    Console.WriteLine($"{attaquant.Pseudo} utilise BLOQUE");
                    // rand entre 1 et 10 si 1 <= random <= 3 alors réussite !
                    random = MatchManager.dice.Next(1, 10);
                    if(random >= 1 && random <= 3)
                    {
                        attaquant.BonusDefense = int.MaxValue/2;
                        Console.WriteLine($"L'attaque spécial de {attaquant.Pseudo} a réussi ! BLOCK !");

                        attaquant.action = CatcheurAction.Defend;
                    }
                    else
                    {
                        Console.WriteLine($"L'attaque spécial de {attaquant.Pseudo} a raté !");
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                case SpecialAttack.Oneshot:
                    random = MatchManager.dice.Next(1,101);
                    Console.WriteLine("random = " + random);
                    if(random >= 1 && random <= 8)
                    {
                        attaquant.BonusAttack = int.MaxValue / 2;
                        Console.WriteLine($"L'attaque spécial de {attaquant.Pseudo} a réussi ! ONESHOT ! -------------------------------------------------------------------------------");

                        attaquant.action = CatcheurAction.Attack;
                    }
                    else
                    {
                        Console.WriteLine($"L'attaque spécial de {attaquant.Pseudo} a raté ! ONESHOT RATE ! -------------------------------------------------------------------------------");
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;

            }
        }


    }
}
