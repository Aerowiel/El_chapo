using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class GameManager
    {
        public MenuManager menuManager { get; set; }
        public MatchManager matchManager { get; set; }
        public MoneyManager MoneyManager { get; set; }
        public HistoryManager HistoryManager { get; set; }


        public GameManager()
        {
            menuManager = new MenuManager();
            matchManager = new MatchManager();
            MoneyManager = new MoneyManager();
            HistoryManager = new HistoryManager();

            menuManager.AskToLoadOrCreate();
        }
    }
}
