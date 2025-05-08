using NUnit.Framework;
using Entity.Flag.Global;

namespace Entity.Flag.Global.Test
{
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
}
