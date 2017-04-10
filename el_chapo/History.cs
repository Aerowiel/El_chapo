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
        public string VictoryCatcheur { get; set; } = "";
        public string LooserCatcheur { get; set; }
        public WinState WinState { get; set; }
        public int Iteration { get; set; }
        public double Gain { get; set; }
        
        

        public History(string victoryCatcheur, string looserCatcheur, WinState winState, int iteration, double gain )
        {
            // Init
            VictoryCatcheur = victoryCatcheur;
            LooserCatcheur = looserCatcheur;
            WinState = winState;
            Iteration = iteration;
            Gain = gain;
        }

        public string DescribeHistory()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" {VictoryCatcheur} a  gagné contre {LooserCatcheur} au bout de {Iteration} round par {WinState} et a remporté {Gain} $ ");

            return sb.ToString();
        }

   


    }
}
