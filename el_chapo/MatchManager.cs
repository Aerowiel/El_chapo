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
        public List<Catcheur> Catcheurs { get; set; }
        public int Season { get; set; }
        public int MatchThisSeason { get; set; }
        public StringBuilder menuRessourceContent;
        private Boolean someoneIsDead = false;
        private Catcheur whoIsDead;
        private int iteration;

        private int iterationMax = 20;

        private int opATM;

        public MatchManager()
        {
            //Init Stats
            Season = 1;

            //Init catcheurs
            Catcheurs = new List<Catcheur>();


            // Special = 0.33, Attaque = 0.33, Defense = 0.33
            Catcheurs.Add(new Catcheur("L'ordonnateur des pompes funèbres", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Jude Sunny", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.JuddyPower));
            Catcheurs.Add(new Catcheur("Triple Hache", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.HacheFoudroyante));
            Catcheurs.Add(new Catcheur("Dead Poule", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.DeadPoulePower));
            Catcheurs.Add(new Catcheur("Jarvan cinquième du nom", CatcheurType.Brute, CatcheurState.Convalescent, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Madusa", CatcheurType.Agile, CatcheurState.Mort, SpecialAttack.OffensiveBlock));
            Catcheurs.Add(new Catcheur("John Cinéma", CatcheurType.Agile, CatcheurState.Mort, SpecialAttack.HacheFoudroyante));
            Catcheurs.Add(new Catcheur("Jeff Radis", CatcheurType.Brute, CatcheurState.Convalescent, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Raie Mystérieuse", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.RaieDuC));
            Catcheurs.Add(new Catcheur("Chris Hart", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Bret Benoit", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Oneshot));

        }

        public void CreateNewMatch() // création de match
        {
            Console.WriteLine(MenuManager.instance.menuCreationMatch);
            Console.WriteLine(DisplayCatcheurList());
            chooseCatcheurs();

        }

        public void OrderByPseudo() // tri par ordre alphabetique 
        {
            Catcheurs.Sort((x, y) => string.Compare(x.Pseudo, y.Pseudo));
        }

        public List<Catcheur> GetCatcheurMort()
        {
            
            List<Catcheur> CatcheurMort = new List<Catcheur>();
            //int index = 0;
            OrderByPseudo();
            foreach (Catcheur catcheur in Catcheurs)
            {
                if (catcheur.CatcheurState == CatcheurState.Mort)
                {
                    CatcheurMort.Add(catcheur);                   
                }
            }
            return CatcheurMort;
        }

        

        public void ChooseToSearchOrQuit(int choix)
        {
            switch (choix)
            {
                case 0:
                    SearchByname();
                    MenuManager.instance.RetourMainMenu();
                    break;

                case 1:

                    MenuManager.instance.RetourMainMenuInstant();
                    break;
            }

        }

        public void DisplayMenuRessource() //menu pour la recherche par nom  
        {
            menuRessourceContent = new StringBuilder();
            menuRessourceContent.AppendLine("0 - Faire une recherche par nom");
            menuRessourceContent.AppendLine("1 - Revenir au menu principal");
            Console.WriteLine(menuRessourceContent);
            ChooseToSearchOrQuit(MenuManager.instance.TestUserInput(0, 2));

        }

        public List<Catcheur> GetCatcheurOp()
        {
            
            List <Catcheur> CatcheursOp = new List<Catcheur>();
            // affiche les catcheurs opérationnel , convalescent et mort 
            OrderByPseudo();
            foreach(Catcheur catcheur in Catcheurs)
            {
                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                {
                    CatcheursOp.Add(catcheur);
                }
            }
            return CatcheursOp;      
        }

        public List<Catcheur> GetCatcheurConv()
        {

            List<Catcheur> CatcheursConv = new List<Catcheur>();
            // récupère les catcheurs  convalescent 
            foreach (Catcheur catcheur in Catcheurs)
            {
                if (catcheur.CatcheurState == CatcheurState.Convalescent)
                {
                    CatcheursConv.Add(catcheur);
                }
            }
            return CatcheursConv;
        }


        public StringBuilder DisplayCatcheurList()// affiche la liste des catcheurs opérationnel
        {
            StringBuilder sb = new StringBuilder();
            List<Catcheur> CatcheursOp = GetCatcheurOp();
            int index = 0;
            // Affiche uniquement les catcheurs OP 
            OrderByPseudo();
            foreach (Catcheur catcheur in CatcheursOp)
            {
                sb.AppendLine(catcheur.Describe(index++));
            }
            opATM = index;
            return sb;
        }

        public List<Catcheur> GetOrderedList()
        {
            List<Catcheur> catcheurFull = new List<Catcheur>();
            catcheurFull.AddRange(GetCatcheurOp());
            catcheurFull.AddRange(GetCatcheurConv());
            catcheurFull.AddRange(GetCatcheurMort());
            return catcheurFull;
        }

        public string SearchByname()
        {
            Console.WriteLine("Séléctionnez le nom du catcheur que vous recherchez \n");
            string nameOfCatcheur = Console.ReadLine();
            Boolean found = false;
            int index = 0;
            foreach (Catcheur catcheur in GetOrderedList())
            {
                if (nameOfCatcheur == catcheur.Pseudo)
                {
                    Console.WriteLine(catcheur.Describe(index++));
                    found = true;
                    return nameOfCatcheur;
                }         
            }
            if (!found )
            {  
                    Console.WriteLine(" Le catcheur que vous recherchez néxiste pas !");              
            }
            return "";
        }


        public StringBuilder DisplayFullCatcheurList() // affiche la liste des catcheurs opérationnel et convalescnet 
        {
            StringBuilder sb = new StringBuilder();
       
            
            int index = 0;
            OrderByPseudo();
            foreach (Catcheur catcheur in GetOrderedList())
            {
                sb.AppendLine(catcheur.Describe(index++));
            }

            return sb;
        }


        public void chooseCatcheurs() // choix des catcheurs en vue du match de samedi 
        {
            int tempP1;
            int tempP2 = int.MaxValue;

            Console.WriteLine("Choisissez le 1er catcheur : ");
            tempP1 = MenuManager.instance.TestUserInput(0, opATM);

            do
            {
                if (tempP2 == int.MaxValue)
                {
                    Console.WriteLine("Choisissez le 2éme catcheur : ");
                    tempP2 = MenuManager.instance.TestUserInput(0, opATM);
                }
                else
                {
                    Console.WriteLine("On ne peut pas combattre contre soi-même... Veuillez choisir un autre catcheur");
                    tempP2 = MenuManager.instance.TestUserInput(0, opATM);
                }

            }
            while (tempP2 == tempP1);
            List<Catcheur> catcheursOp = GetCatcheurOp();
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
                    int randomInt = dice.Next(1, 3);

                    if (randomInt == 2)
                    {
                        Catcheur tempC1 = c1;
                        c1 = c2;
                        c2 = tempC1;
                    }

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
                    //la
                }

                if (iteration == iterationMax)
                {
                    break;
                }
                //Thread.Sleep(2000);
            }
            DisplayAndManageEndGame(c1, c2);
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
                SetConvalAndHeal(winner, looser);
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
                HealWinner(winner);
            }

            Thread.Sleep(3000);
            if (IsEveryoneDead())
            {
                DisplayEndScreen(c1, c2);
            }
            else
            {
                Console.Clear();
                MenuManager.instance.DisplayMenu();
            }
            
        }

        private Boolean IsEveryoneDead() // fonction qui permet de verifier si il reste des catcheurs opérationel 
        {
            List<Catcheur> isNotDead = new List<Catcheur>();
            foreach (Catcheur catcheur in Catcheurs)
            {

                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                {
                    isNotDead.Add(catcheur);
                }

            }

            if(isNotDead.Count <= 1)
            {
                return true;
            }
            else
            {
                return false;
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
        
        private void ColorAttack(Catcheur c) // affiche l'action "attack" en rouge
        {
            Console.Write($"\n{c.Pseudo} - {c.Health}HP - Action : ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(c.action + "\n");
            Console.ResetColor();

        }

        private void ColorDefend(Catcheur c) // affiche l'action "defend" en  vert
        {
            Console.Write($"\n{c.Pseudo} - {c.Health}HP - Action : ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(c.action + "\n");
            Console.ResetColor();

        }

        private void ColorAttackSpe(Catcheur c) // affiche l'action "AttackSpecial" en violet
        {
            Console.Write($"\n{c.Pseudo} - {c.Health}HP - Action : ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(c.action + "\n");
            Console.ResetColor();
        }

        public void IterationMatchUp(Catcheur c1, Catcheur c2, string trinary) // on génère les actions du round 
        {
            switch (trinary)
            {
                // Attaque - Attaque
                case "00":
                    ColorAttack(c1);
                    ColorAttack(c2);
                    
                    if (c1.AttackTarget(c2)) // Si c1 attaque c2 & c2 ne meurt pas
                    {
                        c2.AttackTarget(c1);
                    }
                    break;

                // Attaque - Defense
                case "01":
                    ColorAttack(c1);
                    ColorDefend(c2);
                    c1.AttackTarget(c2);
                    break;
                // Attaque - AttaqueSpe
                case "02":
                    ColorAttack(c1);
                    ColorAttackSpe(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                // Defense - Attaque
                case "10":
                    ColorDefend(c1);
                    ColorAttack(c2);
                    c2.AttackTarget(c1);
                    break;

                // Defense - Defense
                case "11":
                    ColorDefend(c1);
                    ColorDefend(c2);
                    NothingIsHappening(c1, c2);
                    break;

                // Defense - AttaqueSpe
                case "12":
                    ColorDefend(c1);
                    ColorAttackSpe(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                //AttaqueSpe - Attaque
                case "20":
                    ColorAttackSpe(c1);
                    ColorAttack(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                //AttaqueSpe - Defense
                case "21":
                    ColorAttackSpe(c1);
                    ColorDefend(c2);
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    ManageActionAndDisplayResult(c1, c2);
                    break;

                // AttaqueSpe - AttaqueSpe
                case "22":
                    ColorAttackSpe(c1);
                    ColorAttackSpe(c2);
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

        private void SetConvalAndHeal(Catcheur winner, Catcheur looser) // on heal le gagnant puis on verifie la vie du perdant et on lui change son état si besoin 
        {
            HealWinner(winner);
            if (looser.Health < (looser.maxHealth / 2))
            {
                looser.DayRemainingBeforeOp = dice.Next(2, 6);
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

        private void HealWinner(Catcheur winner) // heal le gagnant
        {
            winner.Health = winner.maxHealth;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Le gagnant {winner.Pseudo} a été soigné de la totalité de ses points de vie !");
            Console.ResetColor();
            UpdateConvalAndChangeState();
        }

        private void UpdateConvalAndChangeState() // gere le temps de convalescence des catcheurs
        {
            foreach (Catcheur catcheur in Catcheurs)
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

        private void NothingIsHappening(Catcheur c1, Catcheur c2) // texte si les deux catcheurs n'attaquent pas 
        {
            Console.WriteLine($"{c1.Pseudo} et {c2.Pseudo} se regardent droit dans les yeux, mais rien ne se passe...");
        }
    }
}
