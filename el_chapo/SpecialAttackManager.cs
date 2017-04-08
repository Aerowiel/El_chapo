
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum SpecialAttack { Bloque, Oneshot, JuddyPower, DeadPoulePower, HacheFoudroyante, OffensiveBlock, RaieDuC }

    class SpecialAttackManager
    {
        public static SpecialAttackManager instance = new SpecialAttackManager();


        public void SpecialAttackComputing(Catcheur attaquant)
        {
            List<Object> whatToDo = new List<Object>();
            Boolean specialAttackSuccess;
            string specialAttackSelected;
            switch (attaquant.SpecialAtk)
            {
                //BLOQUE : Defense 
                //L’ordonnateur des pompes funèbres  -> 0.3 bloque
                //Jarvan cinquième du nom            -> 0.3 bloque
                //Jeff Radis                         -> 0.3 bloque
                //Chris Hart                         -> 0.3 bloque
                case SpecialAttack.Bloque:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.Bloque, new int[] { 30 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        attaquant.BonusDefense = int.MaxValue - 1000; //apparement si int.MaxValue dépasse la max value justement le nombre devient négatif ! J'ai soustrait 1000 pour éviter ça :)
                        attaquant.action = CatcheurAction.Defend;
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                //ONESHOT : Attaque
                // Bret Benoit -> 0.08 tue en un coup
                case SpecialAttack.Oneshot:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.Oneshot, new int[] { 8 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        attaquant.BonusAttack = int.MaxValue - 1000;
                        attaquant.action = CatcheurAction.Attack;
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;

                //JudyPower : 
                //P1 -> 0.4 +5 hp ATTAQUE
                //P2 -> 0.6 def + 1 DEFEND
                case SpecialAttack.JuddyPower:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.JuddyPower, new int[] { 60, 40 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        if (specialAttackSelected == "P1")
                        {
                            attaquant.action = CatcheurAction.Defend;
                            attaquant.BonusDefense = 1; // +1 DEF
                        }
                        else if (specialAttackSelected == "P2")
                        {
                            attaquant.action = CatcheurAction.Heal;
                            attaquant.BonusHeal = 5;
                        }

                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;

                //DeadPoulePower : 
                //P1 -> 0.1 def + 1 DEFEND
                //P2 -> 0.1 inflige (3 HP + Attaque) se soigne 3HP ATTAQUE
                //P3 -> 0.3 +2HP ATTAQUE
                case SpecialAttack.DeadPoulePower:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.DeadPoulePower, new int[] { 30, 10, 10 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        if (specialAttackSelected == "P1")
                        {
                            attaquant.action = CatcheurAction.Heal;
                            attaquant.BonusHeal = 2;
                        }
                        else if (specialAttackSelected == "P2")
                        {
                            attaquant.action = CatcheurAction.Attack;
                            attaquant.BonusAttack = 3;
                        }
                        else if (specialAttackSelected == "P3")
                        {
                            attaquant.action = CatcheurAction.Defend;
                            attaquant.BonusDefense = 1;
                        }
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                //Triple Hache 
                //Hache Foudroyante : 0.2 inflige attaque +2dmg, perd 1HP
                case SpecialAttack.HacheFoudroyante:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.HacheFoudroyante, new int[] { 20 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        attaquant.action = CatcheurAction.Attack;
                        attaquant.BonusAttack = 2;
                        attaquant.DebuffHealth = 1;
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }
                    break;
                case SpecialAttack.OffensiveBlock:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.OffensiveBlock, new int[] { 40 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        attaquant.action = CatcheurAction.Attack;
                        attaquant.DebuffAttack = 2; // tape à 1... un peu cochon mais bon...
                        attaquant.BonusDefense = 4; 
                    }
                    else
                    {
                        attaquant.action = CatcheurAction.SpeAttackFailed;
                    }

                    break;
                case SpecialAttack.RaieDuC:
                    whatToDo = AttackSpecialSuccess(attaquant.Pseudo, SpecialAttack.RaieDuC, new int[] { 60, 40 });
                    specialAttackSuccess = (Boolean)whatToDo[0];
                    specialAttackSelected = (string)whatToDo[1];
                    if (specialAttackSuccess)
                    {
                        if (specialAttackSelected == "P1")
                        {
                            attaquant.action = CatcheurAction.Attack;
                            attaquant.BonusAttack = 1; // tape à 1... un peu cochon mais bon...
                            attaquant.BonusDefense = 2;
                        }
                        else if (specialAttackSelected == "P2")
                        {
                            attaquant.action = CatcheurAction.Heal;
                            attaquant.BonusHeal = -3;
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

            int previousSuccessRate = 0;
            int random = MatchManager.instance.dice.Next(1, 101);
            int Count = 1;

            foreach (int successRate in rates)
            {
                if (random > previousSuccessRate && random <= successRate + previousSuccessRate)
                {
                    Console.WriteLine($"L'attaque spéciale [{specialAtk} P{Count}] de {catcheurPseudo} a réussi !");
                    whatToDo.Add(true);
                    whatToDo.Add($"P{Count}");
                    return whatToDo;
                }
                else
                {
                    if(Count == rates.Length)
                    {
                        Console.WriteLine($"L'attaque spéciale [{specialAtk}] de {catcheurPseudo} a échoué...");
                        whatToDo.Add(false);
                        whatToDo.Add("");
                        return whatToDo;
                    }
                    previousSuccessRate = successRate;
                }
                Count++;
            }
            return whatToDo;
            
        }


    }
}
