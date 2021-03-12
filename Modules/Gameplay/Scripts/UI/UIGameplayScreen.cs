using System;
using Gullis;
using OregoFramework.App;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Module
{
    public abstract class UIGameplayScreen<T> : UIScreen where T : IGameContext
    {
        protected T attachedContext;

        [SerializeField]
        private UIGameplayElement uiElements;

        protected virtual void AttachContext(T gameContext)
        {
            this.attachedContext = gameContext;
            this.attachedContext.OnGamePreparedEvent += this.OnGamePrepared;
            this.attachedContext.OnGameReadyEvent += this.OnGameReady;
            this.attachedContext.OnGameStartedEvent += this.OnGameStarted;
            this.attachedContext.OnGamePausedEvent += this.OnGamePaused;
            this.attachedContext.OnGameResumedEvent += this.OnGameResumed;
            this.attachedContext.OnGameFinishedEvent += this.OnGameFinished;
        }

        private void OnGameFinished(object obj)
        {
            
        }

        private void OnGameStarted(object obj)
        {
            
        }

        private void OnGamePrepared(object obj)
        {
            
        }


        #region Lifecycle

        private void OnEnable()
        {
            var buttonPause = this.m_params.buttonPause;
            buttonPause.OnOpenPopupEvent += this.OnOpenPausePopup;
            buttonPause.OnClosePopupEvent += this.OnClosePausePopup;
        }

        private IEnumerator Start()
        {
            yield return this.LoadGame();
            LevelAnalytics.LogLevelStarted();
        }

        private void AttachedStarted(object sender)
        {
            var buttonRestart = this.m_params.buttonRestart;
            buttonRestart.gameObject.SetActive(true);
            buttonRestart.onClick.AddListener(this.OnButtonRestartClicked);
            Destroy(this.loadingScene);
            UIGameplayScreenAnalytics.LogScreenShown();
        }

        private void AttachedFinished(object sender)
        {
            this.m_params.buttonPause.gameObject.SetActive(false);
            var buttonRestart = this.m_params.buttonRestart;
            buttonRestart.gameObject.SetActive(false);
            buttonRestart.onClick.RemoveListener(this.OnButtonRestartClicked);
            if (!ReferenceEquals(sender, this))
            {
                LevelAnalytics.LogLevelEnded();
                this.StartCoroutine(this.ShowEndGamePopupRoutine());
            }
        }

        public override void OnExit(object sender)
        {
            this.UnloadGame();
        }
        
        private void OnDisable()
        {
            var buttonSettings = this.m_params.buttonPause;
            buttonSettings.OnOpenPopupEvent -= this.OnOpenPausePopup;
            buttonSettings.OnClosePopupEvent -= this.OnClosePausePopup;
        }

        #endregion
        
        #region UICallbacks

        private void OnButtonRestartClicked()
        {
            AppSounds.PlayClick();
            UIGameplayScreenAnalytics.LogButtonRestartClicked();
            PlayerAnalyticsNode.LogRestartEvent(this.attachedContext, "screen_gameplay_button_restart");
            this.attachedContext.FinishGame(this);
            this.ChangeScreen<UIGameplayScreen>();
            AnalyticsModule.LogEvent("screen_gameplay_opened", new AnalyticsParam("source_id", "screen_gameplay_restart"));
        }
        
        private void OnClosePausePopup()
        {
            if (this.attachedContext.gameStatus == GameStatus.PAUSING)
            {
                this.attachedContext.ResumeGame(this);
            }
        }

        private void OnOpenPausePopup()
        {
            if (this.attachedContext.gameStatus < GameStatus.PAUSING)
            {
                this.attachedContext.PauseGame(this);
            }
        }

        #endregion
        
        private IEnumerator LoadGame()
        {
            var scenePrefab = new Reference<GameContext>();
            yield return this.levelLoaderInteractor.LoadSelectedScenePrefab(scenePrefab);
            this.gameplayInteractor.CreateGameContext(this, scenePrefab.value);
            this.attachedContext = this.gameplayInteractor.gameContext;
            this.attachedContext.OnGameStartedEvent += this.AttachedStarted;
            this.attachedContext.OnGameFinishedEvent += this.AttachedFinished;
            GameplayAnalytics.RegisterObservers(this.attachedContext);
            var interfacePrefab = new Reference<GameInterfaceSystem>();
            yield return this.levelLoaderInteractor.LoadSelectedInterfacePrefab(interfacePrefab);
            this.gameInterfaceSystem = Instantiate(interfacePrefab.value, this.myTransform);
            this.gameInterfaceSystem.transform.SetAsFirstSibling();
            this.gameInterfaceSystem.AttachContext(this.attachedContext);
            GameplayAnalytics.RegisterObservers(this.gameInterfaceSystem);
            this.gameplayInteractor.LaunchGameContext(this);
        }
        
        private IEnumerator ShowEndGamePopupRoutine()
        {
            yield return new WaitForSeconds(this.m_params.pauseAfterEndGame);
            this.endGameController.ShowEndGamePopup(this.myTransform);
        }
        
        private void UnloadGame()
        {
            this.gameInterfaceSystem.DetachContext();
            this.attachedContext.OnGameFinishedEvent -= this.AttachedFinished;
            this.gameplayInteractor.DestroyGameContext(this);
            GameplayAnalytics.UnregisterAllObservers();
        }
    }
}