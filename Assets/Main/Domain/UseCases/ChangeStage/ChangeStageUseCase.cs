using CheshireCatPoserTalk.Entity.GameStage;

namespace CheshireCatPoserTalk.Domain.UseCases.ChangeStage
{
    public class ChangeStageUseCase
    {
        private GameStage gameStage;

        public ChangeStageUseCase(GameStage gameStage)
        {
            this.gameStage = gameStage;
        }

        public void ChangeStage(GameStageType newStage)
        {
            gameStage.ChangeStage(newStage);
        }
    }
}
