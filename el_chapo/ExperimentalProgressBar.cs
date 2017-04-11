using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace el_chapo
{
    class ExperimentalProgressBar
    {
        private int lenght;
        private ConsoleColor color;

        public ExperimentalProgressBar(ConsoleColor selectedColor, int selectedLenght)
        {
            color = selectedColor;
            lenght = selectedLenght;
        }

        public void DisplayProgressBar()
        {
            int count = 0;

            string progressBar = "";
            while (count < lenght)
            {
                progressBar = "";
                Console.BackgroundColor = color;
                
                for(int i = 0; i < count; i++)
                {
                    progressBar = progressBar +  " ";
                }
                
                Console.Write(progressBar);
                Console.SetCursorPosition(Console.CursorLeft - progressBar.Length, Console.CursorTop);
                count++;
                Thread.Sleep(300);

            }
            
            Console.ResetColor();
            Console.Clear();
        }
    }
}
