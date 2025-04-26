using NUnit.Framework;
using Entity.GlobalScene;

public class GlobalSceneTest
{
    [Test]
    public void デフォルトはPreGameScene()
    {
        var globalScene = new GlobalScene();
        Assert.AreEqual(GlobalSceneType.PreGameScene, globalScene.CurrentScene);
    }

    [Test]
    public void SetSceneメソッドでシーンを変更できる()
    {
        var globalScene = new GlobalScene();
        globalScene.SetScene(GlobalSceneType.MainGameScene);
        Assert.AreEqual(GlobalSceneType.MainGameScene, globalScene.CurrentScene);
    }
}