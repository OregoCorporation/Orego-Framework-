using System;
using System.Collections;
using UnityEngine;

namespace OregoFramework.Util
{
    public sealed class Routine
    {
        #region Event

        public event Action OnFinishedEvent;

        public event Action OnCanceledEvent;
        
        #endregion

        private readonly MonoBehaviour dispatcher;

        private Coroutine routine;

        public Routine(MonoBehaviour dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #region Run

        public void Run(IEnumerator enumerator)
        {
            if (this.IsRunning())
            {
                throw new Exception("Routine is already running!");
            }

            var wrappedEnumerator = this.GetWrappedEnumerator(enumerator);
            var routine = this.dispatcher.StartCoroutine(wrappedEnumerator);
            this.routine = routine;
        }
        
        private IEnumerator GetWrappedEnumerator(IEnumerator enumerator)
        {
            yield return enumerator;
            this.routine = null;
            this.OnFinishedEvent?.Invoke();
        }

        #endregion

        public bool Cancel()
        {
            if (!this.IsRunning())
            {
                return false;
            }

            this.dispatcher.StopCoroutine(this.routine);
            this.routine = null;
            this.OnCanceledEvent?.Invoke();
            return true;
        }

        public bool IsRunning()
        {
            return this.routine != null && this.dispatcher.isActiveAndEnabled;
        }
    }
    
    public static partial class Extensions
    {
        #region Join

        public static IEnumerator Join(this Routine routine, Func<IEnumerator> func)
        {
            var enumerator = func.Invoke();
            yield return routine.Join(enumerator);
        }

        public static IEnumerator Join(this Routine routine, IEnumerator enumerator)
        {
            routine.Run(enumerator);
            yield return new WaitWhile(routine.IsRunning);
        }

        public static IEnumerator TryJoin(this Routine routine, Func<IEnumerator> func)
        {
            var enumerator = func.Invoke();
            yield return routine.TryJoin(enumerator);
        }

        public static IEnumerator TryJoin(this Routine routine, IEnumerator enumerator)
        {
            if (routine.IsRunning())
            {
                yield break;
            }

            yield return routine.Join(enumerator);
        }

        public static IEnumerator ForceJoin(this Routine routine, Func<IEnumerator> func)
        {
            var enumerator = func.Invoke();
            yield return routine.ForceJoin(enumerator);
        }

        public static IEnumerator ForceJoin(this Routine routine, IEnumerator enumerator)
        {
            routine.ForceRun(enumerator);
            yield return new WaitWhile(routine.IsRunning);
        }

        #endregion

        #region Run

        public static void Run(this Routine routine, Func<IEnumerator> func)
        {
            var enumerator = func.Invoke();
            routine.Run(enumerator);
        }

        public static void TryRun(this Routine routine, Func<IEnumerator> func)
        {
            var enumerator = func.Invoke();
            routine.TryRun(enumerator);
        }

        public static void TryRun(this Routine routine, IEnumerator enumerator)
        {
            if (routine.IsRunning())
            {
                return;
            }

            routine.Run(enumerator);
        }

        public static void ForceRun(this Routine routine, Func<IEnumerator> func)
        {
            var enumerator = func?.Invoke();
            routine.ForceRun(enumerator);
        }

        public static void ForceRun(this Routine routine, IEnumerator enumerator)
        {
            if (routine.IsRunning())
            {
                routine.Cancel();
            }

            routine.Run(enumerator);
        }

        #endregion
    }
}