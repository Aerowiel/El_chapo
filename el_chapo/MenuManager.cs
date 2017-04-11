using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

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
            menuPrincipalContent.AppendLine($"Saison : {MatchManager.instance.Season} | Match cette saison {MatchManager.instance.MatchThisSeason}");
            menuPrincipalContent.AppendLine("0 - Créer le match de samedi prochain");
            menuPrincipalContent.AppendLine("1 - Consulter l'historique des matchs");
            menuPrincipalContent.AppendLine("2 - Consulter la base des contacts");
            menuPrincipalContent.AppendLine("3 - Charger une sauvegarde");
            menuPrincipalContent.AppendLine("4 - Quitter le jeu");
            Console.WriteLine(menuPrincipalContent);
            SelectedMenu(TestUserInput(0, 5));

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
                    Console.WriteLine(MatchManager.instance.DisplayFullCatcheurList());
                  
                   
                    RetourMainMenu();
                    break;
                case 3:
                    DisplaySavesMenu();
                    break;
                case 4:
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
            Console.WriteLine("Voulez-vous sauvegarder la partie en cours avant de quitter l'application ?");
            Console.WriteLine("1. Oui");
            Console.WriteLine("2. Non");
            WouldYouLikeToSave(TestUserInput(1, 2));
        }

        public void WouldYouLikeToSave(int answer)
        {
            switch (answer)
            {
                case 0:
                    System.Environment.Exit(-1);
                    break;
                case 1:
                    SaveManager saveManager = new SaveManager();
                    Console.Write("\nNom : ");
                    string name = Console.ReadLine().Replace(" ", "_");
                    saveManager.Save(name);
                    System.Environment.Exit(-1);
                    break;
            }
            
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

        public void DisplaySavesMenu()
        {
            SaveManager saveManager = new SaveManager();

            int index = 0;

            Console.WriteLine("Veuillez choisir un numero de sauvegarde dans la liste ci dessous :");
            foreach (String saveName in saveManager.Saves)
            {
                string formatedString = index++ + saveName.Replace("..\\..\\..\\saves\\", ". ").Replace(".xml","");
                Console.WriteLine(formatedString);
            }
            Console.WriteLine($"{index}. Retour");
            int selected = TestUserInput(0, saveManager.Saves.Count + 1);
            if(selected == (saveManager.Saves.Count))
            {
                Console.Clear();
                DisplayMenu();
            }
            else {
                saveManager.LoadAndUpdateObjects(saveManager.Saves[selected]);
                DisplayMenu();
            }
            
        }



       

    }

    }
