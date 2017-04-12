using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class HistoryManager
    {
        public static  HistoryManager instance = new HistoryManager();
        public List<History> HistoryCatcheur { get; set; }
        public List<History> HistoryCatcheurKO { get; set; }
        public List<History> HistoryCatcheurTKO { get; set; }
        public StringBuilder menuHistoryContent;

        public HistoryManager()
        {
            HistoryCatcheur = new List<History>();
        }

        public void DisplayMenuHistory() //menu pour le choix du filtre 
        {
            menuHistoryContent = new StringBuilder();
            menuHistoryContent.AppendLine("0 - Filtrer l'historique par KO");
            menuHistoryContent.AppendLine("1 - Filtrer l'hisotrique par DELAI");
            menuHistoryContent.AppendLine("2 - Revenir au menu principal");
            Console.WriteLine(menuHistoryContent);
            FilterWinnerByKO_OR_WinnerByTKO(MenuManager.instance.TestUserInput(0 , 3));

        }

        public void Addhistory(Catcheur victory, Catcheur perdant, WinState state, int round, double gain )
        { 
            HistoryCatcheur.Add(new History(victory.Pseudo, perdant.Pseudo, state , round, gain));
        }

        public void FilterWinnerByKO_OR_WinnerByTKO( int choix)
        { 
            switch(choix)
            {
                case 0:
                    Console.WriteLine(DisplayCatcheurHistoryByKOList());
                    break;

                case 1:
                    Console.WriteLine(DisplayCatcheurHistoryByTKOList());
                    break;

                case 2:
                    MenuManager.instance.RetourMainMenuInstant();
                    break;
            }
            
        }

        public StringBuilder DisplayCatcheurHistoryByKOList()
        {
            StringBuilder sb = new StringBuilder();
            HistoryCatcheurKO = new List<History>();
           
            // Affiche uniquement les catcheurs OP 

            foreach (History history in HistoryCatcheur)
            {
                if (history.WinState == WinState.KO)
                {
                    HistoryCatcheurKO.Add(history);
                    sb.AppendLine(history.DescribeHistory());
                }
            }
            
            return sb;
        }

        public StringBuilder DisplayCatcheurHistoryByTKOList()
        {
            StringBuilder sb = new StringBuilder();
            HistoryCatcheurTKO = new List<History>();

            foreach (History history in HistoryCatcheur)
            {

                if (history.WinState == WinState.PAR_DELAI)
                {
                    HistoryCatcheurTKO.Add(history);
                    sb.AppendLine(history.DescribeHistory());
                }
            }
            return sb;
        }
    

        public StringBuilder DisplayHistory()
        {
            StringBuilder sb = new StringBuilder();
            

            foreach (History history in HistoryCatcheur)
            {
                
                sb.AppendLine(history.DescribeHistory());
            }
            return sb;
        }


    }
}
