using System;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Unit
{
    public sealed class UnityProvider : MonoBehaviour
    {
        #region Const

        private const string NAME = "UnityProvider";

        #endregion
        
        private static UnityProvider _instance;

        private static bool isCreated;

        #region Event

        public event Action<float> OnUpdateEvent;

        public event Action<float> OnFixedUpdateEvent;

        public event Action OnApplicationPausedEvent;

        public event Action OnApplicationResumedEvent;

        public event Action OnApplicationFocusedEvent;

        public event Action OnApplicationReleasedEvent;

        public event Action OnApplicationQuitEvent;

        #endregion
        
        public float unscaledDeltaTime { get; private set; }

        public float fixedDeltaTime { get; private set; }

        public static UnityProvider instance
        {
            get
            {
                if (isCreated)
                {
                    return _instance;
                }

                CreateSingleton(out _instance);
                return _instance;
            }
        }

        private static void CreateSingleton(out UnityProvider singleton)
        {
            var dispatcher = new GameObject(NAME)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            DontDestroyOnLoad(dispatcher);
            singleton = dispatcher.AddComponent<UnityProvider>();
            isCreated = true;
        }

        private void Awake()
        {
            this.unscaledDeltaTime = Time.unscaledDeltaTime;
            this.fixedDeltaTime = Time.fixedDeltaTime;
        }
        
        private void Update()
        {
            this.OnUpdateEvent?.Invoke(this.unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            this.OnFixedUpdateEvent?.Invoke(this.fixedDeltaTime);
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