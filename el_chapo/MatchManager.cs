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

        private int opATM;
        private Catcheur[] concurants;

        public MatchManager()
        {
            //Init catcheurs
            Catcheurs = new List<Catcheur>();

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

            //Cache
            concurants = new Catcheur[2];
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

        public void chooseCatcheurs()
        {
            concurants = new Catcheur[2];

            int tempP1;
            int tempP2 = 666;

            Console.WriteLine("Choisissez le 1er catcheur : ");
            tempP1 = MenuManager.instance.TestUserInput(0, opATM);

            do
            {
                if(tempP2 == 666)
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


        }


    }
}
