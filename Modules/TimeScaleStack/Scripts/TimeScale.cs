using System;
using UnityEngine;

namespace OregoFramework.Module
{
    [Serializable]
    public sealed class TimeScale
    {
        [SerializeField]
        public float value;

        public TimeScale(float value)
        {
            this.value = value;
        }
    }
}