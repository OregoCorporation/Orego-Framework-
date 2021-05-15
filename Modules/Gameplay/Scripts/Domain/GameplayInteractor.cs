using System;
using System.Collections;
using System.Collections.Generic;
using GameElementary;
using OregoFramework.App;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class GameplayInteractor<T> : Interactor where T : MonoBehaviour, IGameSystem
    {
        #region Events

        public event Action<object, T> OnGameCreatedEvent;

        public event Action<object, T> OnDestroyGameEvent;

        #endregion

        public T GameContext { get; private set; }

        public bool HasGame
        {
            get { return !ReferenceEquals(this.GameContext, null); }
        }

        protected IEnumerable<SceneGameLoader> SceneLoaders;

        protected TimeScale PlayingTimeScale { get; set; } = new TimeScale(Float.ONE);

        protected TimeScale PauseTimeScale { get; set; } = new TimeScale(Float.ZERO);

        protected override void OnCreate()
        {
            base.OnCreate();
            this.SceneLoaders = this.CreateElements<SceneGameLoader>();
        }

        #region GameLifecycle

        public virtual void CreateGame(object sender, T prefab, Transform parent = null)
        {
            if (!ReferenceEquals(this.GameContext, null))
            {
                throw new Exception($"Game context is already created: {this.GameContext.name}!");
            }

            var context = GameObject.Instantiate(prefab, parent);
            this.RegisterLoaders(context);
            this.Subscribe(context);

            this.GameContext = context;
            this.GameContext.name = prefab.name;
            this.OnGameCreatedEvent?.Invoke(sender, this.GameContext);
        }

        public virtual void LaunchGame(object sender)
        {
            this.GameContext.PrepareGame(sender);
        }

        protected virtual void OnGamePrepared(object sender)
        {
            ApplicationManager.StartCoroutineStatic(this.AfterEndOfFrame(() =>
                this.GameContext.ReadyGame(sender)));
        }

        protected virtual void OnGameReady(object sender)
        {
            ApplicationManager.StartCoroutineStatic(this.AfterEndOfFrame(() =>
                this.GameContext.StartGame(sender)));
        }

        protected virtual void OnGameStarted(object sender)
        {
            TimeScaleManager.PushScale(this.PlayingTimeScale);
        }

        protected virtual void OnGamePaused(object sender)
        {
            TimeScaleManager.PushScale(this.PauseTimeScale);
        }

        protected virtual void OnGameResumed(object sender)
        {
            TimeScaleManager.PopScale(this.PauseTimeScale);
        }

        protected virtual void OnGameFinihsed(object sender)
        {
            TimeScaleManager.PopScale(this.PlayingTimeScale);
        }

        public virtual void DestroyGame(object sender)
        {
            if (ReferenceEquals(this.GameContext, null))
            {
                return;
            }

            var context = this.GameContext;
            if (context.Status < GameStatus.FINISHING)
            {
                context.FinishGame(sender);
            }

            this.UnregisterLoaders(context);
            this.Unsubscribe(context);

            this.GameContext = null;
            this.OnDestroyGameEvent?.Invoke(sender, context);
            GameObject.Destroy(context.gameObject);
        }
        
        #endregion
        
        protected void RegisterLoaders(T context)
        {
            if (!(context is ISceneGameSystem sceneContext))
            {
                return;
            }

            foreach (var loader in this.SceneLoaders)
            {
                sceneContext.RegisterLoader(loader);
            }
        }

        protected void UnregisterLoaders(T context)
        {
            if (!(context is ISceneGameSystem sceneContext))
            {
                return;
            }

            foreach (var loader in this.SceneLoaders)
            {
                sceneContext.UnregisterLoader(loader);
            }
        }

        protected void Subscribe(T context)
        {
            context.OnGamePrepared += this.OnGamePrepared;
            context.OnGameReady += this.OnGameReady;
            context.OnGameStarted += this.OnGameStarted;
            context.OnGamePaused += this.OnGamePaused;
            context.OnGameResumed += this.OnGameResumed;
            context.OnGameFinished += this.OnGameFinihsed;
        }

        protected void Unsubscribe(T context)
        {
            context.OnGamePrepared -= this.OnGamePrepared;
            context.OnGameReady -= this.OnGameReady;
            context.OnGameStarted -= this.OnGameStarted;
            context.OnGamePaused -= this.OnGamePaused;
            context.OnGameResumed -= this.OnGameResumed;
            context.OnGameFinished -= this.OnGameFinihsed;
        }

        protected IEnumerator AfterEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}