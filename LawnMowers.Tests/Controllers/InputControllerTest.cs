using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PC.LawnMowers.Controllers;
using PC.LawnMowers.Model;
using PC.LawnMowers.Settings;
using PC.LawnMowers.Validators;
using System;
using System.Linq;

namespace LawnMowers.Tests.Controllers
{
    [TestClass]
    public class InputControllerTest
    {
        private readonly IInputController _controller;
        private readonly Mock<IInputValidator> _inputValidatorMock;

        public InputControllerTest()
        {
            _inputValidatorMock = new Mock<IInputValidator>();
            _controller = new InputController(_inputValidatorMock.Object);
        }

        #region ReadInput
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "Mower commands input is invalid")]
        public void ReadInput_ShouldThroughNotSupportedExceptionWhenValidateMowerCommandReturnFalse()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize("5 8")).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition("1 2 N")).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(new Position(1, 2, CompassEnum.North), 5, 8)).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands("LMLMLMLMM")).Returns(false);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            Lawn lawn = _controller.ReadInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "Mower position input is invalid")]
        public void ReadInput_ShouldThroughNotSupportedExceptionWhenValidateMowerPositionFormatReturnFalse()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize("5 8")).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition("1 2 N")).Returns(false);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(new Position(1, 2, CompassEnum.North), 5, 8)).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands("LMLMLMLMM")).Returns(true);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            Lawn lawn = _controller.ReadInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "Mower position input is out of range")]
        public void ReadInput_ShouldThroughNotSupportedExceptionWhenValidateMowerPositionOutOfRangeReturnFalse()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize("5 8")).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition("1 2 N")).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(new Position(1, 2, CompassEnum.North), 5, 8)).Returns(false);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands("LMLMLMLMM")).Returns(true);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            Lawn lawn = _controller.ReadInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "Lawn width and length input is invalid")]
        public void ReadInput_ShouldThroughNotSupportedExceptionWhenValidateLawnSizeReturnFalse()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize("5 8")).Returns(false);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition("1 2 N")).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(new Position(1, 2, CompassEnum.North), 5, 8)).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands("LMLMLMLMM")).Returns(true);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            Lawn lawn = _controller.ReadInput(input);
        }

        [TestMethod]
        public void ReadInput_ShouldMatchLawnWidthAndLengthFromInput()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize(It.IsAny<string>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(It.IsAny<string>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(It.IsAny<Position>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands(It.IsAny<string>())).Returns(true);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            Lawn lawn = _controller.ReadInput(input);
            Assert.AreEqual(lawn.Width, 5);
            Assert.AreEqual(lawn.Length, 8);
        }

        [TestMethod]
        public void ReadInput_ShouldMatchNumberOfMowers()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize(It.IsAny<string>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(It.IsAny<string>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(It.IsAny<Position>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands(It.IsAny<string>())).Returns(true);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM" + Environment.NewLine;
            input += "3 1 S" + Environment.NewLine;
            input += "MRMM";
            Lawn lawn = _controller.ReadInput(input);
            Assert.AreEqual(lawn.Mowers.Count, 3);
        }

        [TestMethod]
        public void ReadInput_ShouldMatchMowers()
        {
            _inputValidatorMock.Setup(x => x.ValidateLawnSize(It.IsAny<string>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(It.IsAny<string>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerPosition(It.IsAny<Position>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _inputValidatorMock.Setup(x => x.ValidateMowerCommands(It.IsAny<string>())).Returns(true);

            string input = "5 8" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM" + Environment.NewLine;
            input += "3 1 S" + Environment.NewLine;
            input += "MRMM";
            Lawn lawn = _controller.ReadInput(input);
            Mower mower1 = lawn.Mowers.Take(1).First();
            Assert.AreEqual(mower1.Position.X, 1);
            Assert.AreEqual(mower1.Position.Y, 2);
            Assert.AreEqual(mower1.Position.CompassPoint, CompassEnum.North);
            Assert.AreEqual(mower1.Instructions.Count, 9);
            Assert.AreEqual(string.Join("", mower1.Instructions.ToArray()), "LMLMLMLMM");
            Mower mower2 = lawn.Mowers.Skip(1).Take(1).First();
            Assert.AreEqual(mower2.Position.X, 3);
            Assert.AreEqual(mower2.Position.Y, 3);
            Assert.AreEqual(mower2.Position.CompassPoint, CompassEnum.East);
            Assert.AreEqual(mower2.Instructions.Count, 10);
            Assert.AreEqual(string.Join("", mower2.Instructions.ToArray()), "MMRMMRMRRM");
            Mower mower3 = lawn.Mowers.Skip(2).Take(1).First();
            Assert.AreEqual(mower3.Position.X, 3);
            Assert.AreEqual(mower3.Position.Y, 1);
            Assert.AreEqual(mower3.Position.CompassPoint, CompassEnum.South);
            Assert.AreEqual(mower3.Instructions.Count, 4);
            Assert.AreEqual(string.Join("", mower3.Instructions.ToArray()), "MRMM");
        }
        #endregion
    }
}
