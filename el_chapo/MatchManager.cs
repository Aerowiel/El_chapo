using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class MatchManager
    {
        public List<Catcheur> Catcheurs { get; set; }
        public static MatchManager instance = new MatchManager();
        public static Random dice = new Random();

        private Boolean someoneIsDead = false;
        private Catcheur whoIsDead;

        private int opATM;

        public MatchManager()
        {
            //Init catcheurs
            Catcheurs = new List<Catcheur>();

            // Special = 0.3, Attaque = 0.35, Defense = 0.35
            Catcheurs.Add(new Catcheur("L'ordonnateur des pompes funèbres", CatcheurType.Brute, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("Jude Sunny", CatcheurType.Brute, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("Triple Hache", CatcheurType.Agile, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("Dead Poule", CatcheurType.Agile, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("Jarvan cinquième du nom", CatcheurType.Brute, CatcheurState.Convalescent));
            Catcheurs.Add(new Catcheur("Madusa", CatcheurType.Agile, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("John Cinéma", CatcheurType.Agile, CatcheurState.Convalescent));
            Catcheurs.Add(new Catcheur("Jeff Radis", CatcheurType.Brute, CatcheurState.Convalescent));
            Catcheurs.Add(new Catcheur("Raie Mystérieuse", CatcheurType.Brute, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("Chris Hart", CatcheurType.Brute, CatcheurState.Opérationnel));
            Catcheurs.Add(new Catcheur("Bret Benoit", CatcheurType.Agile, CatcheurState.Opérationnel));

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
            int index = 0;
            // Affiche uniquement les catcheurs OP 

            foreach (Catcheur catcheur in Catcheurs)
            {

                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                    sb.AppendLine(catcheur.Describe(index++));
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

            Console.WriteLine($"Le combat va se disputer entre {Catcheurs[tempP1].Pseudo} & {Catcheurs[tempP2].Pseudo}");
            ManageFight(Catcheurs[tempP1], Catcheurs[tempP2]);

        }

        private void ManageFight(Catcheur c1, Catcheur c2)
        {
            someoneIsDead = false;

            for (int iteration = 1; iteration <= 20; iteration++)
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
                    Console.WriteLine(GetMatchUpScreen(c1, c2));
                    IterationMatchUp(trinaryAction, c1, c2);
                    CheckDeath(c1, c2);
                    Console.WriteLine($"Resultat : \n{c1.Pseudo} : {c1.Vie} HP\n{c2.Pseudo} : {c2.Vie}HP");

                }
                else
                {
                    break;
                }

                DisplayEndScreen(c1,c2);
                Console.WriteLine("Break successfull");
            }

        }

        private StringBuilder GetMatchUpScreen(Catcheur c1, Catcheur c2)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\n{c1.Pseudo} - {c1.Vie}HP - Action : {c1.action}");
            sb.AppendLine($"\n{c2.Pseudo} - {c2.Vie}HP - Action : {c2.action}");
            return sb;
        }

        private void IterationMatchUp(String trinary, Catcheur c1, Catcheur c2)
        {
            switch (trinary)
            {
                // Attaque - Attaque
                case "00": // fait
                    Console.WriteLine($"{c1.Pseudo} attaque {c2.Pseudo} à hauteur de {c1.Attaque}");
                    if (c1.AttaqueCible(c2, null)) // Si c1 attaque c2 est que c2 ne meurt pas
                    {
                        Console.WriteLine($"{c2.Pseudo} attaque {c1.Pseudo} à hauteur de {c2.Attaque}");
                        if (!c2.AttaqueCible(c1, null))
                        {
                            // Le c1 est mort
                            Console.WriteLine($"{c1.Pseudo} est mort sur le coup...");
                        }

                    }
                    else
                    {
                        // Le c2 est mort
                        Console.WriteLine($"{c2.Pseudo} est mort sur le coup...");
                    }
                    Console.WriteLine($"Resultat : \n{c1.Pseudo} : {c1.Vie} HP\n{c2.Pseudo} : {c2.Vie}HP");
                    break;

                // Attaque - Defense
                case "01": //fait
                    Console.WriteLine($"{c1.Pseudo} attaque {c2.Pseudo} à hauteur de {c1.Attaque}, {c2.Pseudo} aborsbe {c2.Defense} point(s) de dégat !");
                    if(!c1.AttaqueCible(c2, c2.Defense))
                    {
                        Console.WriteLine($"{c2.Pseudo} est mort sur le coup...");
                    }
                    break;

                case "02":
                    // On traite les attaques spéciales plus tard hein...
                    break;
                
                // Defense - Attaque
                case "10": //fait
                    Console.WriteLine($"{c2.Pseudo} attaque {c1.Pseudo} à hauteur de {c2.Attaque}, {c1.Pseudo} aborsbe {c1.Defense} point(s) de dégat !");
                    if (c2.AttaqueCible(c1, c1.Defense))
                    {
                        Console.WriteLine($"{c2.Pseudo} est mort sur le coup...");
                    }
                    break;
                // Defense - Defense MDR
                case "11": //fait
                    Console.WriteLine($"{c1.Pseudo} et {c2.Pseudo} se regardent droit dans les yeux, tous deux en position de défense, malheureusement personne ne décidera d'attaquer ce tour-ci...");
                    break;
                case "12":
                    // On traite les attaques spéciales plus tard hein...
                    break;
                case "20":

                    break;
                case "21":

                    break;
                case "22":

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

        private void DisplayEndScreen(Catcheur c1, Catcheur c2)
        {
            Console.Clear();
            Console.WriteLine("THE END");
            Console.WriteLine("Résumé : ");
            // à faire par qui veux
        }

    }
}
