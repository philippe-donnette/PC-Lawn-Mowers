using Microsoft.VisualStudio.TestTools.UnitTesting;
using PC.LawnMowers.Controllers;
using PC.LawnMowers.Model;
using PC.LawnMowers.Settings;
using System.Collections.Generic;

namespace LawnMowers.Tests.Controllers
{
    [TestClass]
    public class RuleControllerTest
    {
        private readonly IRuleController _controller;

        public RuleControllerTest()
        {
            _controller = new RuleController();
        }

        #region HasMowerOnTheWay
        [TestMethod]
        public void HasMowerOnTheWay_ShouldReturnFalse()
        {
            Position position = new Position(1, 1, CompassEnum.East);
            List<Mower> mowers = new List<Mower>
            {
                new Mower { Position = new Position(3, 5, CompassEnum.East) },
                new Mower { Position = new Position(4, 5, CompassEnum.East) }
            };
            bool hasMowerOnTheWay = _controller.HasMowerOnTheWay(position, mowers);
            Assert.IsFalse(hasMowerOnTheWay);
        }

        [TestMethod]
        public void HasMowerOnTheWay_ShouldReturnTrue()
        {
            Position position = new Position(3, 5, CompassEnum.East);
            List<Mower> mowers = new List<Mower>
            {
                new Mower { Position = new Position(3, 5, CompassEnum.East) },
                new Mower { Position = new Position(3, 5, CompassEnum.East) },
                new Mower { Position = new Position(4, 5, CompassEnum.East) }
            };
            bool hasMowerOnTheWay = _controller.HasMowerOnTheWay(position, mowers);
            Assert.IsTrue(hasMowerOnTheWay);
        }

        [TestMethod]
        public void HasMowerOnTheWay_ShouldReturnFalseWhenNoMowerInList()
        {
            Position position = new Position(3, 5, CompassEnum.East);
            List<Mower> mowers = new List<Mower> {};
            bool hasMowerOnTheWay = _controller.HasMowerOnTheWay(position, mowers);
            Assert.IsFalse(hasMowerOnTheWay);
        }

        [TestMethod]
        public void HasMowerOnTheWay_ShouldReturnFalseWhenMowerListIsNull()
        {
            Position position = new Position(3, 5, CompassEnum.East);
            List<Mower> mowers = null;
            bool hasMowerOnTheWay = _controller.HasMowerOnTheWay(position, mowers);
            Assert.IsFalse(hasMowerOnTheWay);
        }
        #endregion

        #region IsOutOfLawn
        [TestMethod]
        public void IsOutOfLawn_ShouldReturnFalse()
        {
            Lawn lawn = new Lawn(5, 7);
            bool isOutOfLawn = _controller.IsOutOfLawn(1, 4, lawn);
            Assert.IsFalse(isOutOfLawn);
        }

        [TestMethod]
        public void IsOutOfLawn_ShouldReturnTrueWhenOutOfRightSide()
        {
            Lawn lawn = new Lawn(5, 7);
            bool isOutOfLawn = _controller.IsOutOfLawn(6, 4, lawn);
            Assert.IsTrue(isOutOfLawn);
        }

        [TestMethod]
        public void IsOutOfLawn_ShouldReturnTrueWhenOutOfLeftSide()
        {
            Lawn lawn = new Lawn(5, 7);
            bool isOutOfLawn = _controller.IsOutOfLawn(-1, 4, lawn);
            Assert.IsTrue(isOutOfLawn);
        }

        [TestMethod]
        public void IsOutOfLawn_ShouldReturnTrueWhenOutOfBottomSide()
        {
            Lawn lawn = new Lawn(5, 7);
            bool isOutOfLawn = _controller.IsOutOfLawn(5, -1, lawn);
            Assert.IsTrue(isOutOfLawn);
        }

        [TestMethod]
        public void IsOutOfLawn_ShouldReturnTrueWhenOutOfTopSide()
        {
            Lawn lawn = new Lawn(5, 7);
            bool isOutOfLawn = _controller.IsOutOfLawn(5, 8, lawn);
            Assert.IsTrue(isOutOfLawn);
        }
        #endregion
    }
}
