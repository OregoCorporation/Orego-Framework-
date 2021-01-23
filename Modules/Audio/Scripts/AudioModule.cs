using System.Collections.Generic;
using OregoFramework.Utils;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class AudioModule<T> : UnitySingleton<T> where T : AudioModule<T>
    {
        [SerializeField]
        protected AudioSource mainAudioSource;

        [SerializeField]
        private bool isEnable;

        private readonly HashSet<AudioSource> registeredAudioSources;

        #region Lifecycle

        public AudioModule()
        {
            this.registeredAudioSources = new HashSet<AudioSource>();
        }

        protected virtual void Awake()
        {
            _instance = (T) this;
            this.RegisterAudioSourceInternal(this.mainAudioSource);
        }

        protected virtual void OnEnable()
        {
            if (this.isEnable)
            {
                this.SetEnableInternal(this.isEnable);
            }
        }

        #endregion

        public static void PlayAudioClip(AudioClip clip)
        {
            _instance.PlayAudioClipInternal(clip);
        }

        protected virtual void PlayAudioClipInternal(AudioClip clip)
        {
            this.mainAudioSource.PlayOneShot(clip);
        }

        public static bool IsEnable()
        {
            return _instance.isEnable;
        }
        
        public static void SetEnable(bool isEnabled)
        {
            _instance.SetEnableInternal(isEnabled);
        }

        protected virtual void SetEnableInternal(bool isEnabled)
        {
            this.isEnable = isEnabled;
            var mute = !isEnabled;
            foreach (var audioSource in this.registeredAudioSources)
            {
                audioSource.mute = mute;
            }
        }

        public static void RegisterAudioSource(AudioSource source)
        {
            _instance.RegisterAudioSourceInternal(source);
        }

        private void RegisterAudioSourceInternal(AudioSource source)
        {
            if (this.registeredAudioSources.Add(source))
            {
                source.mute = !this.isEnable;
            }
        }

        public static void UnregisterAudioSource(AudioSource source)
        {
            _instance.UnregisterAudioSourceInternal(source);
        }

        protected virtual void UnregisterAudioSourceInternal(AudioSource source)
        {
            this.registeredAudioSources.Remove(source);
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            this.isEnable = true;
            this.mainAudioSource = this.GetComponent<AudioSource>();
        }
#endif
    }
}