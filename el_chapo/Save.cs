using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    public class Save
    {
        public DateTime DateLastPlayed { get; set; }
        public DateTime DateCreated { get; set; }

        //MatchManager.cs important variables
        public List<Catcheur> CatcheurList { get; set; }
        public int Season { get; set; }
        public int MatchThisSeason { get; set; }

        //MoneyManager.cs important variables
        public double Money { get; set; }

        //Hisory

        public List<History> HistoryList { get; set; }

        public Save()
        {
            CatcheurList = new List<Catcheur>();
            HistoryList = new List<History>();
        }

        public Save(DateTime lastPlayed, DateTime dateCreated)
        {
            //MatchManager
            Console.WriteLine(Contacts.Catcheurs.Count); //11

            CatcheurList = Contacts.Catcheurs;
            Season = MatchManager.instance.Season;
            MatchThisSeason = MatchManager.instance.MatchThisSeason;

            //MoneyManager
            Money = MoneyManager.instance.Money;

            //HistoryManager
            HistoryList = HistoryManager.instance.HistoryCatcheur;
        }

        
    }
}
