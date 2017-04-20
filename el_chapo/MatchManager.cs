using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace el_chapo
{
    public class MatchManager
    {
        public static MatchManager instance = new MatchManager();

        public Random dice = new Random();
        

        public int Season { get; set; } = 1;
        public int MatchThisSeason { get; set; }
        private Boolean someoneIsDead = false;
        private Catcheur whoIsDead;
        private int iteration;

        private Catcheur startedToAttack;

        private int iterationMax = 20;

        public MatchManager()
        {

        }

        public void CreateNewMatch() // création de match
        {
            Console.WriteLine(MenuManager.instance.menuCreationMatch);
            Console.WriteLine(Contacts.DisplayCatcheurList());
            chooseCatcheurs();

        }


     
        public void ChooseToSearchOrQuit(int choix)
        {
            switch (choix)
            {
                case 0:
                    Contacts.SearchByname();
                    MenuManager.instance.RetourMainMenu();
                    break;

                case 1:

                    MenuManager.instance.RetourMainMenuInstant();
                    break;
            }

        }

        public void DisplayMenuRessource() //menu pour la recherche par nom  
        {
            StringBuilder menuRessourceContent = new StringBuilder();
            menuRessourceContent.AppendLine("0 - Faire une recherche par nom");
            menuRessourceContent.AppendLine("1 - Revenir au menu principal");
            Console.WriteLine(menuRessourceContent);
            ChooseToSearchOrQuit(MenuManager.instance.TestUserInput(0, 2));

        }

        public void chooseCatcheurs() // choix des catcheurs en vue du match de samedi 
        {
            int tempP1;
            int tempP2 = int.MaxValue;

            Console.WriteLine("Choisissez le 1er catcheur : ");
            tempP1 = MenuManager.instance.TestUserInput(0, Contacts.GetCatcheurOp().Count);

            do
            {
                if (tempP2 == int.MaxValue)
                {
                    Console.WriteLine("Choisissez le 2éme catcheur : ");
                    tempP2 = MenuManager.instance.TestUserInput(0, Contacts.GetCatcheurOp().Count);
                }
                else
                {
                    Console.WriteLine("On ne peut pas combattre contre soi-même... Veuillez choisir un autre catcheur");
                    tempP2 = MenuManager.instance.TestUserInput(0, Contacts.GetCatcheurOp().Count);
                }

            }
            while (tempP2 == tempP1);
            List<Catcheur> catcheursOp = Contacts.GetCatcheurOp();
            Console.WriteLine($"Le combat va se disputer entre {catcheursOp[tempP1].Pseudo} & {catcheursOp[tempP2].Pseudo}");
            // lancement du son de debut de match 
            if (("John Cinéma" == catcheursOp[tempP1].Pseudo) || ("John Cinéma" == catcheursOp[tempP2].Pseudo))
            {
                SoundManager.instance.playSimpleSoundCina();
                Thread.Sleep(10000);
            }

            ManageFight(catcheursOp[tempP1], catcheursOp[tempP2]);

        }

        private void ManageFight(Catcheur c1, Catcheur c2) // génération du match round par round 
        {
            if(MatchThisSeason % 8 == 0 && MatchThisSeason != 0)
            {
                Season++;
                MatchThisSeason = 1;
            }
            else
            {
                MatchThisSeason++;
            }

            someoneIsDead = false;
            iteration = 1;
            whoIsDead = null;

            for (iteration = 1; iteration <= iterationMax; iteration++)
            {
                if (!someoneIsDead)
                {
                    Console.WriteLine($"\n\nITERATION {iteration} !");
                    // On détermine les actions à faire (random)
                    c1.ChooseAction();
                    c2.ChooseAction();

                    // On lance le dé pour voir qui commence, si randomInt = 1 c1 commence / si randomInt = 2 c2 commence
                    Catcheur[] order = WhoIsAttackingFirst(iteration, c1, c2);
                    c1 = order[0]; // commence
                    c2 = order[1]; // fini

                    // On détermine le combo d'action et on le transforme en string, chaque cas est représenté par un duo de nombre compris entre 0 et 2 :
                    // 0 = Attaque
                    // 1 = Defense
                    // 2 = AttaqueSpe
                    // On a donc 00, 01, 02, 10, 11, 12, 20, 21, 22
                    string trinaryAction = "" + ((int)c1.action) + "" + ((int)c2.action);
                    //GetMatchUpScreen( c1, c2, trinaryAction);

                    IterationMatchUp(c1,c2,trinaryAction);

                    // On regarde si l'un des deux joueurs est mort à la fin de l'itération en cours.
                    if (CheckDeathAndManageDeath(c1, c2))
                    {
                        RefreshBonus(c1, c2);
                        DisplayResultCurrentIteration(c1, c2, iteration);
                        break;
                    }
                    // On display le résultat pour cette itération
                    RefreshBonus(c1, c2);
                    DisplayResultCurrentIteration(c1, c2, iteration);
                    //Thread.Sleep(1000);
                }

                if (iteration == iterationMax)
                {
                    break;
                }
            }
            DisplayAndManageEndGame(c1, c2);
        }

        private Catcheur[] WhoIsAttackingFirst(int iteration, Catcheur c1, Catcheur c2)
        {
            if (iteration == 1)
            {
                int randomInt = dice.Next(1, 3);

                if (randomInt == 2)
                {
                    Catcheur tempC1 = c1;
                    c1 = c2;
                    c2 = tempC1;
                    startedToAttack = c2;
                }
                else
                {
                    startedToAttack = c1;
                }

            }
            else
            {
                if (startedToAttack == c1)
                {
                    Catcheur tempC1 = c1;
                    c1 = c2;
                    c2 = tempC1;
                    startedToAttack = c2;
                }
                else
                {
                    Catcheur tempC2 = c2;
                    c2 = c1;
                    c1 = tempC2;
                    startedToAttack = c1;
                }
            }
            Catcheur[] orderedCatcheurs = { c1, c2 };
            return orderedCatcheurs;
        }

        private void DisplayAndManageEndGame(Catcheur c1, Catcheur c2) // genère le fin de game
        {
            Catcheur[] winnerLooser = WhoWinsAndLooses(c1, c2);
            Catcheur winner = winnerLooser[0];
            Catcheur looser = winnerLooser[1];
            double gainDuMatch;

            if (whoIsDead == null)
            {
                gainDuMatch = MoneyManager.instance.UpdateMoney(iteration, false);
                Console.WriteLine($"\n\nLES {iteration} ROUNDS SONT FINIS, FIN DU MATCH SANS MORT !\n");

                Console.WriteLine($"Le Vainqueur du match est {winner.Pseudo}, BRAVO ! *El Chapo applaudit*");
                Console.WriteLine($"Le perdant n'est nul autre que {looser.Pseudo}, ce match lui aura valu une bonne convalescence !\n");
                MoneyManager.instance.ColorAndDisplayMoney(gainDuMatch, MoneyManager.instance.Money);
                HistoryManager.instance.Addhistory(winner, looser, WinState.PAR_DELAI, iteration, gainDuMatch);
                Console.WriteLine("  ####  NEWS  ####\n  ");
                Hospital.SetConvalAndHeal(winner, looser);
            }
            else
            {
                SoundManager.instance.playSimpleSoundMort();
                gainDuMatch = MoneyManager.instance.UpdateMoney(iteration, true);
                Console.WriteLine($"\n\nLE MATCH C'EST TERMINE EN {iteration} ROUNDS, malheureusement {looser.Pseudo} est mort !\n");
                Console.WriteLine($"Le Vainqueur du match est {winner.Pseudo}, BRAVO ! *El Chapo applaudit*");
                Console.WriteLine($"Le perdant n'est nul autre que {looser.Pseudo}, ce match lui aura valu une bonne convalescence !\n");
                MoneyManager.instance.ColorAndDisplayMoney(gainDuMatch, MoneyManager.instance.Money);
                HistoryManager.instance.Addhistory(winner, looser, WinState.KO, iteration, gainDuMatch);
                Hospital.HealWinner(winner);
            }

            if (Hospital.IsEveryoneDead())
            {
                DisplayEndScreen(c1, c2);
            }
            else
            {
                MenuManager.instance.RetourMainMenu();

            }
            
        }

        private void RefreshBonus(Catcheur c1, Catcheur c2) // permet de remettre les bonus a 0
        {
            // Refresh bonus
            c1.BonusAttack = 0;
            c1.BonusDefense = 0;

            c2.BonusAttack = 0;
            c2.BonusDefense = 0;

            c1.BonusHeal = 0;
            c2.BonusHeal = 0;


            c1.DebuffHealth = 0;
            c2.DebuffHealth = 0;

            c1.DebuffAttack = 0;
            c2.DebuffAttack = 0;

            c1.DebuffDefense = 0;
            c2.DebuffDefense = 0;

        }

        private void DisplayResultCurrentIteration(Catcheur c1, Catcheur c2, int iteration) // notification de l'état des catcheurs a chaque itération
        {
            Console.WriteLine($"\nRésumé du round {iteration} : \n{c1.Pseudo} : {c1.Health} HP\n{c2.Pseudo} : {c2.Health} HP\n");
        }
        

        public void IterationMatchUp(Catcheur c1, Catcheur c2, string trinary) // on génère les actions du round 
        {
            switch (trinary)
            {
                // Attaque - Attaque
                case "00":
                    Colorer.ColorAttack(c1);
                    Colorer.ColorAttack(c2);
                    
                    if (c1.AttackTarget(c2)) // Si c1 attaque c2 & c2 ne meurt pas
                    {
                        c2.AttackTarget(c1);
                    }
                    break;

                // Attaque - Defense
                case "01":
                    Colorer.ColorAttack(c1);
                    Colorer.ColorDefend(c2);
                    c1.AttackTarget(c2);
                    break;
                // Attaque - AttaqueSpe
                case "02":
                    Colorer.ColorAttack(c1);
                    Colorer.ColorAttackSpe(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                // Defense - Attaque
                case "10":
                    Colorer.ColorDefend(c1);
                    Colorer.ColorAttack(c2);
                    c2.AttackTarget(c1);
                    break;

                // Defense - Defense
                case "11":
                    Colorer.ColorDefend(c1);
                    Colorer.ColorDefend(c2);
                    NothingIsHappening(c1, c2);
                    break;

                // Defense - AttaqueSpe
                case "12":
                    Colorer.ColorDefend(c1);
                    Colorer.ColorAttackSpe(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                //AttaqueSpe - Attaque
                case "20":
                    Colorer.ColorAttackSpe(c1);
                    Colorer.ColorAttack(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                //AttaqueSpe - Defense
                case "21":
                    Colorer.ColorAttackSpe(c1);
                    Colorer.ColorDefend(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                // AttaqueSpe - AttaqueSpe
                case "22":
                    Colorer.ColorAttackSpe(c1);
                    Colorer.ColorAttackSpe(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManageActionAndDisplayResult(c1, c2);
                    break;
            }
        }

        private Boolean CheckDeathAndManageDeath(Catcheur c1, Catcheur c2) // on verifie l'état du catcheur pour savoir si il est mort
        {
            if (c1.CatcheurState == CatcheurState.Mort)
            {
                return SetDeath(c1);
            }
            else if (c2.CatcheurState == CatcheurState.Mort)
            {
                return SetDeath(c2);
            }
            return false;
        }

        private Boolean SetDeath(Catcheur dead) // on passe le statue du catcheur mort a mort 
        {
            whoIsDead = dead;
            someoneIsDead = true;
            return true;
        }
   
        private Catcheur[] WhoWinsAndLooses(Catcheur c1, Catcheur c2) // need explication
        {
            if (c1.Health > c2.Health)
            {
                return new Catcheur[] { c1, c2 };
            }
            else
            {
                return new Catcheur[] { c2, c1 };
            }
        }

        private void DisplayEndScreen(Catcheur c1, Catcheur c2) // flo c'est vraiment l'ecran de game over ?
        {
            Console.WriteLine("Appuyez sur n'importe quelle touche pour accéder à l'écran de fin");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("THE END");
            Console.WriteLine("Résumé : ");
            // à faire par qui veux

            //retour menu principal
            MenuManager.instance.RetourMainMenu();
        }

        public void ManageActionAndDisplayResult(Catcheur attaquant, Catcheur defenseur) // gère les actions 
        {
            switch (attaquant.action)
            {
                case CatcheurAction.Defend:
                    if (defenseur.action == CatcheurAction.Attack)
                    {
                        defenseur.AttackTarget(attaquant);
                    }
                    else if (defenseur.action == CatcheurAction.Heal)
                    {
                        defenseur.Heal(defenseur.BonusHeal);
                    }
                    else
                    {
                        NothingIsHappening(attaquant, defenseur);
                    }

                    break;

                case CatcheurAction.Attack:
                    if (defenseur.action == CatcheurAction.Attack)
                    {
                        if (attaquant.AttackTarget(defenseur))
                        {
                            defenseur.AttackTarget(attaquant);
                        }
                    }
                    if (defenseur.action == CatcheurAction.Heal)
                    {
                        if (attaquant.AttackTarget(defenseur))
                        {
                            defenseur.Heal(defenseur.BonusHeal);
                        }
                    }
                    else if (defenseur.action == CatcheurAction.Defend)
                    {
                        attaquant.AttackTarget(defenseur);
                    }
                    else if (defenseur.action == CatcheurAction.SpeAttackFailed)
                    {
                        attaquant.AttackTarget(defenseur);
                    }
                    break;
                case CatcheurAction.Heal:
                    if (defenseur.action == CatcheurAction.Attack)
                    {
                        attaquant.Heal(attaquant.BonusHeal);
                        defenseur.AttackTarget(attaquant);
                    }
                    if (defenseur.action == CatcheurAction.Heal)
                    {
                        attaquant.Heal(attaquant.BonusHeal);
                        defenseur.Heal(defenseur.BonusHeal);
                    }
                    else if (defenseur.action == CatcheurAction.Defend)
                    {
                        attaquant.Heal(attaquant.BonusHeal);
                    }
                    else if (defenseur.action == CatcheurAction.SpeAttackFailed)
                    {
                        attaquant.Heal(attaquant.BonusHeal);
                    }
                    break;

                case CatcheurAction.SpeAttackFailed:
                    if (defenseur.action == CatcheurAction.Attack)
                    {
                        defenseur.AttackTarget(attaquant);
                    }
                    else if (defenseur.action == CatcheurAction.Heal)
                    {
                        defenseur.Heal(defenseur.BonusHeal);
                    }
                    else
                    {
                        NothingIsHappening(attaquant, defenseur);
                    }
                    break;
            }
        }

        private void NothingIsHappening(Catcheur c1, Catcheur c2) // nothing is happening
        {
            Console.WriteLine($"{c1.Pseudo} et {c2.Pseudo} se regardent droit dans les yeux, mais rien ne se passe...");
        }
    }
}
