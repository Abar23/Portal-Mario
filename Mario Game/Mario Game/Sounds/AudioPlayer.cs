using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Sounds
{
    abstract class AudioPlayer
    {
        public bool isMute;

        public AudioPlayer()
        {
            this.isMute = false;
        }

        public abstract void Mute();
    }
}
