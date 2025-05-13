using CheshireCatPoserTalk.Entity.GameStage;
using System;

namespace CheshireCatPoserTalk.Domain.UseCases.ChangeStage
{
    public class ChangeStageUseCase : IChangeStageUseCase, IDisposable
    {
        private GameStage gameStage;

        public ChangeStageUseCase(GameStage gameStage)
        {
            this.gameStage = gameStage;

            this.gameStage.OnStageChanged += OnStageChangedHandler;
        }

        public GameStageType CurrentStage => gameStage.CurrentStage;

        public event Action<GameStageType> OnStageChanged;
        private void OnStageChangedHandler(GameStageType stage) => OnStageChanged?.Invoke(stage);

        public void ChangeStage(GameStageType newStage)
        {
            gameStage.ChangeStage(newStage);
        }

        public void Dispose() => gameStage.OnStageChanged -= OnStageChangedHandler;
    }
}
