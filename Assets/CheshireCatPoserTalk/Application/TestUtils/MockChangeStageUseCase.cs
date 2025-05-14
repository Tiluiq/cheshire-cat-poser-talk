using System;
using CheshireCatPoserTalk.Domain.UseCases.ChangeStage;
using CheshireCatPoserTalk.Entity.GameStage;

namespace CheshireCatPoserTalk.Application.TestUtils
{
    public class MockChangeStageUseCase : IChangeStageUseCase
    {
        public GameStageType CurrentStage { get; private set; }

        public event Action<GameStageType> OnStageChanged;

        public void ChangeStage(GameStageType newStage)
        {
            CurrentStage = newStage;
            OnStageChanged?.Invoke(newStage);
        }
    }
}
