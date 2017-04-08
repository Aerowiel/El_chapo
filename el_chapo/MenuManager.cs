using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace el_chapo
{

    class MenuManager
    {
        // SB menus
        public StringBuilder menuPrincipalContent;
        public StringBuilder menuCreationMatch;

        public static MenuManager instance = new MenuManager();

        public MenuManager()
        {
            //Déclaration des lignes de création de match
            menuCreationMatch = new StringBuilder();
            menuCreationMatch.AppendLine("############# NEW MATCH ############");
            menuCreationMatch.AppendLine("CHOISISSEZ UN CATCHEUR DANS LA LISTE CI-DESSOUS");
        }

        public int TestUserInput(int min, int max)
        {
            int intSelectedMenu;

            while (!int.TryParse(Console.ReadLine(), out intSelectedMenu) || intSelectedMenu > max - 1 || intSelectedMenu < min)
            {
                if (intSelectedMenu > max - 1 || intSelectedMenu < min)
                {
                    Console.WriteLine("Saisie incorrect !");
                }
                else
                {
                    Console.WriteLine("Ce n'est pas un nombre !");
                }
            }

            return intSelectedMenu;
        }

        public void DisplayMenu()
        {
            menuPrincipalContent = new StringBuilder();
            menuPrincipalContent.AppendLine("################ MENU ###############");
            menuPrincipalContent.AppendLine($"Argent gagné : {MoneyManager.instance.Money} $");
            menuPrincipalContent.AppendLine("0 - Créer le match de samedi prochain");
            menuPrincipalContent.AppendLine("1 - Consulter l'historique des matchs");
            menuPrincipalContent.AppendLine("2 - Consulter la base des contacts");
            menuPrincipalContent.AppendLine("3 - Quitter le jeu");
            Console.WriteLine(menuPrincipalContent);
            SelectedMenu(TestUserInput(0, 4));
            
        }
        
      

        public void SelectedMenu(int selected)
        {
            switch (selected)
            {
                case 0:
                    MatchManager.instance.CreateNewMatch();
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine(HistoryManager.instance.DisplayHistory());
                    HistoryManager.instance.DisplayMenuHistory();
                   

                    break;
                case 2:

                    Console.Clear();

                    Console.WriteLine(MatchManager.instance.DisplayFullCatcheurList());

                    RetourMainMenu();
                    break;
                case 3:
                    ExitApplication();
                    
                    break;
                default:
                    Console.WriteLine("Ce menu n'existe pas veuillez saisir un autre numéro de menu...\n");
                    //DisplayMenu();
                    break;
            }
        }

        public void ExitApplication()
        {
            System.Environment.Exit(-1);
        }

        public void RetourMainMenu()
        {
            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour revenir au menu principal... ");
            Console.ReadLine();
            Console.Clear();
            DisplayMenu();
        }

        public void RetourMainMenuInstant()
        {
            Console.Clear();
            DisplayMenu();
        }


    }
}
