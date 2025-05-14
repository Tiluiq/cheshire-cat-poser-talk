using System;

namespace CheshireCatPoserTalk.Entity.GameStage
{
    public enum GameStageType
    {
        TitleStage,
        MainGameStage,
        EndingStage,
    }

    public class GameStage
    {
        public GameStageType CurrentStage { get; private set; }

        public event Action<GameStageType> OnStageChanged;
        public void ChangeStage(GameStageType newStage)
        {
            if (CurrentStage != newStage)
            {
                CurrentStage = newStage;
                OnStageChanged?.Invoke(CurrentStage);
            }
        }
    }
}