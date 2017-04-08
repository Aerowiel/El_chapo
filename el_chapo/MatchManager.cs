using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace el_chapo
{
    class MatchManager
    {
        public List<Catcheur> Catcheurs { get; set; }
        public List<Catcheur> CatcheursOp { get; set; }
        public List<History> HistoryCatcheur { get; set; }
        //test
        public List<string> HistoryCtacheur { get; set; }
        public static MatchManager instance = new MatchManager();
        public static Random dice = new Random();

        private Boolean someoneIsDead = false;
        private Catcheur whoIsDead;

        private string victoryCatcheur;
        private string looserCatcheur;
        private int itération; // need for history 
        private int benefMoney ;
        private int actuelMoney ;
        private int saison;

        private int opATM;
       

        public MatchManager()
        {
            //Init catcheurs
            Catcheurs = new List<Catcheur>();

            // Special = 0.33, Attaque = 0.33, Defense = 0.33
            Catcheurs.Add(new Catcheur("L'ordonnateur des pompes funèbres", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Jude Sunny", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.JuddyPower));
            Catcheurs.Add(new Catcheur("Triple Hache", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Dead Poule", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Jarvan cinquième du nom", CatcheurType.Brute, CatcheurState.Convalescent, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Madusa", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("John Cinéma", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Jeff Radis", CatcheurType.Brute, CatcheurState.Convalescent, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Raie Mystérieuse", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Chris Hart", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque));
            Catcheurs.Add(new Catcheur("Bret Benoit", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Oneshot));

        }
       


        public void CreateNewMatch()
        {
            Console.WriteLine(MenuManager.instance.menuCreationMatch);
            Console.WriteLine(DisplayCatcheurList());
            chooseCatcheurs();

        }

        public StringBuilder DisplayCatcheurList()
        {
            StringBuilder sb = new StringBuilder();
            CatcheursOp = new List<Catcheur>();
            int index = 0;
            // Affiche uniquement les catcheurs OP 

            foreach (Catcheur catcheur in Catcheurs)
            {

                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                {
                    CatcheursOp.Add(catcheur);
                    sb.AppendLine(catcheur.Describe(index++));
                }
                
            }
            opATM = index;
            return sb;
        }

     

        public StringBuilder DisplayFullCatcheurList()
        {
            StringBuilder sb = new StringBuilder();
            
            int index = 0;

            foreach (Catcheur catcheur in Catcheurs)
            {
                
                sb.AppendLine(catcheur.Describe(index++));
            }
            return sb;
        }

        public void chooseCatcheurs()
        {
            int tempP1;
            int tempP2 = int.MaxValue;

            Console.WriteLine("Choisissez le 1er catcheur : ");
            tempP1 = MenuManager.instance.TestUserInput(0, opATM);

            do
            {
                if(tempP2 == int.MaxValue)
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

            Console.WriteLine($"Le combat va se disputer entre {CatcheursOp[tempP1].Pseudo} & {CatcheursOp[tempP2].Pseudo}");
            // lancement du son de debut de match 
            /*if (("John Cinéma" == CatcheursOp[tempP1].Pseudo) || ("John Cinéma" == CatcheursOp[tempP2].Pseudo))
            {
                playSimpleSoundCina();
                Thread.Sleep(10000);
            }
            */
            ManageFight(CatcheursOp[tempP1], CatcheursOp[tempP2]);

        }
        

        private void ManageFight(Catcheur c1, Catcheur c2)
        {
            someoneIsDead = false;
            benefMoney = 0;
            

            for (int iteration = 1; iteration <= 101; iteration++) // 101 plutot que 100 pour pouvoir afficher le vainquer par defaut si pas de mort

            {
                if (!someoneIsDead && iteration < 101) // rajout de la condition 
                {
                    Console.WriteLine($"\n\nITERATION {iteration} !");
                    itération = iteration;
                    // Refresh bonus
                    c1.BonusAttack = 0;
                    c1.BonusDefense = 0;
                    c2.BonusAttack = 0;
                    c2.BonusDefense = 0;
                    c1.BonusHeal = 0;
                    c2.BonusHeal = 0;

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
                    Console.WriteLine(GetMatchUpScreen(c1, c2));
                    IterationMatchUp(trinaryAction, c1, c2);
                    CheckDeath(c1, c2);
                    Console.WriteLine($"Resultat : \n{c1.Pseudo} : {c1.Health} HP\n{c2.Pseudo} : {c2.Health} HP");
                    benefMoney += 5000;
                    
                 

                }
                else
                {
                    
                    Saison();                 
                    Victory(c1, c2, out victoryCatcheur, out looserCatcheur);// determine le vainqueur et ressort les variables utilisé pour l'historique 
                    Console.WriteLine($"Vous avez gagnez {benefMoney} $ sur ce match");
                    Createhistory(c1, c2);// in progress
                    DisplayEndScreen(c1, c2);            
                    Console.WriteLine("Break successfull");
                    break;
                }

               // Thread.Sleep(2000);
            }



        }


        public void Saison()
        {
            if (!someoneIsDead)// si ca atteint 100 tour ba il gagne que 1000
            {
                benefMoney += 1000;
            }
            else
            {
                benefMoney += 10000;
            }
            saison += 1; 
            if(saison % 8 == 0) { benefMoney = benefMoney + benefMoney*(13/100); }// majoration de  13% par saison 
            actuelMoney += benefMoney;
        }

        private StringBuilder GetMatchUpScreen(Catcheur c1, Catcheur c2)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\n{c1.Pseudo} - {c1.Health}HP - Action : {c1.action}");
            // s'il attaque, lance le son du punch 
            /*if(c1.action == CatcheurAction.Attack)
            {
                playSimpleSoundPunch();
                Thread.Sleep(1000);
            }
            else if (c1.action == CatcheurAction.SpecialAttack)
            {
                playSimpleSoundKameha();
                Thread.Sleep(5000);
            }
            Thread.Sleep(1000);*/

            sb.AppendLine($"\n{c2.Pseudo} - {c2.Health}HP - Action : {c2.action}");
            //s'il attaque, lance le son du punch 
            /*if (c2.action == CatcheurAction.Attack)
            {
                playSimpleSoundPunch();
            }
            else if (c2.action == CatcheurAction.SpecialAttack)
            {
                playSimpleSoundKameha();
            }*/
            return sb;
        }

        private void IterationMatchUp(String trinary, Catcheur c1, Catcheur c2)
        {
            switch (trinary)
            {
                // Attaque - Attaque
                case "00": 
                    if (c1.AttackTarget(c2)) // Si c1 attaque c2 & c2 ne meurt pas
                    {
                        c2.AttackTarget(c1);
                    }
                    break;

                // Attaque - Defense
                case "01":
                    c1.AttackTarget(c2);
                    break;
                // Attaque - AttaqueSpe
                case "02":
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManagaActionAndDisplayResult(c1, c2);
                    break;
                   
                
                // Defense - Attaque
                case "10": 
                    c2.AttackTarget(c1);
                    break;

                // Defense - Defense
                case "11":
                    NothingIsHappening(c1, c2);
                    break;

                // Defense - AttaqueSpe
                case "12":
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManagaActionAndDisplayResult(c1, c2);
                    break;

                //AttaqueSpe - Attaque
                case "20":
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    ManagaActionAndDisplayResult(c1, c2);
                    break;

                //AttaqueSpe - Defense
                case "21":
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    ManagaActionAndDisplayResult(c1, c2);
                    break;

                // AttaqueSpe - AttaqueSpe
                case "22":
                    SpecialAttackManager.instance.SpecialAttackComputing(c1);
                    SpecialAttackManager.instance.SpecialAttackComputing(c2);
                    ManagaActionAndDisplayResult(c1, c2);
                    break;
            }
        }
        
        private void CheckDeath(Catcheur c1, Catcheur c2)
        {
            if (c1.CatcheurState == CatcheurState.Mort)
            {
                Console.WriteLine($"Quelle HORREUR !! {c1.Pseudo} est mort !! FIN DU MATCH !!");
                whoIsDead = c1;
                someoneIsDead = true;

            }
            else if (c2.CatcheurState == CatcheurState.Mort)
            {
                Console.WriteLine($"Quelle HORREUR !! {c2.Pseudo} est mort !! FIN DU MATCH !!");
                whoIsDead = c2;
                someoneIsDead = true;
            }

        }
           

        private  void Victory(Catcheur c1, Catcheur c2, out string victoryCatcheur, out string looserCatcheur)
        {
           
            if (c1.Health > c2.Health)
            {
                Console.WriteLine($"Le Vainqueur est {c1.Pseudo}");
                victoryCatcheur = c1.Pseudo;
                looserCatcheur = c2.Pseudo;    
            }
            else
            {
                Console.WriteLine($"Le Vainqueur est {c2.Pseudo}");
                victoryCatcheur = c2.Pseudo;
                looserCatcheur = c1.Pseudo;
            }
        }

        private void DisplayEndScreen(Catcheur c1, Catcheur c2)
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

        public void ManagaActionAndDisplayResult(Catcheur attaquant, Catcheur defenseur)
        {
            switch (attaquant.action)
            {
                case CatcheurAction.Defend:
                    if (defenseur.action == CatcheurAction.Attack)
                    {
                        defenseur.AttackTarget(attaquant);
                    }
                    else if(defenseur.action == CatcheurAction.Heal)
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
                    if(defenseur.action == CatcheurAction.Heal)
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
        public void test() // fonction de  test :p vu que je savais pas ou afficher tout ca
        {
            Console.WriteLine(actuelMoney);
            
        }


        public void  Createhistory(Catcheur c1, Catcheur c2)
        {
                       //         j'arrive pas a implementer
            HistoryCatcheur = new List<History>();
         
                if (c1.CatcheurState == CatcheurState.Mort || c2.CatcheurState == CatcheurState.Mort)
                {

                    HistoryCatcheur.Add(new History(victoryCatcheur, looserCatcheur, WinState.KO, itération));

                }
                else
                {
                    HistoryCatcheur.Add(new History(victoryCatcheur, looserCatcheur, WinState.PAR_DELAI, itération));
                }
            

        }

        public StringBuilder DisplayHistory()
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;

            foreach (History history in HistoryCatcheur)
            {

                sb.AppendLine(history.DescribeHistory(index++));
            }
            return sb;
        }
        /*public StringBuilder DisplayCatcheurList()
        {
            StringBuilder sb = new StringBuilder();
            CatcheursOp = new List<Catcheur>();
            int index = 0;
            // Affiche uniquement les catcheurs OP 

            foreach (Catcheur catcheur in Catcheurs)
            {

                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                {
                    CatcheursOp.Add(catcheur);
                    sb.AppendLine(catcheur.Describe(index++));
                }

            }
            opATM = index;
            return sb;
        }*/

        private void NothingIsHappening(Catcheur c1, Catcheur c2)
        {
            Console.WriteLine($"{c1.Pseudo} et {c2.Pseudo} se regardent droit dans les yeux, mais rien ne se passe...");
        }


        public void playSimpleSoundCina()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\eliot\Desktop\ynov\b2\C#\to c#\el chapo\ressource\AND GOODBYE.wav");
            simpleSound.Play();
        }

        public void playSimpleSoundPunch()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\eliot\Desktop\ynov\b2\C#\to c#\el chapo\ressource\Punch.wav");
            simpleSound.Play();
        }

        public void playSimpleSoundKameha()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\eliot\Desktop\ynov\b2\C#\to c#\el chapo\ressource\kamehameha.wav");
            simpleSound.Play();
        }

    }
}
