using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace el_chapo
{
    public enum CatcheurType {
        [XmlEnum(Name = "Agile")]
        Agile,
        [XmlEnum(Name = "Brute")]
        Brute
    }
    public enum CatcheurState {
        [XmlEnum(Name = "Opérationnel")]
        Opérationnel,
        [XmlEnum(Name = "Convalescent")]
        Convalescent,
        [XmlEnum(Name = "Mort")]
        Mort
    }
    public enum CatcheurAction {
        [XmlEnum(Name = "Attack")]
        Attack,
        [XmlEnum(Name = "Defend")]
        Defend,
        [XmlEnum(Name = "SpecialAttack")]
        SpecialAttack,
        [XmlEnum(Name = "SpeAttackFailed")]
        SpeAttackFailed,
        [XmlEnum(Name = "Heal")]
        Heal
    }
    

    public class Catcheur
    {
        public string Pseudo { get; set; }
        public CatcheurType CatcheurType { get; set; }
        public CatcheurState CatcheurState { get; set; }
       
        public int Health { get; set; }
        public int maxHealth { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }

        public CatcheurAction action { get; set; }

        public int BonusAttack { get; set; }
        public int BonusDefense { get; set; }
        public int BonusHeal { get; set; }

        public int DebuffHealth { get; set; }
        public int DebuffAttack { get; set; }
        public int DebuffDefense { get; set; }

        public SpecialAttack SpecialAtk { get; set; }

        public int DayRemainingBeforeOp { get; set; }

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
                    maxHealth = 125;
                    Attack = 3;
                    Defense = 3;
                    break;
                case CatcheurType.Brute:
                    Health = 100; //100
                    maxHealth = 100;
                    Attack = 5;
                    Defense = 1;
                    break;
            }

            SpecialAtk = specialAtk;
           
        }

        //Sert à pouvoir sérialisé la classe, il faut qu'il y ai un constructeur vide dans la classe. :p
        public Catcheur()
        {

        }

       public string Describe(int index)
        {
            StringBuilder sb = new StringBuilder();
            string description = $"{index}. NOM : {Pseudo} | TYPE : {CatcheurType} | HP : {Health} | Attack : {Attack} | Defense : {Defense} | Attaque Spéciale : {SpecialAtk} | Statut : {CatcheurState}";
            if (CatcheurState == CatcheurState.Convalescent)
            {
                description = description + $" (jour(s) restant(s) : {DayRemainingBeforeOp})";
            }
            sb.Append(description);

            return sb.ToString();
        }

        public string Describe()
        {
            StringBuilder sb = new StringBuilder();
            string description = $"NOM : {Pseudo} | TYPE : {CatcheurType} | HP : {Health} | Attack : {Attack} | Defense : {Defense} | Attaque Spéciale : {SpecialAtk} | Statut : {CatcheurState}";
            if (CatcheurState == CatcheurState.Convalescent)
            {
                description = description + $" (semaine(s) restante(s) : {DayRemainingBeforeOp})";
            }
            sb.Append(description);

            return sb.ToString();
        }

        public void ChooseAction()
        {
            // On crée un objet de type Random qui permet de générer des nombres aléatoire (dice = dés)
            
            int action = MatchManager.instance.dice.Next(1, 4);

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
            int targetDefense = 0;
            if(cible.action == CatcheurAction.Attack || cible.action == CatcheurAction.SpeAttackFailed || cible.action == CatcheurAction.Heal)
            {
                Console.WriteLine($"{Pseudo} attaque {cible.Pseudo} à hauteur de {Attack + BonusAttack - DebuffAttack}, {cible.Pseudo} absorbe {cible.BonusDefense} point de dégat !");
                if(this.BonusHeal > 0)
                {
                    Console.WriteLine($"{Pseudo} vol {BonusHeal}");
                    Health += BonusHeal;
                }
                else if(this.DebuffHealth > 0)
                {
                    Console.WriteLine($"{Pseudo} se prend -{DebuffHealth} point de dégat de malus dans la face !");
                    Health -= DebuffHealth;
                }
                targetDefense = 0;
            }
            else if(cible.action == CatcheurAction.Defend)
            {
                Console.WriteLine($"{Pseudo} attaque {cible.Pseudo} à hauteur de {Attack + BonusAttack - DebuffAttack}, {cible.Pseudo} absorbe {cible.Defense + cible.BonusDefense} point de dégat !");
                targetDefense = cible.Defense + cible.BonusDefense;
                if (this.BonusHeal > 0)
                {
                    Console.WriteLine($"{Pseudo} vol {BonusHeal}");
                    Health += BonusHeal;
                }
                else if (DebuffHealth > 0)
                {
                    Console.WriteLine($"{Pseudo} se prend -{DebuffHealth} point de dégat de malus !");
                    Health -= DebuffHealth;
                }
            }
            
            
            int healthCalculated;
            healthCalculated = (cible.Health + targetDefense - cible.DebuffDefense) - (this.Attack + this.BonusAttack - this.DebuffAttack);
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
                cible.CatcheurState = CatcheurState.Mort;
                cible.Health = 0;
                return false;
            }
        }


        public void Heal(int amount)
        {
                if (Health + amount <= maxHealth)
                {
                    Console.WriteLine($"{Pseudo} se soigne à hauteur de {amount} HP.");
                    this.Health += amount;
                }
                else
                {
                    Console.WriteLine($"{Pseudo} se soigne à hauteur de {maxHealth - Health} HP");
                    Health = maxHealth;
                }

        }
    }
}
