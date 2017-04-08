using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{

    public enum WinState{ KO, PAR_DELAI }
   



    class History
    {
        public string VictoryCatcheur { get; set; }
        public string LooserCatcheur { get; set; }
        public WinState WinState { get; set; }
        public int Itération { get; set; }
        
        

        public History(string victoryCatcheur, string looserCatcheur, WinState winstate, int itération)
        {
            // Init
            VictoryCatcheur = victoryCatcheur;
            LooserCatcheur = looserCatcheur;
            WinState = WinState;
            Itération = Itération;
        }

        public string DescribeHistory(int index)
        {
            string description = $" {VictoryCatcheur} a  gagné contre {LooserCatcheur} au bout de {Itération} round par {WinState} et a remporté 30 000 boulas, OH YEAH! ";

            return description;
        }
    }
}
