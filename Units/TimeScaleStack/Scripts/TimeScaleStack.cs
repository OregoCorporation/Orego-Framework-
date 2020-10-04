using System;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Unit
{
    public abstract class TimeScaleStack : Element, IRootElement
    {
        private static TimeScaleStack instance;

        private readonly Stack<TimeScale> stack;

        private TimeScale currentScale;

        protected TimeScaleStack()
        {
            this.stack = new Stack<TimeScale>();
        }

        protected sealed override void OnCreate(Element _, IElementContext context)
        {
            instance = this;
            this.currentScale = new TimeScale(Time.timeScale);
            this.OnCreate(this, context);
        }

        protected virtual void OnCreate(TimeScaleStack _, IElementContext context)
        {
        }

        public static void PushScale(TimeScale currentScale)
        {
            instance.PushScaleInternal(currentScale);
        }

        protected virtual void PushScaleInternal(TimeScale currentScale)
        {
            var previousScale = this.currentScale;
            this.stack.Push(previousScale);
            this.SetTimeScaleNode(currentScale);
        }

        public static void PopScale(TimeScale removedScale)
        {
            instance.PopScaleInternal(removedScale);
        }

        protected virtual void PopScaleInternal(TimeScale removedScale)
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