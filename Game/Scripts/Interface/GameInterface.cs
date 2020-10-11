using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameInterface : MonoBehaviour, IGameInterface
    {
        public IGameContext currentGameContext { get; private set; }

        protected IEnumerable<IGameInterfaceForm> forms
        {
            get { return this.formMap.Values; }
        }

        private readonly Dictionary<Type, IGameInterfaceForm> formMap;

        protected GameInterface()
        {
            this.formMap = new Dictionary<Type, IGameInterfaceForm>();
        }

        public virtual void AddForm(IGameInterfaceForm form)
        {
            var type = form.GetType();
            this.formMap.Add(type, form);
            form.OnRegistered(this);
        }

        public virtual void RemoveForm(IGameInterfaceForm form)
        {
            form.OnUnregistered();
            var type = form.GetType();
            this.formMap.Remove(type);
        }

        public T GetForm<T>() where T : IGameInterfaceForm
        {
            return this.formMap.Find<T, IGameInterfaceForm>();
        }

        public IEnumerable<T> GetForms<T>() where T : IGameInterfaceForm
        {
            return this.formMap.FindAll<T, IGameInterfaceForm>();
        }

        public virtual void BindGameContext(IGameContext gameContext)
        {
            this.currentGameContext = gameContext;
            this.currentGameContext.OnGamePreparedEvent += this.OnGamePrepared;
            this.currentGameContext.OnGameReadyEvent += this.OnGameReady;
            this.currentGameContext.OnGameStartedEvent += this.OnGameStarted;
            this.currentGameContext.OnGamePausedEvent += this.OnGamePaused;
            this.currentGameContext.OnGameResumedEvent += this.OnGameResumed;
            this.currentGameContext.OnGameFinishedEvent += this.OnGameFinished;
        }

        public virtual void UnbindGameContext()
        {
            this.currentGameContext.OnGamePreparedEvent -= this.OnGamePrepared;
            this.currentGameContext.OnGameReadyEvent -= this.OnGameReady;
            this.currentGameContext.OnGameStartedEvent -= this.OnGameStarted;
            this.currentGameContext.OnGamePausedEvent -= this.OnGamePaused;
            this.currentGameContext.OnGameResumedEvent -= this.OnGameResumed;
            this.currentGameContext.OnGameFinishedEvent -= this.OnGameFinished;
            this.currentGameContext = null;
        }

        #region GameCallbacks

        private void OnGamePrepared(object sender)
        {
            foreach (var view in this.forms)
            {
                view.OnGamePrepared(sender);
            }
            
            this.OnGamePrepared(this, sender);
        }

        private void OnGameReady(object sender)
        {
            foreach (var view in this.forms)
            {
                view.OnGameReady(sender);
            }
            
            this.OnGameReady(this, sender);
        }

        private void OnGameStarted(object sender)
        {
            foreach (var view in this.forms)
            {
                view.OnGameStarted(sender);
            }
            
            this.OnGameStarted(this, sender);
        }

        private void OnGamePaused(object sender)
        {
            foreach (var view in this.forms)
            {
                view.OnGamePaused(sender);
            }
            
            this.OnGamePaused(this, sender);
        }

        private void OnGameResumed(object sender)
        {
            foreach (var view in this.forms)
            {
                view.OnGameResumed(sender);
            }
            
            this.OnGameResumed(this, sender);
        }

        private void OnGameFinished(object sender)
        {
            foreach (var view in this.forms)
            {
                view.OnGameFinished(sender);
            }
            
            this.OnGameFinished(this, sender);
        }

        #endregion

        protected virtual void OnGamePrepared(GameInterface _, object sender)
        {
        }

        protected virtual void OnGameReady(GameInterface _, object sender)
        {
        }

        protected virtual void OnGameStarted(GameInterface _, object sender)
        {
        }

        protected virtual void OnGamePaused(GameInterface _, object sender)
        {
        }

        protected virtual void OnGameResumed(GameInterface _, object sender)
        {
        }

        protected virtual void OnGameFinished(GameInterface _, object sender)
        {
        }
    }
}