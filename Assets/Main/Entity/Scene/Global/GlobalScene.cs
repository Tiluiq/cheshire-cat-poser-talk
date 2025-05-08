namespace Entity.Scene.Global
{
    public enum GlobalSceneType
    {
        PreGameScene,
        MainGameScene,
    }

    public class GlobalScene
    {
        public GlobalSceneType CurrentScene { get; private set; }

        public void SetScene(GlobalSceneType sceneType)
        {
            CurrentScene = sceneType;
        }
    }
}