using UnityEngine;

namespace OregoFramework.Module
{
    public interface IAudioRegisterer
    {
        void RegisterSource(AudioSource source);

        void UnregisterSource(AudioSource source);
    }
    
    public abstract class AudioRegisterer : MonoBehaviour, IAudioRegisterer
    {
        [SerializeField]
        protected AudioSource[] sources;
        
        public abstract void RegisterSource(AudioSource source);

        public abstract void UnregisterSource(AudioSource source);

        protected virtual void OnEnable()
        {
           this.RegisterSources();
        }

        protected virtual void OnDisable()
        {
            this.UnregisterSources();
        }

        protected void UnregisterSources()
        {
            foreach (var source in this.sources)
            {
                this.UnregisterSource(source);
            }
        }

        protected void RegisterSources()
        {
            foreach (var source in this.sources)
            {
                this.RegisterSource(source);
            }
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            this.sources = this.GetComponentsInChildren<AudioSource>();
        }
#endif
    }
}