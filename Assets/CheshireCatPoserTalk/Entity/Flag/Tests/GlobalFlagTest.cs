using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CheshireCatPoserTalk.Entity.Flag;

namespace CheshireCatPoserTalk.Entity.Flag.Tests
{
    public class GlobalFlagTest
    {
        private GlobalFlag globalFlag;

        [SetUp]
        public void Setup()
        {
            globalFlag = new GlobalFlag();
        }

        [Test]
        public void SetIsCleared_フラグが更新できる()
        {
            Assert.IsFalse(globalFlag.IsCleared, "初期状態ではクリアフラグはfalse");

            globalFlag.SetIsCleared(true);

            Assert.IsTrue(globalFlag.IsCleared, "フラグをtrueに設定できる");

            globalFlag.SetIsCleared(false);

            Assert.IsFalse(globalFlag.IsCleared, "フラグをfalseに設定できる");
        }
    }
}
