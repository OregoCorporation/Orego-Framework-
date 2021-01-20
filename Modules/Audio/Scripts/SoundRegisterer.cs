using UnityEngine;

namespace OregoFramework.Module
{
    public class SoundRegisterer : AudioRegisterer
    {
        public override void RegisterSource(AudioSource source)
        {
            SoundModule.RegisterAudioSource(source);
        }

        public override void UnregisterSource(AudioSource source)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }

            SoundModule.UnregisterAudioSource(source);
        }
    }
}