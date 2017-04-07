using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum SpecialAttack { Bloque, }

    //BLOQUE : Defense = 
    //L’ordonnateur des pompes funèbres - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Jarvan cinquième du nom           - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Jeff Radis                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Chris Hart                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque

    class SpecialAttackManager
    {
        public static SpecialAttackManager instance = new SpecialAttackManager();


        public void SpecialAttackComputing(Catcheur attaquant)
        {
            switch (attaquant.SpecialAtk)
            {
                case SpecialAttack.Bloque:
                    // rand entre 1 et 10 si 1 <= random <= 3 alors réussite !
                    int random = MatchManager.dice.Next(1, 10);
                    if(random >= 1 && random <= 3)
                    {
                        attaquant.BonusDefense = int.MaxValue/2;
                        Console.WriteLine("L'attaque spécial a réussi !");
                        attaquant.action = CatcheurAction.Defend;
                    }
                    else
                    {
                        Console.WriteLine("L'attaque spécial a raté !");
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;

            }
        }


    }
}
