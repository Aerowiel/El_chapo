using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum CatcheurType { Agile, Brute }
    public enum CatcheurState { Opérationnel, Convalescent, Mort }

    public class Catcheur
    {
        public string Pseudo { get; set; }
        public CatcheurType CatcheurType { get; set; }
        public CatcheurState CatcheurState { get; set; }
        public int Vie { get; set; }
        public int Defense { get; set; }
        public int Attaque { get; set; }
        

        public Catcheur(string pseudo, CatcheurType type, CatcheurState state)
        {
            // Init
            Pseudo = pseudo;
            CatcheurType = type;
            CatcheurState = state;

            switch (type)
            {
                case CatcheurType.Agile:
                    Vie = 125;
                    Attaque = 3;
                    Defense = 3;
                    break;
                case CatcheurType.Brute:
                    Vie = 100;
                    Attaque = 5;
                    Defense = 1;
                    break;
            }
           
        }

       public string Describe(int index)
        {
            string description = $"{index}. NOM : {Pseudo} | TYPE : {CatcheurType} | HP : {Vie} | Attaque : {Attaque} | Defense : {Defense}";

            return description;
        }
    }
}
