using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CheshireCatPoserTalk.Entity.GameStage;

namespace CheshireCatPoserTalk.Entity.GameStage.Tests
{
    public class GameStageTest
    {
        private GameStage gameStage;

        [SetUp]
        public void Setup()
        {
            gameStage = new GameStage();
        }

        [Test]
        public void ChangeStage_別のStageに変更するとOnStageChangedが走る()
        {
            var stageChangedCount = 0;
            Action<GameStageType> onStageChangeCalled = (gameStageType) => { stageChangedCount++; };
            gameStage.OnStageChanged += onStageChangeCalled;

            gameStage.ChangeStage(GameStageType.MainGameStage);
            Assert.AreEqual(1, stageChangedCount);
            gameStage.ChangeStage(GameStageType.EndingStage);
            Assert.AreEqual(2, stageChangedCount);
            gameStage.ChangeStage(GameStageType.TitleStage);
            Assert.AreEqual(3, stageChangedCount);
        }

        [Test]
        public void ChangeStage_同じStageに変更してもOnStageChangedは走らない()
        {
            var stageChangedCount = 0;
            Action<GameStageType> onStageChangeCalled = (gameStageType) => { stageChangedCount++; };
            gameStage.OnStageChanged += onStageChangeCalled;

            gameStage.ChangeStage(GameStageType.MainGameStage);
            Assert.AreEqual(1, stageChangedCount);
            gameStage.ChangeStage(GameStageType.MainGameStage);
            Assert.AreEqual(1, stageChangedCount);
            gameStage.ChangeStage(GameStageType.EndingStage);
            Assert.AreEqual(2, stageChangedCount);
            gameStage.ChangeStage(GameStageType.EndingStage);
            Assert.AreEqual(2, stageChangedCount);
        }
    }
}
