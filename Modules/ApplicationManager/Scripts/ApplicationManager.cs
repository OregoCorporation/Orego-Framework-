using System;
using System.Collections;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public sealed class ApplicationManager : LazyUnitySingleton<ApplicationManager>
    {
        #region Event

        public event Action<float> OnUpdateEvent;

        public event Action<float> OnFixedUpdateEvent;

        public event Action OnApplicationPausedEvent;

        public event Action OnApplicationResumedEvent;

        public event Action OnApplicationFocusedEvent;

        public event Action OnApplicationReleasedEvent;

        public event Action OnApplicationQuitEvent;

        #endregion
        
        public static float unscaledDeltaTime { get; private set; }

        public static float fixedDeltaTime { get; private set; }

        public static Coroutine StartCoroutineStatic(IEnumerator routine)
        {
            return instance.StartCoroutine(routine);
        }
        
        private void Awake()
        {
            unscaledDeltaTime = Time.unscaledDeltaTime;
            fixedDeltaTime = Time.fixedDeltaTime;
        }
        
        private void Update()
        {
            this.OnUpdateEvent?.Invoke(unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            this.OnFixedUpdateEvent?.Invoke(fixedDeltaTime);
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                this.OnApplicationPausedEvent?.Invoke();
            }
            else
            {
                this.OnApplicationResumedEvent?.Invoke();
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                this.OnApplicationFocusedEvent?.Invoke();
            }
            else
            {
                this.OnApplicationReleasedEvent?.Invoke();
            }
        }

        private void OnApplicationQuit()
        {
            this.OnApplicationQuitEvent?.Invoke();
        }
    }
}