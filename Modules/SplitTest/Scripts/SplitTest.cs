using System;
using UnityEngine;

namespace OregoFramework.Module
{
    [Serializable]
    public sealed class SplitTest
    {
        [SerializeField]
        public string name;

        [SerializeField]
        public string[] states;
    }
}