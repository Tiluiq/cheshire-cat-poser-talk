using System.Collections;
using System.Threading.Tasks;
using CheshireCatPoserTalk.Application.TestUtils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CheshireCatPoserTalk.Application.MainGame.Tests
{
    public class AnimationControllerTest
    {
        private MockAnimationView animationView;
        private AnimationController animationController;

        [SetUp]
        public void Setup()
        {
            animationView = new MockAnimationView();
            animationController = new AnimationController(animationView);
        }

        [Test]
        public async Task PlayAnimationAsync_IAnimationViewのPlayAnimationAsyncが呼ばれる()
        {
            string targetId = "Target1";
            string animationName = "Animation1";

            Assert.IsFalse(animationView.IsPlayAnimationAsyncCalled, "IAnimationViewのPlayAnimationAsyncが呼ばれていないことを確認");

            await animationController.PlayAnimationAsync(targetId, animationName);

            Assert.IsTrue(animationView.IsPlayAnimationAsyncCalled, "IAnimationViewのPlayAnimationAsyncが呼ばれたことを確認");
        }
    }
}
