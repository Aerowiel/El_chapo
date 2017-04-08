using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public enum CatcheurType { Agile, Brute }
    public enum CatcheurState { Opérationnel, Convalescent, Mort }
    public enum CatcheurAction { Attack, Defend, SpecialAttack, SpeAttackFailed, Heal }
    

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
        public int BonusHeal { get; set; }

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

            SpecialAtk = specialAtk;
           
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

        public Boolean AttackTarget(Catcheur cible)
        {
            int targetDefense;
            if(cible.action == CatcheurAction.Attack || cible.action == CatcheurAction.SpeAttackFailed)
            {
                Console.WriteLine($"{Pseudo} attaque {cible.Pseudo} à hauteur de {Attack + BonusAttack}, {cible.Pseudo} n'absorbe aucun point de dégat !");
                targetDefense = 0;
            }
            else if(cible.action == CatcheurAction.Defend)
            {
                Console.WriteLine($"{Pseudo} attaque {cible.Pseudo} à hauteur de {Attack + BonusAttack}, {cible.Pseudo} absorbe {cible.Defense + cible.BonusDefense} point de dégat !");
                targetDefense = cible.Defense + cible.BonusDefense;
            }
            else
            {
                Console.WriteLine($"{Pseudo} attaque {cible.Pseudo} à hauteur de {Attack + BonusAttack}, {cible.Pseudo} n'absorbe aucun point de dégat !");
                targetDefense = 0;
            }
            
            int healthCalculated;
            healthCalculated = (cible.Health + targetDefense) - (this.Attack + this.BonusAttack);
            //Console.WriteLine(healthCalculated);
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
                Console.WriteLine($"{cible.Pseudo} est mort sur le coup...");
                cible.Health = 0;
                cible.CatcheurState = CatcheurState.Mort;
                return false;
            }
        }


        public void Heal(int amount)
        {
            if(this.CatcheurType == CatcheurType.Agile)
            {
                if (Health + amount <= 125)
                {
                    Console.WriteLine($"{Pseudo} se soigne à hauteur de {amount} HP.");
                    this.Health += amount;
                }
                else
                {
                    Console.WriteLine($"{Pseudo} se soigne à hauteur de {125 - Health} HP");
                    Health = 125;
                }
            }
            else
            {
                if (Health + amount <= 100)
                {
                    Console.WriteLine($"{Pseudo} se soigne à hauteur de {amount} HP.");
                    this.Health += amount;
                }
                else
                {
                    Console.WriteLine($"{Pseudo} se soigne à hauteur de {100 - Health} HP");
                    this.Health = 100;
                }
            }
            
        }
    }
}
