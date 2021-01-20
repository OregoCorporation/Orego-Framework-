using System;
using UnityEngine;

namespace Orego.Module
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