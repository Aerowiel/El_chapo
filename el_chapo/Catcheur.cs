using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum CatcheurType { Agile, Brute }
    public enum CatcheurState { Opérationnel, Convalescent, Mort }
    public enum CatcheurAction { Attaque, Defend, AttaqueSpecial }

    public class Catcheur
    {
        public string Pseudo { get; set; }
        public CatcheurType CatcheurType { get; set; }
        public CatcheurState CatcheurState { get; set; }
        public int Vie { get; set; }
        public int Defense { get; set; }
        public int Attaque { get; set; }

        public CatcheurAction action { get; set; }
       


        public Catcheur(string pseudo, CatcheurType type, CatcheurState state)
        {
            // Init
            Pseudo = pseudo;
            CatcheurType = type;
            CatcheurState = state;

            switch (type)
            {
                case CatcheurType.Agile:
                    Vie = 2; //125
                    Attaque = 3;
                    Defense = 3;
                    break;
                case CatcheurType.Brute:
                    Vie = 3; //100
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

        public void ChooseAction()
        {
            // On crée un objet de type Random qui permet de générer des nombres aléatoire (dice = dés)
            
            int action = MatchManager.dice.Next(1, 4);

            switch (action)
            {
                case 1:
                    this.action = CatcheurAction.Attaque;
                    
                    break;
                case 2:
                    this.action = CatcheurAction.Defend;
                    
                    
                    break;
                case 3:
                    this.action = CatcheurAction.AttaqueSpecial;
                    
                    
                    break;
            }  
        }

        public Boolean AttaqueCible(Catcheur cible, int? defense)
        {
            if ((cible.Vie + cible.Defense) - this.Attaque > 0)
            {
                cible.Vie = (cible.Vie + cible.Defense) - this.Attaque;
                return true;
            }
            else
            {
                cible.Vie = 0;
                cible.CatcheurState = CatcheurState.Mort;
                return false;
            }
        }
    }
}
