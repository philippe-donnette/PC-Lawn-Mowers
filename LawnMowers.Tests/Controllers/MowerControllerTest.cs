using Microsoft.VisualStudio.TestTools.UnitTesting;
using PC.LawnMowers.Controllers;
using PC.LawnMowers.Model;
using PC.LawnMowers.Settings;

namespace LawnMowers.Tests.Controllers
{
    [TestClass]
    public class MowerControllerTest
    {
        private readonly IMowerController _controller;

        public MowerControllerTest()
        {
            _controller = new MowerController();
        }

        #region ChangeDirection
        [TestMethod]
        public void ChangeDirection_FromNorthToLeft_ShouldBeWestDirection()
        {
            Position position = new Position(1, 1, CompassEnum.North);
            Position newPosition = _controller.ChangeDirection(position, 'L');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.West);
        }

        [TestMethod]
        public void ChangeDirection_FromWestToLeft_ShouldBeSouthDirection()
        {
            Position position = new Position(1, 1, CompassEnum.West);
            Position newPosition = _controller.ChangeDirection(position, 'L');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.South);
        }

        [TestMethod]
        public void ChangeDirection_FromSouthToLeft_ShouldBeEastDirection()
        {
            Position position = new Position(1, 1, CompassEnum.South);
            Position newPosition = _controller.ChangeDirection(position, 'L');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.East);
        }

        [TestMethod]
        public void ChangeDirection_FromEastToLeft_ShouldBeNorthDirection()
        {
            Position position = new Position(1, 1, CompassEnum.East);
            Position newPosition = _controller.ChangeDirection(position, 'L');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.North);
        }

        [TestMethod]
        public void ChangeDirection_FromNorthToRight_ShouldBeEastDirection()
        {
            Position position = new Position(1, 1, CompassEnum.North);
            Position newPosition = _controller.ChangeDirection(position, 'R');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.East);
        }

        [TestMethod]
        public void ChangeDirection_FromEastToRight_ShouldBeSouthDirection()
        {
            Position position = new Position(1, 1, CompassEnum.East);
            Position newPosition = _controller.ChangeDirection(position, 'R');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.South);
        }

        [TestMethod]
        public void ChangeDirection_FromSouthToRight_ShouldBeWestDirection()
        {
            Position position = new Position(1, 1, CompassEnum.South);
            Position newPosition = _controller.ChangeDirection(position, 'R');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.West);
        }

        [TestMethod]
        public void ChangeDirection_FromWestToRight_ShouldBeNorthDirection()
        {
            Position position = new Position(1, 1, CompassEnum.West);
            Position newPosition = _controller.ChangeDirection(position, 'R');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.North);
        }

        [TestMethod]
        public void ChangeDirection_FromNorthNotUsingLorR_ShouldNotChangeDirection()
        {
            Position position = new Position(1, 1, CompassEnum.North);
            Position newPosition = _controller.ChangeDirection(position, 'A');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.North);
        }

        [TestMethod]
        public void ChangeDirection_FromEastNotUsingLorR_ShouldNotChangeDirection()
        {
            Position position = new Position(1, 1, CompassEnum.East);
            Position newPosition = _controller.ChangeDirection(position, 'A');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.East);
        }

        [TestMethod]
        public void ChangeDirection_FromSouthNotUsingLorR_ShouldNotChangeDirection()
        {
            Position position = new Position(1, 1, CompassEnum.South);
            Position newPosition = _controller.ChangeDirection(position, 'A');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.South);
        }

        [TestMethod]
        public void ChangeDirection_FromWestNotUsingLorR_ShouldNotChangeDirection()
        {
            Position position = new Position(1, 1, CompassEnum.West);
            Position newPosition = _controller.ChangeDirection(position, 'A');
            Assert.AreEqual(newPosition.CompassPoint, CompassEnum.West);
        }
        #endregion

        #region MoveForward
        [TestMethod]
        public void MoveForward_FromNorth_ShouldIncreasePositionYbyOne()
        {
            Position position = new Position(1, 1, CompassEnum.North);
            Position newPosition = _controller.MoveForward(position);
            Assert.AreEqual(newPosition.Y, position.Y + 1);
            Assert.AreEqual(newPosition.X, position.X);
            Assert.AreEqual(newPosition.CompassPoint, position.CompassPoint);
        }

        [TestMethod]
        public void MoveForward_FromEast_ShouldIncreasePositionXbyOne()
        {
            Position position = new Position(1, 1, CompassEnum.East);
            Position newPosition = _controller.MoveForward(position);
            Assert.AreEqual(newPosition.Y, position.Y);
            Assert.AreEqual(newPosition.X, position.X + 1);
            Assert.AreEqual(newPosition.CompassPoint, position.CompassPoint);
        }

        [TestMethod]
        public void MoveForward_FromSouth_ShouldDecreasePositionYbyOne()
        {
            Position position = new Position(1, 1, CompassEnum.South);
            Position newPosition = _controller.MoveForward(position);
            Assert.AreEqual(newPosition.Y, position.Y - 1);
            Assert.AreEqual(newPosition.X, position.X);
            Assert.AreEqual(newPosition.CompassPoint, position.CompassPoint);
        }

        [TestMethod]
        public void MoveForward_FromWest_ShouldDecreasePositionXbyOne()
        {
            Position position = new Position(1, 1, CompassEnum.West);
            Position newPosition = _controller.MoveForward(position);
            Assert.AreEqual(newPosition.Y, position.Y);
            Assert.AreEqual(newPosition.X, position.X - 1);
            Assert.AreEqual(newPosition.CompassPoint, position.CompassPoint);
        }
        #endregion
    }
}
