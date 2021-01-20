using System;
using System.Collections.Generic;
using Elementary;
using Orego.Utils;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public sealed class TimeScaleStack : LazyUnitySingleton<TimeScaleStack>
    {
        private readonly Stack<TimeScale> stack;

        private TimeScale currentScale;
        
        public TimeScaleStack()
        {
            this.stack = new Stack<TimeScale>();
        }

        private void Awake()
        {
            this.currentScale = new TimeScale(Time.timeScale);
        }

        public static void PushScale(TimeScale currentScale)
        {
            instance.PushScaleInternal(currentScale);
        }

        private void PushScaleInternal(TimeScale currentScale)
        {
            var previousScale = this.currentScale;
            this.stack.Push(previousScale);
            this.SetTimeScaleNode(currentScale);
        }

        public static void PopScale(TimeScale removedScale)
        {
            instance.PopScaleInternal(removedScale);
        }

        private void PopScaleInternal(TimeScale removedScale)
        {
            if (this.stack.IsEmpty())
            {
                throw new Exception("Stack is empty!");
            }

            if (this.currentScale != removedScale)
            {
                while (this.stack.Peek() != removedScale)
                {
                    this.stack.Pop();
                }
            }

            var previousScale = this.stack.Pop();
            this.SetTimeScaleNode(previousScale);
        }

        private void SetTimeScaleNode(TimeScale currentScale)
        {
            this.currentScale = currentScale;
            Time.timeScale = currentScale.value;
        }
    }
}