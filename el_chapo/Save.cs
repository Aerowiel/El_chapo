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
        public int SeasonMaMa { get; set; }
        public int MatchThisSeasonMaMa { get; set; }

        //MoneyManager.cs important variables
        public double MoneyMoMa { get; set; }

        //Hisory

        public List<History> HistoryList { get; set; }

        public Save()
        {
            //MatchManager
            CatcheurList = new List<Catcheur>();
            CatcheurList = MatchManager.instance.Catcheurs;
            SeasonMaMa = MatchManager.instance.Season;
            MatchThisSeasonMaMa = MatchManager.instance.MatchThisSeason;

            //MoneyManager
            MoneyMoMa = MoneyManager.instance.Money;

            //HistoryManager
            HistoryList = new List<History>();
            HistoryList = HistoryManager.instance.HistoryCatcheur;
        }

        
    }
}
