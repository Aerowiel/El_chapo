using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class Contacts
    {
        public static List<Catcheur> Catcheurs { get; set; } = new List<Catcheur> {
            new Catcheur("L'ordonnateur des pompes funèbres", CatcheurType.Brute, CatcheurState.Convalescent, SpecialAttack.Bloque),
            new Catcheur("Jude Sunny", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.JuddyPower),
            new Catcheur("Triple Hache", CatcheurType.Agile, CatcheurState.Convalescent, SpecialAttack.HacheFoudroyante),
            new Catcheur("Dead Poule", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.DeadPoulePower),
            new Catcheur("Jarvan cinquième du nom", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque),
            new Catcheur("Madusa", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.OffensiveBlock),
            new Catcheur("John Cinéma", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.HacheFoudroyante),
            new Catcheur("Jeff Radis", CatcheurType.Brute, CatcheurState.Convalescent, SpecialAttack.Bloque),
            new Catcheur("Raie Mystérieuse", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.RaieDuC),
            new Catcheur("Chris Hart", CatcheurType.Brute, CatcheurState.Opérationnel, SpecialAttack.Bloque),
            new Catcheur("Bret Benoit", CatcheurType.Agile, CatcheurState.Opérationnel, SpecialAttack.Oneshot)
        };

        public static StringBuilder DisplayFullCatcheurList() // affiche la liste des catcheurs opérationnel et convalescnet 
        {
            StringBuilder sb = new StringBuilder();


            int index = 0;
            Orderer.OrderByPseudo(Contacts.Catcheurs);
            foreach (Catcheur catcheur in Orderer.GetOrderedList())
            {
                sb.AppendLine(catcheur.Describe(index++));
            }

            return sb;
        }

        public static List<Catcheur> GetCatcheurMort()
        {
            List<Catcheur> CatcheursMort;
            CatcheursMort = Contacts.Catcheurs.Where(Catcheurs => Catcheurs.CatcheurState == CatcheurState.Mort).ToList();
            Orderer.OrderByPseudo(CatcheursMort);
            return CatcheursMort;
        }

        public static List<Catcheur> GetCatcheurOp() // récupère les catcheurs opérationnel 
        {
            List<Catcheur> CatcheursOp;
            CatcheursOp = Contacts.Catcheurs.Where(Catcheurs => Catcheurs.CatcheurState == CatcheurState.Opérationnel).ToList();
            Orderer.OrderByPseudo(CatcheursOp);
            return CatcheursOp;
        }

        public static List<Catcheur> GetCatcheurConv() // récupère les catcheurs  convalescent 
        {

            List<Catcheur> CatcheursConv;
            CatcheursConv = Contacts.Catcheurs.Where(Catcheurs => Catcheurs.CatcheurState == CatcheurState.Convalescent).ToList();
            Orderer.OrderByPseudo(CatcheursConv);
            return CatcheursConv;
        }


        public static StringBuilder DisplayCatcheurList()// affiche la liste des catcheurs opérationnel
        {
            StringBuilder sb = new StringBuilder();
            List<Catcheur> CatcheursOpAndConv = GetCatcheurOp();
            CatcheursOpAndConv.AddRange(GetCatcheurConv());

            int index = 0;
            // Affiche uniquement les catcheurs OP 
            Orderer.OrderByPseudo(CatcheursOpAndConv);
            foreach (Catcheur catcheur in CatcheursOpAndConv)
            {
                if (catcheur.CatcheurState == CatcheurState.Opérationnel)
                {
                    sb.AppendLine(catcheur.Describe(index++));
                }
            }

            foreach (Catcheur catcheur in CatcheursOpAndConv)
            {
                if (catcheur.CatcheurState == CatcheurState.Convalescent)
                {
                    sb.AppendLine(catcheur.Describe());
                }
            }

            return sb;
        }

        public static string SearchByname()
        {
            Boolean found = false;

            do
            {
                Console.WriteLine("Séléctionnez le nom du catcheur que vous recherchez \n");
                string nameOfCatcheur = Console.ReadLine();
                foreach (Catcheur catcheur in Orderer.GetOrderedList())
                {
                    if (nameOfCatcheur == catcheur.Pseudo)
                    {
                        Console.WriteLine(catcheur.Describe());
                        found = true;
                        return nameOfCatcheur;
                    }
                }
                if (!found)
                {
                    Console.WriteLine(" Le catcheur que vous recherchez n'existe pas !");
                }
            } while (!found);
            return "";

        }

    }
}
