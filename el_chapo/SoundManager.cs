using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace el_chapo
{
    
    class SoundManager
    {
        public static SoundManager instance = new SoundManager();

        public  void playSimpleSoundCina()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\AND GOODBYE.wav");
            simpleSound.Play();
           Thread.Sleep(2000);

        }

        public  void playSimpleSoundPunch()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\Punch.wav");
            simpleSound.Play();
           Thread.Sleep(2000);
        }

        public void playSimpleSoundDefend()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\Defend.wav");
            simpleSound.Play();
          Thread.Sleep(2000);
        }

        public  void  playSimpleSoundKameha()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\kamehameha.wav");
            simpleSound.Play();
           Thread.Sleep(6000);
        }

        public void playSimpleSoundMort()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\MortTroll.wav");
            simpleSound.Play();
            Thread.Sleep(10000);
        }


    }
}
