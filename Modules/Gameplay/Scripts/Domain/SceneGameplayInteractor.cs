using System;
using System.Collections;
using System.Collections.Generic;
using Gullis;
using OregoFramework.App;
using OregoFramework.Module;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class SceneGameplayInteractor<T> : Interactor where T : SceneGameContext
    {
        #region Events

        public event Action<object, T> OnGameCreatedEvent;

        public event Action<object, T> OnGameDestroyedEvent;

        #endregion

        public T gameContext { get; private set; }

        private IEnumerable<SceneGameLoader> sceneLoaders;

        protected TimeScale playingTimeScale { get; set; } = new TimeScale(Float.ONE);

        protected TimeScale pauseTimeScale { get; set; } = new TimeScale(Float.ZERO);

        protected override void OnCreate()
        {
            base.OnCreate();
            this.sceneLoaders = this.CreateElements<SceneGameLoader>();
        }

        #region GameContextLifecycle

        public void CreateGameContext(object sender, T prefab)
        {
            if (this.gameContext != null)
            {
                throw new Exception($"Game context is already created: {this.gameContext.name}!");
            }

            this.gameContext = GameObject.Instantiate(prefab, null);
            this.gameContext.name = prefab.name;
            this.RegisterSceneLoadersToContext();
            this.SubscribeOnContext();
            this.OnGameCreatedEvent?.Invoke(sender, this.gameContext);
        }

        private void RegisterSceneLoadersToContext()
        {
            foreach (var loader in this.sceneLoaders)
            {
                this.gameContext.RegisterLoader(loader);
            }
        }

        private void SubscribeOnContext()
        {
            this.gameContext.OnGamePreparedEvent += this.OnGamePrepared;
            this.gameContext.OnGameReadyEvent += this.OnGameReady;
            this.gameContext.OnGameStartedEvent += this.OnGameStarted;
            this.gameContext.OnGamePausedEvent += this.OnGamePaused;
            this.gameContext.OnGameResumedEvent += this.OnGameResumed;
            this.gameContext.OnGameFinishedEvent += this.OnGameFinihsed;
        }

        public void LaunchGameContext(object sender)
        {
            this.gameContext.PrepareGame(sender);
        }

        protected virtual void OnGamePrepared(object sender)
        {
            ApplicationManager.StartCoroutineStatic(this.DoInNextFrame(() =>
                this.gameContext.ReadyGame(sender)));
        }

        protected virtual void OnGameReady(object sender)
        {
            ApplicationManager.StartCoroutineStatic(this.DoInNextFrame(() =>
                this.gameContext.StartGame(sender)));
        }

        protected virtual void OnGameStarted(object sender)
        {
            TimeScaleStack.PushScale(this.playingTimeScale);
        }

        private void OnGamePaused(object sender)
        {
            TimeScaleStack.PushScale(this.pauseTimeScale);
        }

        private void OnGameResumed(object sender)
        {
            TimeScaleStack.PopScale(this.pauseTimeScale);
        }

        protected virtual void OnGameFinihsed(object sender)
        {
            TimeScaleStack.PopScale(this.playingTimeScale);
        }

        public void DestroyGameContext(object sender)
        {
            if (this.gameContext is null)
            {
                throw new Exception("Game context is null!");
            }

            var gameContext = this.gameContext;
            if (gameContext.Status < GameStatus.FINISHING)
            {
                gameContext.FinishGame(sender);
            }
             
            this.UnregisterSceneLoadersFromContext(gameContext);
            this.UnsubscribeFromContext(gameContext);
            this.OnGameDestroyedEvent?.Invoke(sender, gameContext);
            GameObject.Destroy(gameContext.gameObject);
            this.gameContext = null;
        }

        private void UnregisterSceneLoadersFromContext(T gameContext)
        {
            foreach (var loader in this.sceneLoaders)
            {
                gameContext.UnregisterLoader(loader);
            }
        }

        private void UnsubscribeFromContext(T gameContext)
        {
            gameContext.OnGamePreparedEvent -= this.OnGamePrepared;
            gameContext.OnGameReadyEvent -= this.OnGameReady;
            gameContext.OnGameStartedEvent -= this.OnGameStarted;
            gameContext.OnGamePausedEvent -= this.OnGamePaused;
            gameContext.OnGameResumedEvent -= this.OnGameResumed;
            gameContext.OnGameFinishedEvent -= this.OnGameFinihsed;
        }

        #endregion

        private IEnumerator DoInNextFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}