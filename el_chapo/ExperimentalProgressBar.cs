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

            string progressBar = " ";

            while (count < lenght)
            {
                Console.BackgroundColor = color;
                
                Console.Write(progressBar);
                Console.SetCursorPosition(Console.CursorLeft - (progressBar.Length - 1), Console.CursorTop);
                count++;
                Thread.Sleep(300);

            }

            Console.ResetColor();
            Console.Clear();
        }
    }
}
