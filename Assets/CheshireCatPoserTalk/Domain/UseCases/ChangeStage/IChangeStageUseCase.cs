
using System;
using CheshireCatPoserTalk.Entity.GameStage;

namespace CheshireCatPoserTalk.Domain.UseCases.ChangeStage
{
    public interface IChangeStageUseCase
    {
        GameStageType CurrentStage { get; }

        event Action<GameStageType> OnStageChanged;
        void ChangeStage(GameStageType newStage);
    }
}
