using UnityEngine;

namespace OregoFramework.Module
{
    public class MusicRegisterer : AudioRegisterer
    {
        public override void RegisterSource(AudioSource source)
        {
            MusicModule.RegisterAudioSource(source);
        }

        public override void UnregisterSource(AudioSource source)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
            
            MusicModule.UnregisterAudioSource(source);
        }
    }
}