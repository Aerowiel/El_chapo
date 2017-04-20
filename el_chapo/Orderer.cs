using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class Orderer
    {
        public static void OrderByPseudo(List<Catcheur> Catcheurs) // tri par ordre alphabetique 
        {
            Catcheurs.Sort((x, y) => string.Compare(x.Pseudo, y.Pseudo));
        }

        public static List<Catcheur> GetOrderedList()
        {
            List<Catcheur> catcheurFull = new List<Catcheur>();
            catcheurFull.AddRange(Contacts.GetCatcheurOp());
            catcheurFull.AddRange(Contacts.GetCatcheurConv());
            catcheurFull.AddRange(Contacts.GetCatcheurMort());
            return catcheurFull;
        }


    }
}
