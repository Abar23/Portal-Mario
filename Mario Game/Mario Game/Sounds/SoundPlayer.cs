using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace MarioGame.Sounds
{
    class SoundPlayer : AudioPlayer
    {
        private static SoundPlayer instance;

        private Dictionary<SoundEffectNames, SoundEffect> sfxList;
        private HashSet<SoundEffectInstance> soundInstances;

        private SoundPlayer() : base()
        {
            this.sfxList = new Dictionary<SoundEffectNames, SoundEffect>();
            this.soundInstances = new HashSet<SoundEffectInstance>();
        }

        public static SoundPlayer CreateInstance()
        {
            return instance = new SoundPlayer();
        }

        public static SoundPlayer GetInstance()
        {
            return instance;
        }

        public void StopAllSounds()
        {
            foreach (SoundEffectInstance soundEffectInstance in soundInstances)
            {
                soundEffectInstance.Stop();
            }
            soundInstances.Clear();
        }

        public void MuteAllSounds()
        {
            foreach (SoundEffectInstance soundEffectInstance in soundInstances)
            {
                soundEffectInstance.Volume = 0;
            }
        }

        public void UnmuteAllSounds()
        {
            foreach (SoundEffectInstance soundEffectInstance in soundInstances)
            {
                soundEffectInstance.Volume = 1;
            }
        }

        public void addSFX(Enum sfxName, SoundEffect sfx)
        {
            sfxList.Add((SoundEffectNames)sfxName , sfx);
        }

        public void PlaySoundEffect(SoundEffectNames effect)
        {
            SoundEffectInstance soundEffectInstance = this.sfxList[effect].CreateInstance();
            if (this.isMute)
            {
                soundEffectInstance.Volume = 0;
            }
            soundEffectInstance.Play();
            this.soundInstances.Add(soundEffectInstance);
        }

        public override void Mute()
        {
            this.isMute = !this.isMute;
            if (this.isMute)
            {
                MuteAllSounds();
            } else
            {
                UnmuteAllSounds();
            }
        }
    }
}
