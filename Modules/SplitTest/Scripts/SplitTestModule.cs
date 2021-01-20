using System.Collections.Generic;
using Orego.Utils;
using OregoFramework.Util;
using UnityEngine;

namespace Orego.Module
{
    public abstract class SplitTestModule : UnitySingleton<SplitTestModule>
    {
        #region Const

        private const string PREF_KEY_FORMAT = "SplitTester/{0}";

        #endregion

        private readonly Dictionary<string, string> stateMap;

        [SerializeField]
        private SplitTest[] parameters;

        public SplitTestModule()
        {
            this.stateMap = new Dictionary<string, string>();
        }

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public static void Initialize()
        {
            _instance.InitializeInternal();
        }

        private void InitializeInternal()
        {
            foreach (var parameter in this.parameters)
            {
                this.InitializeParameter(parameter);
            }
        }

        protected void InitializeParameter(SplitTest test)
        {
            var key = string.Format(PREF_KEY_FORMAT, test.name);
            string state;
            if (PlayerPrefs.HasKey(key))
            {
                state = PlayerPrefs.GetString(key);
            }
            else
            {
                state = test.states.GetRandom();
                PlayerPrefs.SetString(key, state);
            }

            this.stateMap.Add(key, state);
        }

        public string GetState(string testName)
        {
            return this.stateMap[testName];
        }
    }
}