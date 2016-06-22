using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PC.LawnMowers.Controllers;
using PC.LawnMowers.Model;
using System;
using System.Collections.Generic;

namespace LawnMowers.Tests.Controllers
{
    [TestClass]
    public class MainControllerTest
    {
        private readonly IMainController _controller;
        private readonly Mock<IInputController> _inputControllerMock;
        private readonly Mock<IRuleController> _ruleControllerMock;
        private readonly Mock<IMowerController> _mowerControllerMock;

        public MainControllerTest()
        {
            _inputControllerMock = new Mock<IInputController>();
            _ruleControllerMock = new Mock<IRuleController>();
            _mowerControllerMock = new Mock<IMowerController>();
            _controller = new MainController(_inputControllerMock.Object, _ruleControllerMock.Object, 
                _mowerControllerMock.Object);
        }
        
        #region Start
        [TestMethod]
        public void Start_ShouldMatchOutput_SomeTest1()
        {
            string input = "5 5" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            string expectedOutput = "1 3 N" + Environment.NewLine;
            
            _mowerControllerMock.Setup(x => x.MoveForward(It.IsAny<Position>())).Returns(new Position(1, 3, "N"));
            _mowerControllerMock.Setup(x => x.ChangeDirection(It.IsAny<Position>(), It.IsAny<char>())).Returns(new Position(1, 3, "N"));
            _ruleControllerMock.Setup(x => x.HasMowerOnTheWay(It.IsAny<Position>(), It.IsAny<List<Mower>>())).Returns(false);
            _ruleControllerMock.Setup(x => x.IsOutOfLawn(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Lawn>())).Returns(false);
            _inputControllerMock.Setup(x => x.ReadInput(It.IsAny<string>())).Returns(new Lawn
            {
                Length = 5,
                Width = 5,
                Mowers = new List<Mower>
                    {
                        new Mower { Position = new Position(1, 2, "N"), Instructions = new List<char> { 'L', 'M', 'L', 'M', 'L', 'M', 'L', 'M', 'M' } }
                    }
            });

            string actualOutput = _controller.Start(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void Start_ShouldThrowExceptionWhenAnotherMowerIsOnTheWay()
        {
            string input = "5 5" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            
            _mowerControllerMock.Setup(x => x.MoveForward(It.IsAny<Position>())).Returns(new Position(1, 3, "N"));
            _mowerControllerMock.Setup(x => x.ChangeDirection(It.IsAny<Position>(), It.IsAny<char>())).Returns(new Position(1, 3, "N"));
            _ruleControllerMock.Setup(x => x.HasMowerOnTheWay(It.IsAny<Position>(), It.IsAny<List<Mower>>())).Returns(true);
            _ruleControllerMock.Setup(x => x.IsOutOfLawn(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Lawn>())).Returns(false);
            _inputControllerMock.Setup(x => x.ReadInput(input)).Returns(new Lawn
            {
                Length = 5,
                Width = 5,
                Mowers = new List<Mower>
                {
                    new Mower { Position = new Position(1, 2, "N"), Instructions = new List<char> { 'L', 'M', 'L', 'M', 'L', 'M', 'L', 'M', 'M' } },
                    new Mower { Position = new Position(3, 3, "E"), Instructions = new List<char> { 'M', 'M', 'R', 'M', 'M', 'R', 'M', 'R', 'R', 'M' } }
                }
            });
            string output = _controller.Start(input);
            StringAssert.Contains(output, "Cannot go further, another mower is on the way");
        }

        [TestMethod]
        public void Start_ShouldThrowExceptionWhenMowerIsGoingOutOfTheLawn()
        {
            string input = "5 5" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            string expectedOutput = "1 3 N" + Environment.NewLine;
            expectedOutput += "1 3 N";

            _mowerControllerMock.Setup(x => x.MoveForward(It.IsAny<Position>())).Returns(new Position(1, 2, "N"));
            _mowerControllerMock.Setup(x => x.ChangeDirection(It.IsAny<Position>(), It.IsAny<char>())).Returns(new Position(1, 2, "N"));
            _ruleControllerMock.Setup(x => x.HasMowerOnTheWay(It.IsAny<Position>(), It.IsAny<List<Mower>>())).Returns(false);
            _ruleControllerMock.Setup(x => x.IsOutOfLawn(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Lawn>())).Returns(true);
            _inputControllerMock.Setup(x => x.ReadInput(input)).Returns(new Lawn
            {
                Length = 5,
                Width = 5,
                Mowers = new List<Mower>
                {
                    new Mower { Position = new Position(1, 2, "N"), Instructions = new List<char> { 'L', 'M', 'L', 'M', 'L', 'M', 'L', 'M', 'M' } },
                    new Mower { Position = new Position(3, 3, "E"), Instructions = new List<char> { 'M', 'M', 'R', 'M', 'M', 'R', 'M', 'R', 'R', 'M' } }
                }
            });

            string output = _controller.Start(input);
            StringAssert.Contains(output, "Cannot go further, mower will be outside of lawn");
        }
        #endregion
    }
}
