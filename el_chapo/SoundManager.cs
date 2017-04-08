using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace el_chapo
{
    class SoundManager
    {

        public static void playSimpleSoundCina()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\AND GOODBYE.wav");
            simpleSound.Play();
        }

        public static void playSimpleSoundPunch()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\Punch.wav");
            simpleSound.Play();
        }

        public static void  playSimpleSoundKameha()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\..\ressource\kamehameha.wav");
            simpleSound.Play();
        }
    }
}
