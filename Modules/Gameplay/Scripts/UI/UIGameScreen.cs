using System;
using System.Collections.Generic;
using Gullis;
using OregoFramework.App;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class UIGameScreen : UIScreen
    {
        public bool IsContextAttached
        {
            get { return !ReferenceEquals(this.AttachedContext, null); }
        }

        public IGameContext AttachedContext { get; private set; }

        protected readonly HashSet<IUIGameController> Controllers;

        [SerializeField]
        private bool registerChildren;
        
        public UIGameScreen()
        {
            this.Controllers = new HashSet<IUIGameController>();
        }

        protected virtual void Awake()
        {
            var controllers = this.GetComponentsInChildren<IUIGameController>();
            foreach (var controller in controllers)
            {
                this.RegisterController(controller);
            }
        }
        
        public T ProvideContext<T>()
        {
            return (T) this.AttachedContext;
        }
        
        protected virtual bool RegisterController(IUIGameController controller)
        {
            if (this.Controllers.Add(controller))
            {
                controller.OnRegistered(this);
                return true;
            }
            
            return false;
        }

        protected virtual bool UnregisterController(IUIGameController controller)
        {
            if (this.Controllers.Remove(controller))
            {
                controller.OnUnregistered();
                return true;
            }
            
            return false;
        }

        protected virtual void AttachContext(IGameContext context)
        {
            if (this.IsContextAttached)
            {
                throw new Exception("Game context is already attached!");
            }

            context.OnGamePreparedEvent += this.OnGamePrepared;
            context.OnGameReadyEvent += this.OnGameReady;
            context.OnGameStartedEvent += this.OnGameStarted;
            context.OnGamePausedEvent += this.OnGamePaused;
            context.OnGameResumedEvent += this.OnGameResumed;
            context.OnGameFinishedEvent += this.OnGameFinished;
            this.AttachedContext = context;
        }

        protected virtual void DetachContext()
        {
            if (!this.IsContextAttached)
            {
                return;
            }

            var context = this.AttachedContext;
            context.OnGamePreparedEvent -= this.OnGamePrepared;
            context.OnGameReadyEvent -= this.OnGameReady;
            context.OnGameStartedEvent -= this.OnGameStarted;
            context.OnGamePausedEvent -= this.OnGamePaused;
            context.OnGameResumedEvent -= this.OnGameResumed;
            context.OnGameFinishedEvent -= this.OnGameFinished;

            this.AttachedContext = default;
        }

        protected virtual void OnGamePrepared(object sender)
        {
            foreach (var controller in this.Controllers)
            {
                controller.OnGamePrepared(sender);
            }
        }

        protected virtual void OnGameReady(object sender)
        {
            foreach (var controller in this.Controllers)
            {
                controller.OnGameReady(sender);
            }
        }

        protected virtual void OnGameStarted(object sender)
        {
            foreach (var controller in this.Controllers)
            {
                controller.OnGameStarted(sender);
            }
        }

        protected virtual void OnGamePaused(object sender)
        {
            foreach (var controller in this.Controllers)
            {
                controller.OnGamePaused(sender);
            }
        }

        protected virtual void OnGameResumed(object sender)
        {
            foreach (var controller in this.Controllers)
            {
                controller.OnGameResumed(sender);
            }
        }

        protected virtual void OnGameFinished(object sender)
        {
            foreach (var controller in this.Controllers)
            {
                controller.OnGameFinished(sender);
            }
        }
    }
}