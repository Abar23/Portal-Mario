using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Media;

namespace MarioGame.Sounds
{
    class MusicPlayer : AudioPlayer
    {
        private static MusicPlayer instance;
        private Song music;
        private MusicPlayer(Song song) : base()
        {
            this.music = song;
            MediaPlayer.Play(this.music);
            MediaPlayer.IsRepeating = true;
        }

        public static MusicPlayer CreateInstance(Song song)
        {
            return instance = new MusicPlayer(song);
        }

        public static MusicPlayer GetInstance()
        {
            return instance;
        }

        public static void StopPlayer()
        {
            MediaPlayer.Stop();
        }

        public void ResetPlayer()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(this.music);
        }

        public override void Mute()
        {
            this.isMute = !this.isMute;
            if (this.isMute)
            {
                MediaPlayer.Volume = 0;
            }
            else
            {
                MediaPlayer.Volume = 1;
            }
        }
    }
}
