
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum SpecialAttack { Bloque, Oneshot, JuddyPower }

    //BLOQUE : Defense = 
    //L’ordonnateur des pompes funèbres - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Jarvan cinquième du nom           - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Jeff Radis                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque
    //Chris Hart                        - A : 0.33 | D : 0.33 | S : 0.33 -> 0.3 bloque attaque

    //ONESHOT :
    //Bret Benoit                       - A : 0.33 | D : 0.33 | S : 0.33 -> 0.08 HP adversaire = 0 Non

    //JudyPower :
    //Judy Sunny - A : 0.33 | D : 0.33 | S : 0.33 -> 0.6 pare pour def + 1 Oui
    //                                            -> 0.4 +5HP Non

    class SpecialAttackManager
    {
        public static SpecialAttackManager instance = new SpecialAttackManager();


        public void SpecialAttackComputing(Catcheur attaquant)
        {
            List<Object> whatToDo = new List<Object>();
            Boolean specialAttackSuccess;
            int specialAttackSelected;
            switch (attaquant.SpecialAtk)
            {
                case SpecialAttack.Bloque:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.Bloque, new int[] { 30 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (int)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        attaquant.BonusDefense = int.MaxValue - 100;
                        attaquant.action = CatcheurAction.Defend;
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                case SpecialAttack.Oneshot:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.Oneshot, new int[] { 8 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (int)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        attaquant.BonusAttack = int.MaxValue - 100;
                        attaquant.action = CatcheurAction.Attack;
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                case SpecialAttack.JuddyPower:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.JuddyPower, new int[] { 40, 60 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (int)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        if(specialAttackSelected == 40)
                        {
                            attaquant.action = CatcheurAction.Heal;
                            attaquant.BonusHeal = 5;
                        }
                        else if (specialAttackSelected == 60)
                        {
                            attaquant.action = CatcheurAction.Defend;
                            attaquant.BonusDefense += 1; // +1 DEF
                        }

                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                
                

            }
        }

        private List<Object> AttackSpecialSuccess(string catcheurPseudo, SpecialAttack specialAtk, int[] rates)
        { 
            List<Object> whatToDo = new List<Object>();
            int Count = 1;
            foreach (int successRate in rates)
            {
                int random = MatchManager.dice.Next(1, 101);
                if (random >= 1 && random <= successRate)
                {
                    Console.WriteLine($"L'attaque spéciale [{specialAtk}] de {catcheurPseudo} a réussi !");
                    whatToDo.Add(true);
                    whatToDo.Add(successRate);
                    return whatToDo;
                }
                else
                {
                    if(Count == rates.Length)
                    {
                        Console.WriteLine($"L'attaque spéciale [{specialAtk}] de {catcheurPseudo} a échoué...");
                        whatToDo.Add(false);
                        whatToDo.Add(0);
                        return whatToDo;
                    }
                }
                Count++;
            }
            return whatToDo;
            
        }


    }
}
