using NUnit.Framework;
using Entity.GlobalFlag;

public class GlobalFlagTest
{
    [Test]
    public void IsClearedフラグが更新できる()
    {
        var globalFlag = new GlobalFlag();
        Assert.IsFalse(globalFlag.IsCleared);
        globalFlag.SetIsCleared(true);
        Assert.IsTrue(globalFlag.IsCleared);
    }
}