using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum CatcheurType { Agile, Brute }
    public enum CatcheurState { Opérationnel, Convalescent, Mort }
    public enum CatcheurAction { Attack, Defend, SpecialAttack, SpeAttackFailed }

    public class Catcheur
    {
        public string Pseudo { get; set; }
        public CatcheurType CatcheurType { get; set; }
        public CatcheurState CatcheurState { get; set; }
        public int Health { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }

        public CatcheurAction action { get; set; }

        public int BonusAttack { get; set; }
        public int BonusDefense { get; set; }

        public SpecialAttack SpecialAtk { get; set; }


        public Catcheur(string pseudo, CatcheurType type, CatcheurState state, SpecialAttack specialAtk)
        {
            // Init
            Pseudo = pseudo;
            CatcheurType = type;
            CatcheurState = state;

            switch (type)
            {
                case CatcheurType.Agile:
                    Health = 125; //125
                    Attack = 3;
                    Defense = 3;
                    break;
                case CatcheurType.Brute:
                    Health = 100; //100
                    Attack = 5;
                    Defense = 1;
                    break;
            }
           
        }

       public string Describe(int index)
        {
            string description = $"{index}. NOM : {Pseudo} | TYPE : {CatcheurType} | HP : {Health} | Attack : {Attack} | Defense : {Defense}";

            return description;
        }

        public void ChooseAction()
        {
            // On crée un objet de type Random qui permet de générer des nombres aléatoire (dice = dés)
            
            int action = MatchManager.dice.Next(1, 4);

            switch (action)
            {
                case 1:
                    this.action = CatcheurAction.Attack;
                    
                    break;
                case 2:
                    this.action = CatcheurAction.Defend;
                    
                    
                    break;
                case 3:
                    this.action = CatcheurAction.SpecialAttack;
                    
                    
                    break;
            }  
        }

        public Boolean AttackTarget(Catcheur cible, int cibleDefense)
        {
            int healthCalculated;
            healthCalculated = (cible.Health + cibleDefense + cible.BonusDefense) - (this.Attack + this.BonusAttack);
            if (healthCalculated > 0)
            {
                if(healthCalculated < cible.Health)
                {
                    cible.Health = healthCalculated;
                }
                // else no changes...


                return true;
            }
            else
            {
                cible.Health = 0;
                cible.CatcheurState = CatcheurState.Mort;
                return false;
            }
        }

        public Boolean AttackSpeCible(Catcheur cible, int? defense)
        {

            return false;
        }
    }
}
